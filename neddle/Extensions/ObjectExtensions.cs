using System.Linq;
using System.Reflection;

namespace Neddle.Extensions
{
    /// <summary>
    /// Contains <see cref="object"/> extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
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