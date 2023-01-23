using System;

namespace NavySpade.Modules.Extensions.Syntax
{
    public static class SyntaxExtensions
    {
        public static TOut With<TIn, TOut>(this TIn self, Func<TIn, TOut> function, TOut failValue = null)
            where TIn : class
            where TOut : class
        {
            return self == null ? failValue : function(self);
        }

        public static TOut Try<TIn, TOut>(this TIn self, Func<TIn, TOut> function, TOut failValue = default)
            where TIn : class
        {
            if (self == null)
            {
                return failValue;
            }

            try
            {
                return function(self);
            }
            catch
            {
                return failValue;
            }
        }
    }
}