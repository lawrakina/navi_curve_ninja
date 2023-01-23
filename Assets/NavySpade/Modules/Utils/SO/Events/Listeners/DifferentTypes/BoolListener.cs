using UnityEngine.Events;
using Utils.SO.Events.Events;

namespace Utils.SO.Events.Listeners
{
    public class BoolListener : GameEventListenerBase<bool, BoolEvent, UnityEvent<bool>>
    {
    }
}