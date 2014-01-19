using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        /// <param name="obj">The <see cref="IEnumerable"/> list.</param>
        /// <param name="separator">Separator string.</param>
        /// <returns>A string representation of the list delimeted by <b>seprator</b>.</returns>
        public static string ToString<T>(this IEnumerable<T> obj, string separator) where T : class
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(separator));

            string[] array = obj.Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        /// <summary>
        /// Returns a string for generic <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <param name="obj">The <see cref="IEnumerable"/> list.</param>
        /// <param name="separator">Separator string.</param>
        /// <returns>A string representation of the list delimeted by <b>seprator</b>.</returns>
        public static string ToString(this IEnumerable obj, string separator)
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(separator));
            
            string[] array = obj.Cast<object>().Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        /// <summary>
        /// Returns a conditional Where expression.
        /// </summary>
        /// <typeparam name="T">The type of the obj.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A where clause, iff the condition is <c>true</c>.</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> obj, bool condition, Func<T, bool> predicate)
        {
            return condition ? obj.Where(predicate) : obj;
        }

        /// <summary>
        /// Determines whether the specified lists are equal.
        /// </summary>
        /// <typeparam name="T">The concrete type of the entities contained in the lists.</typeparam>
        /// <param name="list1">A list to compare.</param>
        /// <param name="list2">Another list to compare.</param>
        /// <returns>
        ///   <c>true</c> if the specified lists are equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public static bool NullSafeSequenceEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2) where T : NeddleObject<T>
        {
            return ((null == list1 || null == list2) ? list1 == list2 : ((list1.Equals(list2)) | (list1.OrderBy(x => x.Id).SequenceEqual(list2.OrderBy(x => x.Id)))));
        }

        /// <summary>
        /// Clones the specified collection, if it is not null.
        /// </summary>
        /// <typeparam name="T">Type of the items in the collection.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>A list of cloned items or null.</returns>
        public static IEnumerable<T> NullSafeClone<T>(this IEnumerable<T> obj) where T : NeddleObject<T>, ICloneable
        {
            return obj == null ? null : obj.Select(item => (T)item.Clone());
        }
    }
}