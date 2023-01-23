using UnityEngine;
using Utils.TriggerAction.Triggers.Base;

namespace Utils.TriggerAction
{
    public class TriggerGroup : MonoBehaviour
    {
        [SerializeField] private MonoTrigger[] _triggers;

        public void FireAll()
        {
            foreach (var trigger in _triggers)
            {
                trigger.FireAction();
            }
        }
        
        public void Reset()
        {
            _triggers = GetComponentsInChildren<MonoTrigger>();
        }
    }
}