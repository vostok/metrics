
using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
	internal class MetricGroup<TMetric> : MetricGroupBase<TMetric>,
		IMetricGroup1<TMetric>,
		IMetricGroup2<TMetric>,
		IMetricGroup3<TMetric>,
		IMetricGroup4<TMetric>,
		IMetricGroup5<TMetric>,
		IMetricGroup6<TMetric>,
		IMetricGroup7<TMetric>,
		IMetricGroup8<TMetric>
    {
		private readonly string[] keys;
		
        public MetricGroup([NotNull] Func<MetricTags, TMetric> factory, [NotNull] params string[] keys)
            : base(factory)
        {
            this.keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }
		
		public TMetric For(string value1)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	            tags = tags.Append(keys[3], value4);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	            tags = tags.Append(keys[3], value4);
	            tags = tags.Append(keys[4], value5);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	            tags = tags.Append(keys[3], value4);
	            tags = tags.Append(keys[4], value5);
	            tags = tags.Append(keys[5], value6);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	            tags = tags.Append(keys[3], value4);
	            tags = tags.Append(keys[4], value5);
	            tags = tags.Append(keys[5], value6);
	            tags = tags.Append(keys[6], value7);
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(keys[0], value1);
	            tags = tags.Append(keys[1], value2);
	            tags = tags.Append(keys[2], value3);
	            tags = tags.Append(keys[3], value4);
	            tags = tags.Append(keys[4], value5);
	            tags = tags.Append(keys[5], value6);
	            tags = tags.Append(keys[6], value7);
	            tags = tags.Append(keys[7], value8);
	  
            return For(tags); 
		}
    }
}