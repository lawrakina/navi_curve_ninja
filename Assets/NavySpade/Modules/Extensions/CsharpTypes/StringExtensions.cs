using System;
using System.Text;

namespace NavySpade.Modules.Extensions.CsharpTypes
{
    /// <summary>
    /// <see cref="string"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        #region Syntax

        /// <summary>
        /// Returns true if string is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this string value, bool trimSpaces = true)
        {
            if (value == null)
            {
                return true;
            }

            return trimSpaces == false ? value.Length == 0 : value.Trim().Length == 0;
        }

        /// <summary>
        /// Returns true if string is null, empty or only whitespaces.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static bool IsNotNullAndNotEmpty(this string value)
            => string.IsNullOrEmpty(value) == false;

        public static bool IsNotNullAndNotWhiteSpace(this string value)
            => string.IsNullOrWhiteSpace(value) == false;

        /// <summary>
        /// Returns specified value if string is null/empty/whitespace else same string.
        /// </summary>
        public static string Or(this string value, string or)
            => value.IsNullOrWhiteSpace() == false ? value : or;

        /// <summary>
        /// Returns empty if string is null/empty/whitespace else same string.
        /// </summary>
        public static string OrEmpty(this string value) => value.Or("");

        /// <summary>
        /// Returns null if string is null/empty else same string.
        /// </summary>
        public static string NullIfEmpty(this string value)
            => value.IsNullOrEmpty() == false ? value : null;

        /// <summary>
        /// Returns null if string is null/empty/whitespace else same string.
        /// </summary>
        public static string NullIfWhiteSpace(this string value)
            => value.IsNullOrWhiteSpace() == false ? value : null;

        #endregion

        public static int CountInstancesOf(this string source, string substring)
        {
            var removedInstancesLength = source.Replace(substring, string.Empty).Length;
            return (source.Length - removedInstancesLength) / substring.Length;
        }

        public static int CountInstancesOf(this string source, char @char)
        {
            var removedInstancesLength = source.Replace(@char.ToString(), string.Empty).Length;
            return source.Length - removedInstancesLength;
        }

        /// <summary>
        /// Like <see cref="string.Replace(string, string)"/> but it only replaces the first instance
        /// </summary>
        public static string ReplaceFirst(this string text, string search, string replace, int startIndex = 0)
        {
            var index = text.IndexOf(search, startIndex);
            if (index < 0)
            {
                return text;
            }

            return text.Substring(0, index) + replace + text.Substring(index + search.Length);
        }

        /// <summary>
        /// Replace any characters from a list with a given character.
        /// </summary>
        public static string ReplaceAny(this string text, char[] search, char replace, int startIndex = 0)
        {
            var builder = new StringBuilder(text);
            builder.ReplaceAny(search, replace, startIndex);
            return builder.ToString();
        }

        /// <summary>
        /// Get a section of text.
        /// </summary>
        public static string Snip(this string text, int startIndex, int endIndex)
        {
            if (startIndex < 0)
            {
                throw new ArgumentException($"{nameof(startIndex)} must not be less than 0");
            }

            if (endIndex < 0)
            {
                throw new ArgumentException($"{nameof(endIndex)} must not be less than 0");
            }

            if (endIndex < startIndex)
            {
                throw new ArgumentException($"{nameof(endIndex)} must not be less than {nameof(startIndex)}");
            }

            if (startIndex >= text.Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} is outside the range of the string!");
            }

            if (endIndex >= text.Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(endIndex)} is outside the range of the string!");
            }

            var length = endIndex - startIndex + 1;
            return text.Substring(startIndex, length);
        }
    }
}