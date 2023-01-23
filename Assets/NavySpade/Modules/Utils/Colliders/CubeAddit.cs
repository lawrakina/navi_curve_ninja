using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NavySpade.Modules.Utils.Colliders
{
    public class CubeAddit
    {
        public enum CubePivotPoint
        {
            MIDDLE,
            LEFT,
            RIGHT,
            UP,
            DOWN,
            FORWARD,
            BACK
        }

        private static Vector3 CreatePivot(CubePivotPoint pivot)
        {
            return pivot switch
            {
                CubePivotPoint.MIDDLE => new Vector3(0f, 0f, 0f),
                CubePivotPoint.LEFT => new Vector3(-0.5f, 0f, 0f),
                CubePivotPoint.RIGHT => new Vector3(0.5f, 0f, 0f),
                CubePivotPoint.UP => new Vector3(0f, 0.5f, 0f),
                CubePivotPoint.DOWN => new Vector3(0f, -0.5f, 0f),
                CubePivotPoint.FORWARD => new Vector3(0f, 0f, 0.5f),
                CubePivotPoint.BACK => new Vector3(0f, 0f, -0.5f),
                _ => default(Vector3)
            };
        }

        public static Vector3 CreateMultiPivotsByList(IEnumerable<CubePivotPoint> pivots)
        {
            return pivots.Aggregate(Vector3.zero, (current, pivotP) => current + CreatePivot(pivotP));
        }

        public static GameObject CreateCubeWithPivotPos(Vector3 pivotPos)
        {
            var childCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var parentObject = new GameObject("CubeHolder")
            {
                transform =
                {
                    position = pivotPos
                }
            };
            childCube.transform.SetParent(parentObject.transform);

            return parentObject;
        }
    }
}