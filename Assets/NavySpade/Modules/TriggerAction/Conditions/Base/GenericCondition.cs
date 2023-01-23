namespace Misc.Conditions
{
    public abstract class GenericCondition<T> : ICondition
    {
        public abstract bool IsMet(T data);
    }
}