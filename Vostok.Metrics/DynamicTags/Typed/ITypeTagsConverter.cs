using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags.Typed
{
    public interface ITypeTagsConverter<TFor>
    {
        MetricTags Convert(TFor forValue);
    }
}