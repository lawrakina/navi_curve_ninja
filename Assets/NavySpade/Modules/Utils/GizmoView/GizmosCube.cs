using System;
using UnityEngine;

namespace NavySpade.Modules.Utils.GizmoView
{
    [Serializable]
    [CustomSerializeReferenceName("Cube")]
    public class GizmosCube : IDrawingGizmo
    {
        public Color gizmosColor;
        public Vector3 gizmosSize;

        public void Draw(Transform transform)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireCube(transform.position, gizmosSize);
            Gizmos.DrawLine(transform.position, transform.forward * gizmosSize.z * 2);
        }
    }
}