using Utils.TriggerAction.Triggers.Physical.Abstract;

namespace Utils.TriggerAction.Triggers.Physical.Collision
{
    /// <summary>
    /// When an object stays the trigger, fires off the associated action.
    /// </summary>
    public class CollisionStayTrigger : PhysicalTrigger
    {
        private void OnCollisionStay(UnityEngine.Collision other)
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