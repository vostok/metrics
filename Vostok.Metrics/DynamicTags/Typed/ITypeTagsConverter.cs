using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags.Typed
{
    [PublicAPI]
    public interface ITypeTagsConverter<TFor>
    {
        MetricTags Convert(TFor forValue);
    }
}