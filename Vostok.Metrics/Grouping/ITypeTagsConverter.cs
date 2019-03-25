using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
    [PublicAPI]
    public interface ITypeTagsConverter<TFor>
    {
        MetricTags Convert(TFor forValue);
    }
}