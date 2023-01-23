using UnityEngine;

namespace Utils.SO.Events.Events
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "Misc/SO Events/Bool", order = 51)]
    public class BoolEvent : GameEventBase<bool>
    {
        [SerializeField] private bool _value;

        public void Invoke() => Invoke(_value);
    }
}