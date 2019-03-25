using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
    internal class TypeTagsConverter<TFor> : ITypeTagsConverter<TFor>
    {
        public static readonly ITypeTagsConverter<TFor> Default = new TypeTagsConverter<TFor>();
        
        public MetricTags Convert(TFor forValue)
        {
            throw new System.NotImplementedException();
        }
    }
}