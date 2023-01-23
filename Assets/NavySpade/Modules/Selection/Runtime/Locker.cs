using NavySpade.Modules.Selection.Runtime.Abstract;
using NavySpade.Modules.Selection.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.Modules.Selection.Runtime
{
    public class Locker : MonoBehaviour, ILocker
    {
        [SerializeField] private UnityEvent _onLock;
        [SerializeField] private UnityEvent _onUnlock;
        
        public void Lock()
        {
            _onLock.Invoke();
        }

        public void Unlock()
        {
            _onUnlock.Invoke();
        }
    }
}