using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc.Entities.Ragdoll
{
    [CreateAssetMenu(fileName = "Ragdoll Params", menuName = "Game/Entity/Ragdoll Params", order = 51)]
    public class RagdollBodyParameters : ScriptableObject
    {
        public List<RagdollBoneParameters> data = new List<RagdollBoneParameters>()
        {
            new RagdollBoneParameters {type = BoneType.Pelvis},
            new RagdollBoneParameters {type = BoneType.LeftHips},
            new RagdollBoneParameters {type = BoneType.LeftKnee},
            new RagdollBoneParameters {type = BoneType.RightHips},
            new RagdollBoneParameters {type = BoneType.RightKnee},
            new RagdollBoneParameters {type = BoneType.LeftArm},
            new RagdollBoneParameters {type = BoneType.LeftElbow},
            new RagdollBoneParameters {type = BoneType.RightArm},
            new RagdollBoneParameters {type = BoneType.RightElbow},
            new RagdollBoneParameters {type = BoneType.MiddleSpine},
            new RagdollBoneParameters {type = BoneType.Head},
        };
    }

    public enum BoneType
    {
        Pelvis,
        LeftHips,
        LeftKnee,
        LeftFoot,
        RightHips,
        RightKnee,
        RightFoot,
        LeftArm,
        LeftElbow,
        RightArm,
        RightElbow,
        MiddleSpine,
        Head,
    }

    [Serializable]
    public class RagdollBoneParameters
    {
        public BoneType type;
        public float mass = 3.125f;
        public float drag = 0;
        public float angularDrag = 0.05f;
        
        public bool useGravity = true;
        
        public PhysicMaterial physicsMaterial = default;
        
        public LimitSettings lowTwist = new LimitSettings();
        public LimitSettings highTwist = new LimitSettings();
        public LimitSettings swing1 = new LimitSettings();
        public LimitSettings swing2 = new LimitSettings();
    }

    [Serializable]
    public class LimitSettings
    {
        public float limit;
        public float bounciness;
        public float contactDistance;

        public SoftJointLimit Get()
        {
            var softJointLimit = new SoftJointLimit
            {
                bounciness = bounciness,
                limit = limit,
                contactDistance = contactDistance
            };
            
            return softJointLimit;
        }
    }
}