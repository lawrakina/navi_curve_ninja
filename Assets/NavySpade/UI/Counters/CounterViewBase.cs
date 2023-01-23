using UnityEngine;

namespace Core.UI.Counters
{
    public abstract class CounterViewBase : MonoBehaviour
    {
        [field: SerializeField] protected string Prefix { get; set; }
        [field: SerializeField] protected string Postfix { get; set; }

        public abstract void UpdateValue(int value);

        protected string PrepareValue(int value)
        {
            return Prefix + value + Postfix;
        }
    }
}