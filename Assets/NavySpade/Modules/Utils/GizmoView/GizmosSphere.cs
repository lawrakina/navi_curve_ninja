using System;
using UnityEngine;

namespace NavySpade.Modules.Utils.GizmoView
{
    [Serializable]
    [CustomSerializeReferenceName("Sphere")]
    public class GizmosSphere : IDrawingGizmo
    {
        public float Radius = 0.1f;
        public Color gizmosColor = Color.red;

        public void Draw(Transform transform)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawSphere(transform.position, Radius);
        }
    }
}
