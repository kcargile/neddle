using System;

namespace Neddle.Extensions
{
    /// <summary>
    /// Contains <see cref="DateTime"/> extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines if the two dates are equivalent to within one second.
        /// </summary>
        /// <param name="t">The <see cref="DateTime"/> object.</param>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the two dates are approximately equal; otherwise, false.</returns>
        public static bool ApproximatelyEqual(this DateTime t, DateTime obj)
        {
            return Math.Abs((t - obj).TotalSeconds) < 1;
        }

        /// <summary>
        /// Determines if the two dates are equivalent to within one second.
        /// </summary>
        /// <param name="t">The <see cref="DateTime"/> object.</param>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the two dates are approximately equal; otherwise, false.</returns>
        public static bool ApproximatelyEqual(this DateTime? t, DateTime? obj)
        {
            if (!t.HasValue && !obj.HasValue)
            {
                return true;
            }

            if ((t.HasValue && !obj.HasValue) || !t.HasValue)
            {
                return false;
            }

            return Math.Abs((t.Value - obj.Value).TotalSeconds) < 1;
        }
    }
}