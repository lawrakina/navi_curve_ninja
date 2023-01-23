using UnityEngine;
using UnityEngine.Events;
using Utils.SO.Events.Events;

namespace Utils.SO.Events.Listeners
{
    public class GameEventListenerBase<T, E, UER> : MonoBehaviour, IGameEventListener<T>
        where E : GameEventBase<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E _gameEvent;
        [SerializeField] private UER _unityEventResponse;

        private void OnEnable()
        {
            if (_gameEvent == null)
                return;

            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (_gameEvent == null)
                return;

            _gameEvent.UnregisterListener(this);
        }

        public void OnEventInvoked(T item)
        {
            _unityEventResponse?.Invoke(item);
        }
    }
}