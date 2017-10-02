using System.Collections.Generic;
using System.Linq;

namespace ArmorOptimizer.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IList<T> ToEnumeratedList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null ? null : enumerable as IList<T> ?? enumerable.ToList();
        }
    }
}