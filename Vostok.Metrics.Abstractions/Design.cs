using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Vostok.Metrics.Abstractions
{
    public class MetricEvent
    {
        public readonly double Value;
        public readonly DateTimeOffset Timestamp;
        public readonly string Unit;
        public readonly string AggregationType;
        public readonly MetricTags Tags;

        public MetricEvent(double value, DateTimeOffset timestamp, string unit, string aggregationType, MetricTags tags)
        {
            Value = value;
            Timestamp = timestamp;
            Unit = unit;
            AggregationType = aggregationType;
            Tags = tags;
        }
    }

    public class MetricTags : IReadOnlyList<MetricTag>
    {
        public IEnumerator<MetricTag> GetEnumerator() =>
            throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Count { get; }
        
        public MetricTag this[int index] =>
            throw new NotImplementedException();
    }

    public class MetricTag
    {
        public readonly string Key;
        public readonly string Value;

        public MetricTag(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public interface IMetricEventSender
    {
        void Send(MetricEvent @event);
    }

    public interface IMetricContext
    {
        MetricTags Tags { get; }
        IDisposable Register(IScrapableMetric metric, TimeSpan scrapePeriod);
        void Send(MetricEvent @event);
    }

    public static class MetricContextExtensions
    {
        public static IMetric Histogram(this IMetricContext context, out IDisposable registration, string name, HistogramConfig config, TimeSpan scrapePeriod, string unit = null)
        {
            var metric = new HistogramMetric(config, context.Tags);
            registration = context.Register(metric, scrapePeriod);
            return metric;
        }

        public static ITaggedMetric2 Histogram(this IMetricContext context, out IDisposable registration, string name, HistogramConfig config, TimeSpan scrapePeriod, string key1, string key2, string unit = null)
        {
            var taggedMetric = new TaggedMetric2(tags =>
            {
                var finalTags = TagsMerger.Merge(context.Tags, name, tags);
                var metric = new HistogramMetric(config, finalTags);
                return metric;
            });
            registration = context.Register(taggedMetric, scrapePeriod);
            
            return taggedMetric;
        }
    }

    public class TagsMerger
    {
        public static MetricTags Merge(MetricTags contextTags, string name, MetricTags tags)
        {
            throw new NotImplementedException();
        }
    }

    public class TaggedMetric2 : ITaggedMetric2, IScrapableMetric
    {
        private readonly ConcurrentDictionary<MetricTags, IMetric> cache = new ConcurrentDictionary<MetricTags, IMetric>();
        private readonly Func<MetricTags, IMetric> factory;

        public TaggedMetric2(Func<MetricTags, IMetric> factory)
        {
            this.factory = factory;
        }

        public IMetric For(string value1, string value2) =>
            throw new NotImplementedException();

        public IEnumerable<MetricEvent> Scrape()
        {
            throw new NotImplementedException();
        }
    }

    internal class Timing : IMetric
    {
        private readonly IMetricContext context;

        public Timing(IMetricContext context)
        {
            this.context = context;
        }

        public void Report(double value)
        {
            // context.Send(new MetricEvent());
        }
    }
    
    internal class HistogramMetric : IScrapableMetric, IMetric
    {
        public HistogramMetric(HistogramConfig config, MetricTags contextTags) // tags are already merged
        {
        }

        public IEnumerable<MetricEvent> Scrape()
        { 
            yield break;
        }

        public void Report(double value)
        {
            
        }
    }

    public class HistogramConfig
    {
    }

    public interface IScrapableMetric
    {
        IEnumerable<MetricEvent> Scrape();
    }

    public interface IMetric
    {
        void Report(double value);
    }

    public interface ITaggedMetric1
    {
        IMetric For(string value1);
    }
    
    public interface ITaggedMetric2
    {
        IMetric For(string value1, string value2);
    }

    public interface ITaggedMetric<T>
    {
        IMetric For(T value);
    }
    
    public interface IGauge
    {
        void Set(double value);
    }
    
    public interface IHistogram
    {
        void Report(double value);
    }
    
    public interface ITiming
    {
        void Report(double value);
    }
    
    public interface ICounter
    {
        void Add(double value);
    }
    
    public interface ISummary
    {
        void Report(double value);
    }
}