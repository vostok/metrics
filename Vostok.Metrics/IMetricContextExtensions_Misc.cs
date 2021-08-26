using System;
using JetBrains.Annotations;

namespace Vostok.Metrics
{
    internal static class IMetricContextExtensions_Misc
    {
        [NotNull]
        public static IAnnotationContext AsAnnotationContext([NotNull] this IMetricContext context)
            => context as IAnnotationContext ?? throw new NotSupportedException($"This metric context of type '{context.GetType().Name}' does not support annotations.");
    }
}
