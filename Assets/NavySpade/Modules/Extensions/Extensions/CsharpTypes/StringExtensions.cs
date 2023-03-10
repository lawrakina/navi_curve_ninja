using System;
using System.Text;

namespace Core.Extensions.CsharpTypes
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
            => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhiteSpace(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static bool IsNotNullAndNotEmpty(this string value)
            => !string.IsNullOrEmpty(value);

        public static bool IsNotNullAndNotWhiteSpace(this string value)
            => !string.IsNullOrWhiteSpace(value);


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
        /// Get a section of text
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