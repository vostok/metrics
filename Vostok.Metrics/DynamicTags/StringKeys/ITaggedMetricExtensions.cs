
using JetBrains.Annotations;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
    [PublicAPI]
    public static partial class ITaggedMetricExtensions
    {
        public static TMetric For<TMetric, TValue1>(this ITaggedMetric1<TMetric> metric, TValue1 value1)
        {
            return metric.For(value1.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2>(this ITaggedMetric2<TMetric> metric, TValue1 value1, TValue2 value2)
        {
            return metric.For(value1.ToString(), value2.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3>(this ITaggedMetric3<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3, TValue4>(this ITaggedMetric4<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3, TValue4, TValue5>(this ITaggedMetric5<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(this ITaggedMetric6<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5, TValue6 value6)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString(), value6.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(this ITaggedMetric7<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5, TValue6 value6, TValue7 value7)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString(), value6.ToString(), value7.ToString());
        }
        public static TMetric For<TMetric, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8>(this ITaggedMetric8<TMetric> metric, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5, TValue6 value6, TValue7 value7, TValue8 value8)
        {
            return metric.For(value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString(), value6.ToString(), value7.ToString(), value8.ToString());
        }
    }
}