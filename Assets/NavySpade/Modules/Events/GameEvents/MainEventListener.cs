using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem.GameEvents
{
    public class MainEventListener : MonoBehaviour
    {
        [SerializeField] private MainEnumEvent _event;
        [SerializeField] private UnityEvent _callback;

        private readonly EventDisposal _disposal = new EventDisposal();
        
        private void OnEnable()
        {
            EventManager.Add(_event, () => _callback.Invoke()).AddTo(_disposal);
        }

        private void OnDisable()
        {
            _disposal.Dispose();
        }
    }
}
