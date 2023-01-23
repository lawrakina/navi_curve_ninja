using System;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace NavySpade.Modules.Utils.GizmoView
{
    public class GizmoDrawing : MonoBehaviour
    {
        [Flags]
        public enum ExecutionMode
        {
            AlwaysDraw = 1 << 0,
            SelectedDraw = 1 << 1
        }

        [SerializeReference] [SR] public IDrawingGizmo Gizmo;

        public ExecutionMode Mode;

        private void OnDrawGizmos()
        {
            if (Gizmo == null)
            {
                return;
            }

            if ((Mode & ExecutionMode.AlwaysDraw) != ExecutionMode.AlwaysDraw)
            {
                Gizmo.Draw(transform);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (Gizmo == null)
            {
                return;
            }

            if ((Mode & ExecutionMode.SelectedDraw) != ExecutionMode.SelectedDraw)
            {
                Gizmo.Draw(transform);
            }
        }
    }
}