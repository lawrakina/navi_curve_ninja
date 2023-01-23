using UnityEngine;
using Utils.SO.Events.Events;

namespace Utils.SO.Events.Senders
{
    public abstract class GameEventSender<T, TE> : MonoBehaviour, IGameEventSender<T> where TE: GameEventBase<T>
    {
        [SerializeField] private TE _event;

        public void Send(T item)
        {
            _event.Invoke(item);
        }
    }
}