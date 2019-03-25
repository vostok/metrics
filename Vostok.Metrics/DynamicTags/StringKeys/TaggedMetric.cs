
using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
	internal class TaggedMetric<TMetric> : TaggedMetricBase<TMetric>,
		ITaggedMetric1<TMetric>,
		ITaggedMetric2<TMetric>,
		ITaggedMetric3<TMetric>,
		ITaggedMetric4<TMetric>,
		ITaggedMetric5<TMetric>,
		ITaggedMetric6<TMetric>,
		ITaggedMetric7<TMetric>,
		ITaggedMetric8<TMetric>
    {
		private readonly string[] keys;
		
        public TaggedMetric(Func<MetricTags, TMetric> factory, params string[] keys)
            : base(factory)
        {
            this.keys = keys;
        }
		
		public TMetric For(string value1)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	            tags = tags.Append(new MetricTag(keys[3], value4));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	            tags = tags.Append(new MetricTag(keys[3], value4));
	            tags = tags.Append(new MetricTag(keys[4], value5));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	            tags = tags.Append(new MetricTag(keys[3], value4));
	            tags = tags.Append(new MetricTag(keys[4], value5));
	            tags = tags.Append(new MetricTag(keys[5], value6));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	            tags = tags.Append(new MetricTag(keys[3], value4));
	            tags = tags.Append(new MetricTag(keys[4], value5));
	            tags = tags.Append(new MetricTag(keys[5], value6));
	            tags = tags.Append(new MetricTag(keys[6], value7));
	  
            return For(tags); 
		}
		
		public TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
		{
			var tags = MetricTags.Empty;
	            tags = tags.Append(new MetricTag(keys[0], value1));
	            tags = tags.Append(new MetricTag(keys[1], value2));
	            tags = tags.Append(new MetricTag(keys[2], value3));
	            tags = tags.Append(new MetricTag(keys[3], value4));
	            tags = tags.Append(new MetricTag(keys[4], value5));
	            tags = tags.Append(new MetricTag(keys[5], value6));
	            tags = tags.Append(new MetricTag(keys[6], value7));
	            tags = tags.Append(new MetricTag(keys[7], value8));
	  
            return For(tags); 
		}
    }
}