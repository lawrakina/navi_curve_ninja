using UnityEngine;
using UnityEngine.Events;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that can be used to call nearly anything.
    /// Easy to overuse.
    /// </summary>
    public class SendUnityEventAction : ActionBase
    {
        [SerializeField] private UnityEvent _onFire = new UnityEvent();

        public override void Fire()
        {
            _onFire.Invoke();
        }
    }
}
