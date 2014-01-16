using System;
using System.Linq;
using Neddle.Extensions;

namespace Neddle.Data.Sql.Extensions
{
    /// <summary>
    /// Contains <see cref="object"/> extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the object to its corresponsing nullable type.
        /// </summary>
        /// <typeparam name="T">The nullable type.</typeparam>
        /// <param name="obj">The <see cref="object"/>.</param>
        /// <returns>Corresponding nullable type.</returns>
        public static T ToNullableValueFromDb<T>(this object obj)
        {
            if (obj == DBNull.Value || obj == null)
            {
                return default(T);
            }

            Type paramType = typeof(T);
            if (paramType.IsNullable())
            {
                paramType = typeof(T).GetGenericArguments().First();
            }
            return (T)Convert.ChangeType(obj, paramType);
        }

        /// <summary>
        /// Converts the object to its corresponsing non-nullable value type.
        /// </summary>
        /// <typeparam name="T">The non-nullable type.</typeparam>
        /// <param name="obj">The <see cref="object"/>.</param>
        /// <returns>Corresponding non-nullable value type.</returns>
        public static T ToValueFromDb<T>(this object obj)
        {
            if (obj == DBNull.Value)
            {
                throw new ArgumentNullException("obj");
            }

            Type paramType = typeof(T);
            return (T)Convert.ChangeType(obj, paramType);
        }
    }
}