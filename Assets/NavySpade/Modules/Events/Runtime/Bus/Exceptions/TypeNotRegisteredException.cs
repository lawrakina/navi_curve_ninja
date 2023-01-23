using System;
using System.Runtime.Serialization;

namespace EventSystem.Runtime.Bus.Exceptions
{
    /// <summary>
    /// Is thrown when no handler is found for particular message
    /// </summary>
    [Serializable]
    public class TypeNotRegisteredException : Exception
    {
        public Type NonRegisteredType { get; }

        public TypeNotRegisteredException()
        {
        }

        public TypeNotRegisteredException(Type type, string message) : base(message)
        {
            NonRegisteredType = type;
        }

        public TypeNotRegisteredException(Type type, string message, Exception inner) : base(message, inner)
        {
            NonRegisteredType = type;
        }

        public TypeNotRegisteredException(string message) : base(message)
        {
        }

        public TypeNotRegisteredException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TypeNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}