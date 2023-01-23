using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.ColliderTrigger
{
    /// <summary>
    /// When an object enters/exits the trigger, fires off the associated action.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ColliderTriggerEnterTrigger : ColliderTriggerTrigger
    {
        private void Awake()
        {
            Collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Process(other);
        }
    }
}