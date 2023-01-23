using System;

namespace NavySpade.Modules.Extensions.Exceptions
{
    /// <summary>
    /// Exception helper class.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Throws NullReferenceException if true with message.
        /// </summary>
        public static void ThrowIfNull(bool throwEx, string exMessage)
        {
            if (throwEx)
            {
                throw new NullReferenceException(exMessage);
            }
        }

        /// <summary>
        /// Throws ArgumentNullException if true with message.
        /// </summary>
        public static void ThrowIfArgumentNull(bool throwEx, string exMessage)
        {
            if (throwEx)
            {
                throw new ArgumentNullException(exMessage);
            }
        }

        /// <summary>
        /// Throws EmptyException if true with message.
        /// </summary>
        public static void ThrowIfEmpty(bool throwEx, string exMessage)
        {
            if (throwEx)
            {
                throw new EmptyException(exMessage);
            }
        }

        /// <summary>
        /// Throws ArgumentOutOfRangeException if true with message.
        /// </summary>
        public static void ThrowIfArgumentOutOfRange(bool throwEx, string exMessage)
        {
            if (throwEx)
            {
                throw new ArgumentOutOfRangeException(exMessage);
            }
        }
    }
}