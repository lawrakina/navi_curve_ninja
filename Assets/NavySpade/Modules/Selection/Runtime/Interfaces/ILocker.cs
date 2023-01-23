using JetBrains.Annotations;

namespace NavySpade.Modules.Selection.Runtime.Interfaces
{
    public interface ILocker
    {
        [PublicAPI]
        void Lock();

        [PublicAPI]
        void Unlock();
    }
}