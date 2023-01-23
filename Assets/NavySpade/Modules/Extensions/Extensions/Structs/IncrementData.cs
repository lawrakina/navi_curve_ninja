using System;

namespace Core.Extensions.Structs
{
    public abstract class IncrementData<T> where T : IEquatable<T>
    {
        public T DefaultValue;
        public T Increment;
    }

    public class IntIncrement : IncrementData<int>
    {
    }

    public class FloatIncrement : IncrementData<float>
    {
    }
}