using System;
using System.Diagnostics.Contracts;

namespace Neddle.Extensions
{
    /// <summary>
    /// Contains <see cref="Type"/> extension methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="t">The <see cref="Type"/>.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullable(this Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        /// Determines whether the specified type is a subclass of the specified generic type.
        /// </summary>
        /// <param name="t">The <see cref="Type"/>.</param>
        /// <param name="generic">The generic to compare to.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is a generic subtype; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSubclassOfRawGeneric(this Type t, Type generic)
        {
            Contract.Requires<ArgumentNullException>(t != null);
            Contract.Requires<ArgumentNullException>(generic != null);

            if (t.IsInterface || t.IsValueType)
            {
                return false;
            }

            while (t != typeof(object))
            {
                var cur = t.IsGenericType ? t.GetGenericTypeDefinition() : t;
                if (cur.IsGenericType && generic.GetGenericTypeDefinition() == cur.GetGenericTypeDefinition())
                {
                    return true;
                }
                t = t.BaseType;
            }
            return false;
        }
    }
}