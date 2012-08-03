using System;
using System.Security.Cryptography;
using System.Text;
using Neddle.Properties;

namespace Neddle.Extensions
{
    /// <summary>
    /// Contains <see cref="string"/> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks for a null or empty string and optionally trims the string before testing.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns><c>true</c> if the string is null or emtpy; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmptyTrimmed(this string s)
        {
            return String.IsNullOrEmpty((s != null) ? s.Trim() : s);
        }

        /// <summary>
        /// Checks for a null or empty string and optionally trims the string before testing.
        /// </summary>
        /// <param name="param">The string.</param>
        /// <exception cref="ArgumentNullException"><b>param</b> was null.</exception>
        public static void CheckNullOrEmpty(this string param)
        {
            param.CheckNullOrEmpty(null);
        }

        /// <summary>
        /// Checks parameter for null and throws <code>ArgumentNullException</code> if null.
        /// </summary>
        /// <param name="param">The string.</param>
        /// <param name="paramName">The name of the parameter to check.</param>
        /// <exception cref="ArgumentNullException"><b>param</b> was null.</exception>
        /// <exception cref="ArgumentException"><b>param</b> was empty.</exception>
        public static void CheckNullOrEmpty(this string param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.IsNullOrEmptyTrimmed())
            {
                throw new ArgumentException(Messages.ex_valueCannotBeEmpty, paramName);
            }
        }

        /// <summary>
        /// Creates an MD5 hash of the specified string.
        /// </summary>
        /// <param name="valueToHash">The value to hash.</param>
        /// <returns>An MD5 hash or an empty string if <b>valueToHash</b> was empty.</returns>
        public static string Md5Hash(this string valueToHash)
        {
            if (string.IsNullOrEmpty(valueToHash))
            {
                return string.Empty;
            }

            var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(valueToHash);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
