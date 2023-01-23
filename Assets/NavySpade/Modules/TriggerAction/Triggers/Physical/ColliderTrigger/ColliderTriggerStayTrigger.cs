using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.ColliderTrigger
{
    /// <summary>
    /// When an object stays the trigger, fires off the associated action.
    /// </summary>
    public class ColliderTriggerStayTrigger : ColliderTriggerTrigger
    {
        private void OnTriggerStay(Collider other)
        {
            Process(other);
        }
    }
}