using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Vostok.Metrics.Abstractions
{
    public class MetricEvent 
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public List<Tag> Tags { get; set; }
        public string AggregationType { get; set; }
    }

    public struct Tag
    {
        public string Key { get; }
        public string Value { get; }

        public Tag(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #region Equality
        public bool Equals(Tag other) =>
            string.Equals(Key, other.Key, StringComparison.InvariantCulture) && string.Equals(Value, other.Value, StringComparison.InvariantCulture);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Tag other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key != null ? StringComparer.InvariantCulture.GetHashCode(Key) : 0) * 397) ^ (Value != null ? StringComparer.InvariantCulture.GetHashCode(Value) : 0);
            }
        }

        public static bool operator==(Tag left, Tag right) =>
            left.Equals(right);

        public static bool operator!=(Tag left, Tag right) =>
            !left.Equals(right);
        #endregion
    }

    public class Gauge : IScrapableMetric
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public TimeSpan ScrapePeriod { get; set; }
        public double Value { get; private set; }

        private readonly IMetricContext context;

        public Gauge(IMetricContext context)
        {
            this.context = context;
        }

        public void Set(double value)
        {
            this.Value = value;
        }

        public void Scrape()
        {
            var tags = context.GetStaticTags();
            tags.Add(new Tag("_name", Name));
            tags.AddRange(context.GetDynamicTags());
            
            var me = new MetricEvent
            {
                Value = Value,
                Timestamp = DateTimeOffset.Now,
                AggregationType = null,
                Unit = Unit,
                Tags = tags
            };

            context.Sender.Send(me);
        }
    }

    // auto-generate these extensions
    public static class GaugeExtensions
    {
        public static ClassTaggedMetric<Gauge, T> Gauge<T>(this IMetricContext context, string name, string unit, TimeSpan scrapePeriod)
        {
            var metric = new ClassTaggedMetric<Gauge, T>(c => CreateGauge(name, unit, scrapePeriod, c), context);
            return metric;
        }

        public static KeysTaggedMetric2<Gauge> Gauge(this IMetricContext context, string name, string unit, TimeSpan scrapePeriod, string key1, string key2)
        {
            return new KeysTaggedMetric2<Gauge>(
                c => CreateGauge(name, unit, scrapePeriod, c),
                context,
                key1,
                key2);
        }

        public static Gauge Gauge(this IMetricContext context, string name, string unit, TimeSpan scrapePeriod)
        {
            return CreateGauge(name, unit, scrapePeriod, context);
        }

        private static Gauge CreateGauge(string name, string unit, TimeSpan scrapePeriod, IMetricContext context)
        {
            var gauge = new Gauge(context) {Name = name, Unit = unit, ScrapePeriod = scrapePeriod};
            context.Scraper.Register(gauge);
            return gauge;
        }
    }

    public class Timing
    {
        public string Name { get; set; }
        public string Unit { get; set; }

        private readonly IMetricContext context;

        public Timing(IMetricContext context)
        {
            this.context = context;
        }

        public void Observe(double value)
        {
            var tags = context.GetStaticTags();
            tags.Add(new Tag("_name", Name));
            tags.AddRange(context.GetDynamicTags());

            var me = new MetricEvent
            {
                Tags = tags,
                Unit = Unit,
                Value = value,
                Timestamp = DateTimeOffset.UtcNow,
                AggregationType = "timing"
            };

            context.Sender.Send(me);
        }
    }
    
    // auto-generate these extensions
    public static class TimingExtensions
    {
        public static ClassTaggedMetric<Timing, T> Timing<T>(this IMetricContext context, string name, string unit)
        {
            var metric = new ClassTaggedMetric<Timing, T>(
                c => CreateTiming(name, unit, context),
                context);
            return metric;
        }

        public static KeysTaggedMetric2<Timing> Timing(this IMetricContext context, string name, string unit, string key1, string key2)
        {
            return new KeysTaggedMetric2<Timing>(
                c => CreateTiming(name, unit, c),
                context,
                key1,
                key2);
        }

        public static Timing Timing(this IMetricContext context, string name, string unit)
        {
            return CreateTiming(name, unit, context);
        }

        private static Timing CreateTiming(string name, string unit, IMetricContext c) =>
            new Timing(c) {Name = name, Unit = unit};
    }

    public class TaggedWrapper : IMetricContext
    {
        private readonly IMetricContext parentContext;
        private readonly List<Tag> staticTags;
        private readonly List<Tag> dynamicTags;

        public TaggedWrapper(List<Tag> staticTags, List<Tag> dynamicTags, IMetricContext parentContext)
        {
            this.parentContext = parentContext;
            this.staticTags = staticTags;
            this.dynamicTags = dynamicTags;
        }

        public IMetricEventSender Sender =>
            parentContext.Sender;
        public IMetricEventScraper Scraper =>
            parentContext.Scraper;

        public List<Tag> GetStaticTags()
        {
            var result = parentContext.GetStaticTags();
            result.AddRange(staticTags);
            return result;
        }

        public List<Tag> GetDynamicTags()
        {
            var result = parentContext.GetDynamicTags();
            result.AddRange(dynamicTags);
            return result;
        }
    }

    public class TaggedMetric<TMetric>
    {
        protected readonly Func<IMetricContext, TMetric> metricFactory;
        protected readonly IMetricContext context;

        // implement comparer
        // or write custom collection for this
        protected readonly ConcurrentDictionary<List<Tag>, TMetric> metrics;
        private static readonly IEqualityComparer<List<Tag>> comparer = EqualityComparer<List<Tag>>.Default;

        public TaggedMetric(Func<IMetricContext, TMetric> metricFactory, IMetricContext context)
        {
            this.metricFactory = metricFactory;
            this.context = context;
            metrics = new ConcurrentDictionary<List<Tag>, TMetric>(comparer);
        }

        protected TMetric For(List<Tag> dynamicTags)
        {
            return metrics.GetOrAdd(
                dynamicTags,
                ts =>
                {
                    var contextWrapper = new TaggedWrapper(new List<Tag>(), ts, context);
                    return metricFactory(contextWrapper);
                });
        }
    }

    // auto generate this for reasonable amount of keys (1..8 ~ as tuple)

    public class KeysTaggedMetric2<TMetric> : TaggedMetric<TMetric>
    {
        private readonly string key1;
        private readonly string key2;

        public KeysTaggedMetric2(Func<IMetricContext, TMetric> metricFactory, IMetricContext context, string key1, string key2)
            : base(metricFactory, context)
        {
            this.key1 = key1;
            this.key2 = key2;
        }

        public TMetric For(string value1, string value2)
        {
            var dynamicTags = context.GetDynamicTags();
            dynamicTags.Add(new Tag(key1, value1));
            dynamicTags.Add(new Tag(key2, value2));
            return For(dynamicTags);
        }
    }

    public class ClassTaggedMetric<TMetric, TTags> : TaggedMetric<TMetric>
    {
        public ClassTaggedMetric(Func<IMetricContext, TMetric> metricFactory, IMetricContext context)
            : base(metricFactory, context)
        {
        }

        public TMetric For(TTags tags)
        {
            var dynamicTags = context.GetDynamicTags();
            Populate(dynamicTags, tags);
            return For(dynamicTags);
        }

        private void Populate(List<Tag> dynamicTags, TTags tags)
        {
            // populates dynamicTags with fields from TTags
            dynamicTags.Add(new Tag("key1", "value1"));
        }
    }

    public interface IMetricContext
    {
        IMetricEventSender Sender { get; }
        
        IMetricEventScraper Scraper { get; }
        
        List<Tag> GetStaticTags();
        List<Tag> GetDynamicTags();
    }

    public interface IMetricEventScraper
    {
        void Register(IScrapableMetric metric);
        void Unregister(IScrapableMetric metric);
    }

    public interface IScrapableMetric
    {
        TimeSpan ScrapePeriod { get; }
        void Scrape();
    }

    public interface IMetricEventSender
    {
        void Send(MetricEvent @event);
    }

    internal class ConsoleMetricEventSender : IMetricEventSender
    {
        public void Send(MetricEvent @event)
        {
            var tags = string.Join(".", @event.Tags.Select(t => $"({t.Key} : {t.Value})"));
            Console.WriteLine($"{tags} {@event.Timestamp} {@event.Value} {@event.Unit} {@event.AggregationType}");
        }
    }

    public class RootContext : IMetricContext
    {
        public RootContext(IMetricEventSender sender, IMetricEventScraper scraper = null)
        {
            Sender = sender;
            // use default scraper here
            Scraper = scraper;
        }

        public IMetricEventSender Sender { get; }
        public IMetricEventScraper Scraper { get; }

        public List<Tag> GetStaticTags() =>
            new List<Tag>();

        public List<Tag> GetDynamicTags() =>
            new List<Tag>();
    }

    public class Program
    {
        public static void Main()
        {
            PrimitiveGauge();
            StringTaggedGauge();
            ClassTaggedGauge();

            StringTaggedTiming();

            ConfigureSender();

            ChildContexts();
            
            // to be discussed
            ExternalDynamicTag();
            OpenStringTagging();
            AnonymousClassTagging();
        }

        private static void ChildContexts()
        {
            var root = new RootContext(new ConsoleMetricEventSender());
            
            var applicationContext = new TaggedWrapper(
                new List<Tag>
                {
                    new Tag("env", "prod"),
                    new Tag("app", "fat-service")
                },
                new List<Tag>(),
                root);
            
            var replicaContext = new TaggedWrapper(
                new List<Tag>{new Tag("replica", "3")},
                new List<Tag>(), 
                applicationContext);

            // We get application context from hosting
            // We create replicaContext ourselves
            
            var reqTiming = applicationContext.Timing("req-duration", "seconds");
            var queueLengthGauge = replicaContext.Gauge("queue-length", "items", TimeSpan.FromSeconds(1));

            reqTiming.Observe(10);
            queueLengthGauge.Set(2345);
        }

        private static void AnonymousClassTagging()
        {
            var context = new RootContext(new ConsoleMetricEventSender());

            var timing = context.Timing<object>("my-timing", "seconds");
            
            timing
                .For(new {Clid = "fat-service", DocType = "doc1"})
                .Observe(100);
            
            // 1. We don't know the number of keys at construction time
            // 2. How will we check that the same keys are passed to For(...)? 
        }

        private static void OpenStringTagging()
        {
            var context = new RootContext(new ConsoleMetricEventSender());

            var timing = context.Timing("my-timing", "seconds", "clid", "doc-type");

            // Should we allow this?
            
            // timing
            //     .For("fat-service")
            //     .For("doc1")
            //     .Observe(100);
            
            // Or maybe something more complex:

            // timing
            //     .For("clid", "fat-service")
            //     .For("doc-type", "doc1")
            //     .Observe(100);
        }

        private static void StringTaggedTiming()
        {
            var context = new RootContext(new ConsoleMetricEventSender());

            var timing = context.Timing("my-timing", "seconds", "clid", "doc-type");
            
            timing
                .For("fat-service", "doc1")
                .Observe(100);
        }

        private static void ConfigureSender()
        {
            var sender = new ConsoleMetricEventSender();
            var context = new RootContext(sender);
            
            // You can reconfigure Context with Wrapper (.SetSender extension method)
        }

        // There are several problems with this scenario
        //
        // 1.a What if there are two 'using' in one execution flow and three in another?
        // 1.b We don't know for sure how many dynamic tags we have.
        //     This may affect performance of TagList -> TMetric mapping
        //
        // 2. If we have a simple Gauge without dynamic keys we can't cache it anymore because external value can change
        private static void ExternalDynamicTag()
        {
            var context = new RootContext(new ConsoleMetricEventSender());
            var gauge = context.Gauge("nya-nya", "seconds", TimeSpan.FromSeconds(10), "mykey1", "mykey2");
            
            // var dynamicValue = "value3";
            // using (new SetDynamicTag("key3", dynamicValue))
            // {
            //     // MetricEvent: "nya-nya".value3.value1.value2
            //     gauge.For("value1", "value2").Set(14);
            // }
        }

        private static void StringTaggedGauge()
        {
            var context = new RootContext(new ConsoleMetricEventSender());
            var taggedGauge = context.Gauge("nya-nya", "seconds", TimeSpan.FromSeconds(10), "clid", "docType");
            taggedGauge
                .For("fat-service", "doc-type-1")
                .Set(10);
        }

        private static void ClassTaggedGauge()
        {
            var context = new RootContext(new ConsoleMetricEventSender());
            var taggedGauge = context.Gauge<Values>("nya-nya", "seconds", TimeSpan.FromSeconds(10));
            taggedGauge
                .For(new Values {ClientId = "clid1", DocType = "docType1"})
                .Set(100);
        }
        
        private class Values
        {
            public string ClientId { get; set; }
            public string DocType { get; set; }
        }

        private static void PrimitiveGauge()
        {
            var context = new RootContext(new ConsoleMetricEventSender());
            var primitiveGauge = new Gauge(context);
            primitiveGauge.Set(10);
        }
    }
}