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
        /// Determines whether the specified lists are equal.
        /// </summary>
        /// <typeparam name="TU">The concrete type of the entities contained in the lists.</typeparam>
        /// <param name="list1">A list to compare.</param>
        /// <param name="list2">Another list to compare.</param>
        /// <returns>
        ///   <c>true</c> if the specified lists are equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public static bool NullSafeSequenceEquals<TU>(this IEnumerable<TU> list1, IEnumerable<TU> list2) where TU : NeddleObject<TU>
        {
            return ((null == list1 || null == list2) ? list1 == list2 : ((list1.Equals(list2)) | (list1.OrderBy(x => x.Id).SequenceEqual(list2.OrderBy(x => x.Id)))));
        }
    }
}
