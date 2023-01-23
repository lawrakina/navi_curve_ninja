using System;
using System.Collections.Generic;
using System.Linq;
using NavySpade.Modules.Extensions.Exceptions;

namespace NavySpade.Modules.Extensions.CsharpTypes
{
    /// <summary>
    /// <see cref="Enum"/> extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// TryParses the string to enum of type <typeparamref name="T"/>.
        /// </summary>
        public static bool TryParse<T>(this string name, out T result, bool ignoreCase = false) where T : struct
        {
            return Enum.TryParse(name, ignoreCase, out result);
        }

        /// <summary>
        /// Parses the string to enum of type <typeparamref name="T"/>.
        /// </summary>
        public static T Parse<T>(this string name, bool ignoreCase = false) where T : struct
        {
            return (T)Enum.Parse(typeof(T), name, ignoreCase);
        }

        /// <summary>
        /// Gets values for type <typeparamref name="T"/> enum.
        /// </summary>
        public static T[] GetEnumValues<T>() where T : struct
        {
            return EnumInternal<T>.ValueToNameMap.Select(x => x.Key).ToArray();
        }

        /// <summary>
        /// Gets names (strings) for type <typeparamref name="T"/> enum.
        /// </summary>
        public static string[] GetEnumNames<T>() where T : struct
        {
            return EnumInternal<T>.NameToValueMap.Select(x => x.Key).ToArray();
        }

        /// <summary>
        /// Gets enum name (string) -> description (string) map for
        /// type <typeparamref name="T"/> enum.
        /// </summary>
        public static IDictionary<string, string> GetEnumNameToDescriptionMap<T>() where T : struct
        {
            return EnumInternal<T>.ValueToDescriptionMap
                .ToDictionary(x => x.Key.GetEnumName(), x => x.Value).AsReadOnly();
        }

        /// <summary>
        /// Gets the name (string) for the enum value or throws exception if not exists.
        /// </summary>
        public static string GetEnumName<T>(this T enumValue) where T : struct
        {
            if (EnumInternal<T>.ValueToNameMap.TryGetValue(enumValue, out var enumName))
            {
                return enumName;
            }

            throw new UnsupportedEnumValueException<T>(enumValue);
        }

        /// <summary>
        /// Gets enum name (string) for description or throws exception if not exists.
        /// </summary>
        public static string GetEnumNameFromDescription<T>(this string description) where T : struct
        {
            if (EnumInternal<T>.DescriptionToValueMap.TryGetValue(description, out var enumValue))
            {
                return enumValue.GetEnumName();
            }

            throw new UnsupportedEnumValueException<T>(
                $"{nameof(description)} {description} of enum {typeof(T).Name} is not supported.");
        }

        /// <summary>
        /// Gets the value for the enum name (string) or throws exception if not exists.
        /// </summary>
        public static T GetEnumValue<T>(this string name) where T : struct
        {
            if (EnumInternal<T>.NameToValueMap.TryGetValue(name, out var enumValue))
            {
                return enumValue;
            }

            throw new UnsupportedEnumValueException<T>(
                $"{nameof(name)} {name} of enum {typeof(T).Name} is not supported.");
        }

        /// <summary>
        /// Gets the value for the enum description (string) or throws exception if not exists.
        /// </summary>
        public static T GetEnumValueFromDescription<T>(this string description) where T : struct
        {
            if (EnumInternal<T>.DescriptionToValueMap.TryGetValue(description, out var enumValue))
            {
                return enumValue;
            }

            throw new UnsupportedEnumValueException<T>(
                $"{nameof(description)} {description} of enum {typeof(T).Name} is not supported.");
        }

        /// <summary>
        /// Gets the description (DescriptionAttribute) for the enum value
        /// or throws exception if not exists.
        /// </summary>
        public static string GetDescription<T>(this T enumValue) where T : struct
        {
            if (EnumInternal<T>.ValueToDescriptionMap.TryGetValue(enumValue, out var description))
            {
                return description;
            }

            throw new UnsupportedEnumValueException<T>(enumValue);
        }

        /// <summary>
        /// Generic method for <see cref="Enum.IsDefined(Type, object)"/>.
        /// </summary>
        public static bool IsDefined<T>(this int value) where T : struct
        {
            // avoid Enum.IsDefined(typeof(T), value) due to boxing
            return EnumInternal<T>.ValueIntToValueMap.ContainsKey(value);
        }
    }
}