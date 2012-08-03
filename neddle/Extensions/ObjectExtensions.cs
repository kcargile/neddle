using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace Neddle.Extensions
{
    /// <summary>
    /// Contains <see cref="object"/> extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks for and throws an <see cref="ArgumentNullException"/> if the object is null.
        /// </summary>
        /// <param name="param">The <see cref="object"/>.</param>
        /// <exception cref="ArgumentNullException"><b>parm</b> was null.</exception>
        public static void CheckNull(this object param)
        {
            param.CheckNull(null);
        }

        /// <summary>
        /// Checks for and throws an <see cref="ArgumentNullException"/> if the object is null.
        /// </summary>
        /// <param name="param">The <see cref="object"/>.</param>
        /// <param name="parmName">Name of the parameter to check.</param>
        /// <exception cref="ArgumentNullException"><b>param</b> was null.</exception>
        public static void CheckNull(this object param, string parmName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(parmName);
            }
        }

        /// <summary>
        /// Gets the <see cref="Enum"/> description, if it exists.
        /// </summary>
        /// <param name="enumeration">The <see cref="Enum"/>.</param>
        /// <returns>The enumeration's description.</returns>
        public static string GetDescription(this Enum enumeration)
        {
            FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : enumeration.ToString();
        }

        /// <summary>
        /// Gets the <see cref="Enum"/> as a list.
        /// </summary>
        /// <param name="enumerationType"><see cref="Type"/> of the enumeration.</param>
        /// <returns>A list containing the enumeration's values.</returns>
        public static List<KeyValuePair<Enum, string>> GetEnumList(this Type enumerationType)
        {
            if (enumerationType == null)
            {
                throw new ArgumentNullException("enumerationType");
            }

            Array enumValues = Enum.GetValues(enumerationType);
            return (from Enum value in enumValues select new KeyValuePair<Enum, string>(value, GetDescription(value))).ToList();
        }

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