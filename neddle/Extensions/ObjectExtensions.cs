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

        /// <summary>
        /// Determines if the two objects are equivalent in a way that will not throw if the current object is null.
        /// </summary>
        /// <param name="obj1">The current object.</param>
        /// <param name="obj2">The object to compare.</param>
        /// <returns><c>true</c> if the two objects are equivalent; otherwise, <c>false</c>.</returns>
        public static bool NullSafeEquals(this object obj1, object obj2)
        {
            return null != obj1 ? obj1.Equals(obj2) : (null == obj2);
        }

        /// <summary>
        /// Calculates a hash for the object in a way that will not throw or influence the value if the current object is null.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="seed">The seed.</param>
        /// <returns>The hash.</returns>
        public static int NullSafeHash(this object obj, int seed)
        {
            return null != obj ? seed * 7 + obj.GetHashCode() : seed;   
        }

        /// <summary>
        /// Calculates a hash for the object using reflection.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>A hash.</returns>
        public static int CalculateHash(this object obj)
        {
            unchecked
            {
                PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public);
                return props.Aggregate(3, (current, propertyInfo) => propertyInfo.GetValue(obj, null).NullSafeHash(current));
            }
        }
    }
}