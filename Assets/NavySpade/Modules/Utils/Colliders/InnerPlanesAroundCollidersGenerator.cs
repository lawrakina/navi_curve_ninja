using System.Collections.Generic;
using NavySpade.Modules.Extensions.UnityStructs;
using UnityEngine;

namespace NavySpade.Modules.Utils.Colliders
{
    public class InnerPlanesAroundCollidersGenerator : MonoBehaviour
    {
        [SerializeField] private float _height = 5;
        [SerializeField] private float _scaleCoeff = 0.3f;

        public bool isNeedShowGeneratedMesh = false;

        public LayerMask floorLayerMask;

        List<GameObject> colliderObjectsInside = new List<GameObject>();

        private void Start()
        {
            GenerateAroundColliders();
        }

        private void GenerateAroundColliders()
        {
            foreach (Transform innerPlaneTrans in transform)
            {
                // TODO: Component storage
                var mf = innerPlaneTrans.GetComponent<MeshFilter>();

                var originPos = Vector3.zero;

                if (mf != null)
                {
                    var extentX = mf.mesh.bounds.extents.x;
                    var extentZ = mf.mesh.bounds.extents.z;

                    // LEFT SIDE
                    var leftUpColObj = CreateColliderObj(innerPlaneTrans, "LeftUp",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.BACK, CubeAddit.CubePivotPoint.DOWN, CubeAddit.CubePivotPoint.RIGHT
                        });
                    var leftDownColObj = CreateColliderObj(innerPlaneTrans, "LeftDown",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.FORWARD, CubeAddit.CubePivotPoint.DOWN,
                            CubeAddit.CubePivotPoint.RIGHT
                        });

                    leftUpColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(-extentX, originPos.y, -extentZ));
                    leftDownColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(-extentX, originPos.y, extentZ));

                    var leftDist = leftUpColObj.transform.position.DistanceZ(leftDownColObj.transform.position);

                    SetObjScaleByMeshSizeInPixels(leftUpColObj,
                        leftUpColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        leftDist / 2, false, false, true);

                    SetObjScaleByMeshSizeInPixels(leftDownColObj,
                        leftUpColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        leftDist / 2, false, false, true);

                    // UP SIDE
                    var upLeftColObj = CreateColliderObj(innerPlaneTrans, "UpLeft",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.BACK, CubeAddit.CubePivotPoint.LEFT, CubeAddit.CubePivotPoint.DOWN
                        });
                    var upRightColObj = CreateColliderObj(innerPlaneTrans, "UpRight",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.BACK, CubeAddit.CubePivotPoint.RIGHT, CubeAddit.CubePivotPoint.DOWN
                        });

                    upLeftColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(-extentX, originPos.y, extentZ));
                    upRightColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(extentX, originPos.y, extentZ));

                    var upDist = upLeftColObj.transform.position.DistanceX(upRightColObj.transform.position);

                    SetObjScaleByMeshSizeInPixels(upLeftColObj,
                        upLeftColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        upDist / 2, true, false, false);

                    SetObjScaleByMeshSizeInPixels(upRightColObj,
                        upRightColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        upDist / 2, true, false, false);

                    // RIGHT SIDE
                    var rightUpColObj = CreateColliderObj(innerPlaneTrans, "RightUp",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.BACK, CubeAddit.CubePivotPoint.DOWN, CubeAddit.CubePivotPoint.LEFT
                        });
                    var rightDownColObj = CreateColliderObj(innerPlaneTrans, "RightDown",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.FORWARD, CubeAddit.CubePivotPoint.DOWN,
                            CubeAddit.CubePivotPoint.LEFT
                        });

                    rightUpColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(extentX, originPos.y, -extentZ));
                    rightDownColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(extentX, originPos.y, extentZ));

                    SetObjScaleByMeshSizeInPixels(rightUpColObj,
                        rightUpColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        leftDist / 2, false, false, true);

                    SetObjScaleByMeshSizeInPixels(rightDownColObj,
                        rightDownColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        leftDist / 2, false, false, true);

                    // DOWN SIDE
                    var downLeftColObj = CreateColliderObj(innerPlaneTrans, "DownLeft",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.FORWARD, CubeAddit.CubePivotPoint.LEFT,
                            CubeAddit.CubePivotPoint.DOWN
                        });
                    var downRightColObj = CreateColliderObj(innerPlaneTrans, "DownRight",
                        new List<CubeAddit.CubePivotPoint>()
                        {
                            CubeAddit.CubePivotPoint.FORWARD, CubeAddit.CubePivotPoint.RIGHT,
                            CubeAddit.CubePivotPoint.DOWN
                        });

                    downLeftColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(-extentX, originPos.y, -extentZ));
                    downRightColObj.transform.position =
                        innerPlaneTrans.TransformPoint(new Vector3(extentX, originPos.y, -extentZ));

                    SetObjScaleByMeshSizeInPixels(downLeftColObj,
                        downLeftColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        upDist / 2, true, false, false);

                    SetObjScaleByMeshSizeInPixels(downRightColObj,
                        downRightColObj.transform.Find("Cube").gameObject.GetComponent<MeshFilter>().mesh.bounds.size,
                        upDist / 2, true, false, false);

                    // set inner colliders height
                    foreach (Transform innerCubeHolderTrans in innerPlaneTrans)
                    {
                        Vector3 correctedScale = innerCubeHolderTrans.localScale;
                        correctedScale.y = _height;
                        innerCubeHolderTrans.localScale = correctedScale;

                        if (!isNeedShowGeneratedMesh)
                        {
                            innerCubeHolderTrans.Find("Cube").GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
            }
        }

        private void AnalyseIntersectCollisions()
        {
            //foreach (Transform innerPlaneTrans in transform) {
            if (transform.childCount > 1) return;

            // эта дичь не сработает

            for (int i = 1; i < transform.childCount; i++)
            {
                Transform prevPlaneTrans = transform.GetChild(i - 1);
                Transform curPlaneTrans = transform.GetChild(i);

                GameObject curColObj = colliderObjectsInside[i];
                GameObject prevColObj = colliderObjectsInside[i - 1];

                MeshFilter mf = curPlaneTrans.GetComponent<MeshFilter>();

                float extentX = mf.mesh.bounds.extents.x;
                float extentZ = mf.mesh.bounds.extents.z;

                Vector3 leftLimitPos = prevPlaneTrans.TransformPoint(new Vector3(-extentX, 0, 0));
                Vector3 rightLimitPos = prevPlaneTrans.TransformPoint(new Vector3(extentX, 0, 0));
                Vector3 upLimitPos = prevPlaneTrans.TransformPoint(new Vector3(0, 0, extentZ));
                Vector3 downLimitPos = prevPlaneTrans.TransformPoint(new Vector3(0, 0, -extentZ));

                foreach (Transform colTrans in curColObj.transform)
                {
                    //if (Physics.Raycast())

                    //Debug.Log(innerPlaneTrans.gameObject.name + "," + curColObj.name + " : " + innerPlaneTrans.TransformPoint(curColObj.transform.localPosition)
                    //    .normalized.ToString());
                }
            }
        }

        GameObject CreateColliderObj(Transform _parentTrans, string _namePrefix, List<CubeAddit.CubePivotPoint> _pivots)
        {
            GameObject newColObj = CubeAddit.CreateCubeWithPivotPos(CubeAddit.CreateMultiPivotsByList(_pivots));
            //newColObj.name = _namePrefix + " GenCollider " + (colliderObjectsInside.Count + 1);

            newColObj.name = _namePrefix + "GenCollider";
            newColObj.transform.parent = _parentTrans;

            colliderObjectsInside.Add(newColObj);

            return newColObj;
        }

        void SetObjScaleByMeshSizeInPixels(GameObject _obj, Vector3 _curObjSize, float _value, bool _isX = true,
            bool _isY = false, bool _isZ = false)
        {
            GameObject curObj = _obj;
            float targetSize = _value;

            //Vector3 curSize = curObj.GetComponent<MeshFilter>().mesh.bounds.size;
            Vector3 curSize = _curObjSize;
            Vector3 curScale = curObj.transform.localScale;

            curScale.x = _isX ? targetSize * curScale.x / curSize.x : curScale.x;
            curScale.y = _isY ? targetSize * curScale.y / curSize.y : curScale.y;
            curScale.z = _isZ ? targetSize * curScale.z / curSize.z : curScale.z;

            curObj.transform.localScale = curScale;
        }
    }
}