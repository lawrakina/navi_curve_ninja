namespace Core.Meta.Unlocks.Conditions
{
    public interface IUnlockCondition
    {
        bool IsMet();
        void Process();
        void Clear();
    }
}