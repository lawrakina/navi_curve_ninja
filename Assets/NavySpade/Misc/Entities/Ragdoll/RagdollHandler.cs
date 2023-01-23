using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Misc.Entities.Ragdoll
{
    public class RagdollHandler : MonoBehaviour
    {
        public Rigidbody mainRigidbody;
        public List<Bone> bones = new List<Bone>();
        
        private RagdollBodyParameters _parameters;

        public void ActivateAny(List<BoneType> elements)
        {
            foreach (var bone in bones)
            {
                if (bone.Physics.attachedRigidbody == null)
                    continue;

                bone.Physics.attachedRigidbody.isKinematic = !elements.Contains(bone.Physics.ragdollType);
            }
        }

        public void ActivateAnyRotate(List<BoneType> elements)
        {
            foreach (var element in bones)
            {
                if (element.Physics.attachedRigidbody == null)
                    continue;

                element.Physics.attachedRigidbody.constraints = !elements.Contains(element.Physics.ragdollType)
                    ? RigidbodyConstraints.FreezeRotation
                    : element.Physics.constraints;

                element.Physics.attachedRigidbody.isKinematic = false;
            }
        }

        public void ActivateAll()
        {
            foreach (var element in bones)
            {
                if (element.Physics.attachedRigidbody == null)
                    continue;

                element.Physics.attachedRigidbody.isKinematic = false;
                element.Physics.attachedRigidbody.constraints = element.Physics.constraints;
            }
        }

        public void DeactivateAll()
        {
            foreach (var element in bones)
            {
                if (element.Physics.attachedRigidbody == null)
                    continue;

                element.Physics.attachedRigidbody.isKinematic = true;
                element.Physics.attachedRigidbody.constraints = element.Physics.constraints;
            }
        }

        public void SetParameters(RagdollBodyParameters parameters)
        {
            _parameters = parameters;
            foreach (var element in bones)
            {
                element.Physics.SetParameters(
                    _parameters.data.FirstOrDefault(e => e.type == element.Physics.ragdollType));
            }
        }
    }
}