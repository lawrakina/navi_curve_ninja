using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq.Expressions;

namespace NavySpade.Modules.Extensions.UnityTypes
{
    public static class LanguageTools
    {
        private static readonly ConcurrentDictionary<Type, object> Cache;

        static LanguageTools()
        {
            Cache = new ConcurrentDictionary<Type, object>();
        }

        public static bool IsFlagSet<T>(this T value, T flag) where T : struct, IComparable, IFormattable, IConvertible
        {
            Func<T, T, bool> function;
            if (Cache.TryGetValue(typeof(T), out var maybeFn) == false)
            {
                function = GenerateHasFlag<T>();
                Cache[typeof(T)] = function;
            }
            else if (maybeFn is Func<T, T, bool> function2)
            {
                function = function2;
            }
            else
            {
                var valueLong = value.ToInt64(NumberFormatInfo.CurrentInfo);
                var flagLong = flag.ToInt64(NumberFormatInfo.CurrentInfo);
                return (valueLong & flagLong) == flagLong;
            }

            return function.Invoke(value, flag);
        }

#if !ENABLE_IL2CPP
        private static Func<T, T, bool> GenerateHasFlag<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            var value = Expression.Parameter(typeof(T));
            var flag = Expression.Parameter(typeof(T));

            // Convert from Enum to underlying type (byte, int, long, ...) to allow bitwise functions to work.
            var valueConverted = Expression.Convert(value, Enum.GetUnderlyingType(typeof(T)));
            var flagConverted = Expression.Convert(flag, Enum.GetUnderlyingType(typeof(T)));

            // (Value & Flag)
            var bitwiseAnd = Expression.MakeBinary(ExpressionType.And, valueConverted, flagConverted);

            // (Value & Flag) == Flag
            var hasFlagExpression = Expression.MakeBinary(ExpressionType.Equal, bitwiseAnd, flagConverted);

            return Expression.Lambda<Func<T, T, bool>>(hasFlagExpression, value, flag)
                .Compile();
        }
#else
        private static Func<T, T, bool> GenerateHasFlag<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            return (value, flag) =>
            {
                var valueLong = value.ToInt64(NumberFormatInfo.CurrentInfo);
                var flagLong = flag.ToInt64(NumberFormatInfo.CurrentInfo);
                return (valueLong & flagLong) == flagLong;
            };
        }
#endif
    }
}