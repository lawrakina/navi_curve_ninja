using UnityEngine;
using Utils.Triggers.Actions;

namespace Utils.TriggerAction.Triggers.Base
{
    public abstract class MonoTrigger : MonoBehaviour
    {
        [field: SerializeField] protected ActionBase Action { get; private set; }

        public void FireAction()
        {
            if (Action == null)
                return;
            
            Action.Fire();
        }
    }
}
