using UnityEngine;
using Utils.TriggerAction.Triggers.Physical.Abstract;

namespace Utils.TriggerAction.Triggers.Physical.ColliderTrigger
{
    [RequireComponent(typeof(Collider))]
    public class ColliderTriggerTrigger : PhysicalTrigger
    {
        protected Collider Collider
        {
            get
            {
                if (_collider != null)
                    return _collider;

                _collider = GetComponent<Collider>();
                _collider.isTrigger = true;

                return _collider;
            }
        }

        private Collider _collider;

        public override void Enable()
        {
            Collider.enabled = true;
        }

        public override void Disable()
        {
            Collider.enabled = false;
        }
    }
}