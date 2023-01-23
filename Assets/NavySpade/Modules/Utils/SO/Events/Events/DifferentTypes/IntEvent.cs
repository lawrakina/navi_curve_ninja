using UnityEngine;

namespace Utils.SO.Events.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Misc/SO Events/Int", order = 51)]
    public class IntEvent : GameEventBase<int>
    {
        [SerializeField] private int _value;

        public void Invoke() => Invoke(_value);
    }
}