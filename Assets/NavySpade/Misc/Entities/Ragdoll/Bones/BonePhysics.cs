using UnityEngine;

namespace Misc.Entities.Ragdoll
{
    public class BonePhysics : MonoBehaviour
    {
        public RagdollHandler mainHandler;
        public BoneType ragdollType;
        public Rigidbody attachedRigidbody;
        public CharacterJoint attachedJoint;
        [SerializeField] private Collider _collider;
        
        [HideInInspector] public RigidbodyConstraints constraints;
        
        private void Start()
        {
            if (attachedRigidbody != null)
                constraints = attachedRigidbody.constraints;
        }
        
        public void SetParameters(RagdollBoneParameters parameters)
        {
            if (parameters == null)
                return;

            if (attachedRigidbody)
            {
                attachedRigidbody.mass = parameters.mass;
                attachedRigidbody.drag = parameters.drag;
                attachedRigidbody.angularDrag = parameters.angularDrag;
                attachedRigidbody.useGravity = parameters.useGravity;
            }

            if (_collider)
            {
                _collider.material = parameters.physicsMaterial;
            }

            if (attachedJoint)
            {
                attachedJoint.swing1Limit = parameters.swing1.Get();
                attachedJoint.swing2Limit = parameters.swing2.Get();
                attachedJoint.lowTwistLimit = parameters.lowTwist.Get();
                attachedJoint.highTwistLimit = parameters.highTwist.Get();
            }
        }
        
        public void Reset()
        {
            mainHandler = GetComponentInParent<RagdollHandler>();
            attachedRigidbody = GetComponent<Rigidbody>();
            attachedJoint = GetComponent<CharacterJoint>();
            _collider = GetComponent<Collider>();
        }
    }
}