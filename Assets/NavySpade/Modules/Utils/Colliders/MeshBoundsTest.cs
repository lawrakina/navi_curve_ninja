using UnityEngine;

namespace NavySpade.Modules.Utils.Colliders
{
    public class MeshBoundsTest : MonoBehaviour
    {
        private Transform _marker1;
        private Transform _marker2;

        private int _clickCount = 0;

        private void Start()
        {
            _marker1 = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            _marker2 = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            _marker1.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            _marker2.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    var target = hit.transform;
                    float extent;
                    var mf = target.GetComponent<MeshFilter>();
                    var localHit = target.InverseTransformPoint(hit.point);

                    if (mf != null)
                    {
                        //if (Mathf.Abs(Vector3.Dot(hit.normal, target.forward)) > 0.9) {
                        if (_clickCount == 0)
                        {
                            extent = mf.mesh.bounds.extents.x;
                            _marker1.position = target.TransformPoint(new Vector3(-extent, localHit.y, localHit.z));
                            _marker2.position = target.TransformPoint(new Vector3(extent, localHit.y, localHit.z));
                        }
                        else
                        {
                            extent = mf.mesh.bounds.extents.z;
                            _marker1.position = target.TransformPoint(new Vector3(localHit.x, localHit.y, -extent));
                            _marker2.position = target.TransformPoint(new Vector3(localHit.x, localHit.y, extent));
                        }

                        Debug.Log("Distance 1: " + Vector3.Distance(_marker1.position, hit.point));
                        Debug.Log("Distance 2: " + Vector3.Distance(_marker2.position, hit.point));
                    }
                }

                _clickCount++;
                if (_clickCount > 1) _clickCount = 0;
            }
        }
    }
}