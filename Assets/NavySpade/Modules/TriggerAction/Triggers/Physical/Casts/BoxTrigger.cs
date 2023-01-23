using UnityEngine;
using Utils.TriggerAction.Triggers.Physical.Abstract;

namespace Utils.TriggerAction.Triggers.Physical.Casts
{
    public class BoxTrigger : PhysicalTrigger
    {
        [SerializeField] private Vector3 _size;

        private void FixedUpdate()
        {
            var halfExtents = _size / 2f;
            var innerColliders = Physics.OverlapBox(transform.position, halfExtents);

            foreach (var collider in innerColliders)
            {
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        public override void Enable()
        {
            
        }

        public override void Disable()
        {
            
        }
    }
}