using UnityEngine;

namespace Misc.Entities.Ragdoll
{
    public class Bone : MonoBehaviour
    {
        [field: SerializeField] public BonePhysics Physics { get; private set; }
        //[field: SerializeField] public BoneVisual Visual { get; private set; }

        public void Reset()
        {
            Physics = GetComponentInChildren<BonePhysics>();
            //Visual = GetComponentInChildren<BoneVisual>();
        }
    }
}