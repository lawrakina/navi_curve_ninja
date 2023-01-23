using Utils.TriggerAction.Triggers.Physical.Abstract;

namespace Utils.TriggerAction.Triggers.Physical.Collision
{
    /// <summary>
    /// When the object starts collision, fires off the associated action.
    /// </summary>
    public class CollisionEnterTrigger : PhysicalTrigger
    {
        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (enabled == false)
                return;

            Process(other.collider);
        }

        public override void Enable()
        {
            enabled = true;
        }

        public override void Disable()
        {
            enabled = false;
        }
    }
}