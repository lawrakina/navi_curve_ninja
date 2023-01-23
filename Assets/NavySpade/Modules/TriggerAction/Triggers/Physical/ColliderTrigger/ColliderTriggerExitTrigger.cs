using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.ColliderTrigger
{
    /// <summary>
    /// When an object exits the trigger, fires off the associated action.
    /// </summary>
    public class ColliderTriggerExitTrigger : ColliderTriggerTrigger
    {
        private void OnTriggerExit(Collider other)
        {
            Process(other);
        }
    }
}