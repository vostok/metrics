using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags.Typed
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