using NavySpade.Modules.Selection.Runtime.Interfaces;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Abstract
{
    public abstract class UILocker : MonoBehaviour, ILocker
    {
        public abstract void Lock();
        public abstract void Unlock();
    }
}