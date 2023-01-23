using UnityEngine;
using UnityEngine.Events;

namespace Misc.Physic
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private float _mass;
        [SerializeField] private Rigidbody[] _rbs;
        [SerializeField] private Rigidbody _centerBody;
        
        [SerializeField] private Collider[] _colliders;

        [SerializeField] private UnityEvent _onEnableRagdoll;
        [SerializeField] private UnityEvent _onDisableRagdoll;

        public Rigidbody CenterBody => _centerBody;

        public bool IsRagdollActive
        {
            set
            {
                foreach (var rb in _rbs)
                {
                    rb.isKinematic = value == false;
                    rb.mass = _mass / _rbs.Length;
                }

                foreach (var collider1 in _colliders)
                {
                    collider1.enabled = value;
                }

                if (value)
                    _onEnableRagdoll.Invoke();
                else
                    _onDisableRagdoll.Invoke();
            }
        }

        private void Awake()
        {
            IsRagdollActive = false;
        }
    }
}