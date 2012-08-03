using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Neddle.Extensions
{
    /// <summary>
    /// Extension methods for classes that implement the <see cref="IEnumerable"/> interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns a string for generic <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of the list's items.</typeparam>
        /// <param name="source">The <see cref="IEnumerable"/> list.</param>
        /// <param name="separator">Separator string.</param>
        /// <returns>A string representation of the list delimeted by <b>seprator</b>.</returns>
        public static string ToString<T>(this IEnumerable<T> source, string separator) where T : class
        {
            source.CheckNull("source");
            separator.CheckNullOrEmpty("separator");

            string[] array = source.Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        /// <summary>
        /// Returns a string for generic <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable"/> list.</param>
        /// <param name="separator">Separator string.</param>
        /// <returns>A string representation of the list delimeted by <b>seprator</b>.</returns>
        public static string ToString(this IEnumerable source, string separator)
        {
            source.CheckNull("source");
            separator.CheckNullOrEmpty("separator");
            
            string[] array = source.Cast<object>().Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        /// <summary>
        /// Returns a conditional Where expression.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A where clause, iff the condition is <c>true</c>.</returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Determines if the two lists are equal.
        /// </summary>
        /// <typeparam name="TU">The type of the items in the list.</typeparam>
        /// <typeparam name="TKey">The type of the key for sorting the list.</typeparam>
        /// <param name="list1">The source.</param>
        /// <param name="list2">The list to compare.</param>
        /// <param name="predicate">The predicate to calculate the sort key.</param>
        /// <returns><c>true</c> if the lists are equivalent; otherwise, false.</returns>
        public static bool Equals<TU, TKey>(IList<TU> list1, IList<TU> list2, Func<TU, TKey> predicate) where TU : class
        {
            return ((null == list1 || null == list2) ? list1 == list2 : ((list1.Equals(list2)) | (list1.OrderBy(predicate).SequenceEqual(list2.OrderBy(predicate)))));
        }
    }
}
