using UnityEngine;

namespace Utils.SO.Events.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Misc/SO Events/Void", order = 51)]
    public class VoidEvent : GameEventBase<Void>
    {
        public void Invoke() => Invoke(new Void());
    }
}
