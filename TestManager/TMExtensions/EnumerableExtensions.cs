using System;
using System.Collections.Generic;

namespace TMExtensions
{
    /// <summary>
    /// Расширения Enumerable<T>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Рекурсивное получение иерархических данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Где искать</param>
        /// <param name="selector">Что искать</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in SelectRecursive(children, selector))
                    yield return child;
            }
        }
    }
}
