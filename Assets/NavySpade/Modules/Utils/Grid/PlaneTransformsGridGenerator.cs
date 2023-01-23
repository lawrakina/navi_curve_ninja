using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Modules.Utils.Grid
{
    public class PlaneTransformsGridGenerator : MonoBehaviour
    {
        public Transform targetPlaneTrans;
        public Transform targetCellsTransformsHolder;
        public MeshFilter targetPlaneMeshFilter;
        public MeshRenderer targetPlaneMeshRenderer;

        public bool isNeedCheckInnerColliders = false;
        public Transform collidersHolderTrans;
        public LayerMask collidersLayerMask;

        public Vector3 cellSpawnOffset = new Vector3(0, 0, 0);

        [HideInInspector] public Material targetPlaneMaterial;

        bool isGenerated;

        public class InnerCell
        {
            public PlaneTransformsGridGenerator Generator;

            public string name = "Unknown Cell";

            public int row = 0;
            public int col = 0;
            public int floorNum = 0;

            public GameObject cellObj;

            public Transform parentTrans;

            public bool isCollidedWithSomething = false;
            public bool isBlocked = false;

            public GameObject innerObj = null;

            public InnerCell(PlaneTransformsGridGenerator _generator)
            {
                Generator = _generator;
            }

            public bool GetIsCollidedWithTrans(Transform _otherTrans)
            {
                if (isCollidedWithSomething)
                    return true; // mb fix

                if (cellObj == null)
                    return false;

                var distToOtherTrans = Vector3.Distance(cellObj.transform.position, _otherTrans.position);

                isCollidedWithSomething = true;
                return isCollidedWithSomething;
            }

            public bool GetIsCollidedWithColliders(IEnumerable<Collider> colliders)
            {
                if (isCollidedWithSomething)
                    return true;

                if (cellObj == null)
                    return false;

                foreach (Collider collider in colliders)
                {
                    if (collider.isTrigger)
                    {
                        //Debug.Log(col.ClosestPoint(cellObj.transform.position));
                    }

                    var colliderTransform = collider.transform;
				
                    Vector3 cellPos = cellObj.transform.position;

                    Vector3 closest = collider.ClosestPoint(cellPos);
                    Vector3 origin = colliderTransform.position + (colliderTransform.rotation * collider.bounds.center);
                    Vector3 originToContact = closest - origin;
                    Vector3 pointToContact = closest - cellPos;

                    Rigidbody r = collider.attachedRigidbody;

                    // при использовании rigidbody нужно использовать доп. манипуляции для проверки коллизии
                    if (r != null)
                    {
                        originToContact = closest - (r.position + (r.rotation * r.centerOfMass));
                    }

                    if (Vector3.Angle(originToContact, pointToContact) < 90)
                    {
                        isCollidedWithSomething = true;
                    }
                }

                return isCollidedWithSomething;
            }

            public void PutObjectInside(GameObject _obj, bool _makeChild = false)
            {
                innerObj = _obj;

                if (_makeChild)
                    innerObj.transform.parent = cellObj.transform;
            }

            public void RemoveInnerObj()
            {
                innerObj = null;
            }

            public void SetIsBlocked(bool _b) {
                isBlocked = _b;
            }

            public bool GetIsBlocked() {
                return isBlocked;
            }

            public bool GetIsEmpty()
            {
                return innerObj == null;
            }
        }

        [HideInInspector] public List<InnerCell> innerCells = new List<InnerCell>();

        public int cellsDensityX = 10;
        public int cellsDensityZ = 10;

        public int floorsCount = 1;
        public float floorsOffset = 0.4f;

        public bool isHaveBlockedCells = false;
        public int initAvailableCellsCount = 50;

        private void Awake()
        {
            targetPlaneMaterial = targetPlaneMeshRenderer.material;
            //StartCoroutine(StartGridGeneration());
        }

        private IEnumerator StartGridGeneration()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            GenerateGrid();

            StopCoroutine(StartGridGeneration());
        }

        public void GenerateGrid()
        {
            innerCells.Clear();

            MeshFilter mf = targetPlaneMeshFilter;

            Mesh mesh = mf.mesh;
            float extentX = mesh.bounds.extents.x;
            float extentZ = mesh.bounds.extents.z;

            int spawnedCellsCount = 0;

            for (int iFloor = 0; iFloor < floorsCount; iFloor++) {
                for (int row = 0; row < cellsDensityZ; row++) {
                    for (int col = 0; col < cellsDensityX; col++) {

                        float marginX = extentX * 2 / cellsDensityX;
                        float marginZ = extentZ * 2 / cellsDensityZ;

                        Vector3 spawnPos = targetPlaneTrans.TransformPoint(
                            -extentX + marginX * col,
                            targetPlaneTrans.position.y,
                            -extentZ + marginZ * row
                        );

                        spawnPos.y = targetPlaneTrans.position.y + floorsOffset * iFloor;

                        spawnPos += new Vector3(cellSpawnOffset.x * col, cellSpawnOffset.y * iFloor, cellSpawnOffset.z * row);

                        bool isNeedSpawnCell = false;

                        if (isNeedCheckInnerColliders)
                        {
                            //if (Physics.Raycast(new Ray(spawnPos + Vector3.up * 2, Vector3.down), out RaycastHit hit, 3f, LayerMask.GetMask("Paintable"))) {
                            if (Physics.Raycast(new Ray(spawnPos + targetPlaneTrans.up * 2, targetPlaneTrans.up * -1 * 2), out RaycastHit hit, 3f,
                                collidersLayerMask))
                            {
                                bool isCorrectColliderHitted = false;

                                foreach (Transform colTrans in collidersHolderTrans)
                                {
                                    if (colTrans == hit.collider.transform)
                                    {
                                        isCorrectColliderHitted = true;

                                        break;
                                    }
                                }

                                if (!isCorrectColliderHitted)
                                {
                                    isNeedSpawnCell = false;
                                }
                                else
                                {
                                    if (false) {
                                        GameObject visualPrimitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                                        visualPrimitive.GetComponent<Collider>().enabled = false;
                                        visualPrimitive.transform.position = spawnPos;
                                    }

                                    isNeedSpawnCell = true;
                                }
                            }
                        }
                        else
                        {
                            isNeedSpawnCell = true;
                        }

                        if (isNeedSpawnCell)
                        {
                            string cellName = "InnerCell_floor_" + iFloor + "_col_" + col + "_row_" + row;

                            GameObject newCellObj = new GameObject(cellName);

                            InnerCell innerCell = new InnerCell(this)
                            {
                                name = cellName,
                                row = row,
                                col = col,
                                floorNum = iFloor,

                                parentTrans = targetCellsTransformsHolder,
                                cellObj = newCellObj,
                                innerObj = null
                            };

                            newCellObj.transform.position = this.transform.position + spawnPos;
                            newCellObj.transform.SetParent(targetCellsTransformsHolder);
                            newCellObj.transform.rotation = targetPlaneTrans.transform.rotation;

                            innerCells.Add(innerCell);

                            spawnedCellsCount++;

                            if (isHaveBlockedCells && spawnedCellsCount > initAvailableCellsCount) {
                                innerCell.SetIsBlocked(true);
                            }
                        }
                    }
                }
            }

            isGenerated = true;
        }

        public List<InnerCell> GetCellsFromTopInCount(int _count, bool _onlyEmpty = false)
        {
            List<InnerCell> resultCells = new List<InnerCell>();

            for (int i = innerCells.Count - 1; i >= 0; i--)
            {
                InnerCell checkCell = innerCells[i];

                bool isCorrectCell = true;

                if (checkCell.GetIsBlocked()) {
                    isCorrectCell = false;

                } else {
                    if (_onlyEmpty) {
                        isCorrectCell = checkCell.GetIsEmpty();
                    } else {
                        isCorrectCell = checkCell.GetIsEmpty() == false;
                    }
                }

                if (isCorrectCell) {
                    resultCells.Add(checkCell);
                }

                if (resultCells.Count >= _count) break;
            }

            return resultCells;
        }

        public List<InnerCell> GetCellsWithRow(int _row) {
            return innerCells.FindAll((item) => item.row == _row);
        }

        public InnerCell GetCellWithColInRow(int _row, int _col) {
            return innerCells.Find((item) => (item.row == _row && item.col == _col));
        }

        public InnerCell GetCellsWithFloor(int _floor) {
            return innerCells.Find((item) => item.floorNum == _floor); ;
        }

        public int GetFilledCellsCount() {
            int filledCellsCount = 0;

            foreach (InnerCell innerCell in innerCells)
            {
                if (!innerCell.GetIsEmpty() && !innerCell.GetIsBlocked()) 
                    filledCellsCount += 1;
            }

            return filledCellsCount;
        }

        public void UnlockCellsInCount(int _count) {
            int unlockedCellsCount = GetUnlockedCellsCount();

            for (int i = unlockedCellsCount; i < unlockedCellsCount + _count; i++) {
                if (i > innerCells.Count - 1) break;

                InnerCell curCell = innerCells[i];

                curCell.SetIsBlocked(false);
            }
        }

        public int GetUnlockedCellsCount() {
            int unlockedCellsCount = 0;

            foreach (InnerCell innerCell in innerCells) {
                if (!innerCell.GetIsBlocked()) unlockedCellsCount += 1;
            }

            return unlockedCellsCount;
        }

        public bool GetIsFull()
        {
            foreach (InnerCell innerCell in innerCells)
            {
                if (!innerCell.GetIsBlocked() && innerCell.GetIsEmpty())
                {
                    return false;
                }
            }

            return true;
        }

        private void OnDestroy()
        {
            innerCells.Clear();
        }

        private void OnDrawGizmos()
        {
            if (isGenerated)
            {
                foreach (InnerCell innerCell in innerCells)
                {
                    if (innerCell.GetIsBlocked()) 
                        Gizmos.color = Color.red;
                    else 
                        Gizmos.color = Color.white;

                    Gizmos.DrawSphere(innerCell.cellObj.transform.position, 0.5f);
                }
            }
        }
    }
}