using System;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Data
{
    [Serializable]
    public struct TransformData
    {
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
        public Vector3 LocalScale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }

        public TransformData(Transform transform) : this(transform.localPosition, transform.localRotation,
            transform.localScale)
        {
        }

        public void ApplyTo(Transform transform)
        {
            transform.localPosition = LocalPosition;
            transform.localRotation = LocalRotation;
            transform.localScale = LocalScale;
        }
    }
}