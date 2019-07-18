using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Vostok.Commons.Collections;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Grouping
{
    internal static class MetricValueExtractor
    {
        private const int CacheCapacity = 100;

        private static readonly RecyclingBoundedCache<Type, Func<object, object>> Cache =
            new RecyclingBoundedCache<Type, Func<object, object>>(CacheCapacity);
        
        public static double? ExtractValue(object item)
        {
            var property = Cache.Obtain(item.GetType(), LocateProperty);

            return ObtainPropertyValue(item, property);
        }

        private static Func<object, object> LocateProperty(Type type)
        {
            try
            {
                var property = type
                    .GetTypeInfo()
                    .DeclaredProperties
                    .Where(p => p.CanRead)
                    .Where(p => p.GetMethod.IsPublic)
                    .Where(p => !p.GetMethod.IsStatic)
                    .Where(p => !p.GetIndexParameters().Any())
                    .Single(p => p.GetCustomAttribute<MetricValueAttribute>() != null);

                var parameter = Expression.Parameter(typeof(object));
                var convertedParameter = Expression.Convert(parameter, type);

                var propertyExpression = Expression.Property(convertedParameter, property);
                var convertedProperty = Expression.Convert(propertyExpression, typeof(object));

                return Expression.Lambda<Func<object, object>>(convertedProperty, parameter).Compile();
            }
            catch
            {
                return null;
            }
        }

        private static double? ObtainPropertyValue(object item, Func<object, object> getter)
        {
            try
            {
                var value = getter?.Invoke(item);

                return value == null ? (double?)null : Convert.ToDouble(value);
            }
            catch
            {
                return null;
            }
        }
    }
}