using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags.Typed
{
    public interface ITypeTagsConverter<TFor>
    {
        MetricTags Convert(TFor forValue);
    }
}