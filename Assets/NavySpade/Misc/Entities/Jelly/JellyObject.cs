using UnityEngine;

namespace Core.Entity.Jelly
{
    [HelpURL("https://docs.google.com/document/d/1pOku7G-X-U1qHPqZLVki4D_UomUsONdF8uoXV8D33Ts/edit#heading=h.96q7y6a5wcn2")]
    public class JellyObject : MonoBehaviour
    {
        public float bounceSpeed;
        public float fallForce;
        public float stiffness;

        private MeshFilter meshFilter;
        private Mesh mesh;

        private JellyVertex[] jellyVertices;
        private Vector3[] currentMeshVertices;


        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            mesh = meshFilter.mesh;

            GetVertuces();
        }

        private void GetVertuces()
        {
            jellyVertices = new JellyVertex[mesh.vertices.Length];
            currentMeshVertices = new Vector3[mesh.vertices.Length];

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                jellyVertices[i] = new JellyVertex(i, mesh.vertices[i], mesh.vertices[i], Vector3.zero);
                currentMeshVertices[i] = mesh.vertices[i];
            }
        }

        private void Update()
        {
            UpdateVertices();
        }

        private void UpdateVertices()
        {
            for (int i = 0; i < jellyVertices.Length; i++)
            {
                var currentJellyVertices = jellyVertices[i];
                currentJellyVertices.UpdateVelocity(bounceSpeed);
                currentJellyVertices.Settle(stiffness);

                currentJellyVertices.currentVertexPosition += jellyVertices[i].currentVelocity * Time.deltaTime;
                currentMeshVertices[i] = currentJellyVertices.currentVertexPosition;
            }

            mesh.vertices = currentMeshVertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        private void OnCollisionEnter(Collision other)
        {
            var points = other.contacts;
            foreach (var point in points)
            {
                var inputPoint = point.point + (point.point * .1f);
                ApplyPressureToPoint(inputPoint, fallForce);
            }
        }

        private void ApplyPressureToPoint(Vector3 _point, float _pressure)
        {
            for (int i = 0; i < jellyVertices.Length; i++)
            {
                jellyVertices[i].ApplyPressureToVertex(transform, _point, _pressure);
            }
        }
    }
}