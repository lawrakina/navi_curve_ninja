using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1pOku7G-X-U1qHPqZLVki4D_UomUsONdF8uoXV8D33Ts/edit#heading=h.dhyj0c6372pa")]
public class DeformationObject : MonoBehaviour
{
    [Range(0, 10)] public float maxDeform = 0.1f;

    [Range(0, 1)] public float damageFalloff = 1;

    [SerializeField] private float _deformRadius = 1;
    [SerializeField] private float _damageMultiplay = 1;

    private MeshFilter filter;
    private MeshCollider coll;
    private Vector3[] startingVerticies;
    private Vector3[] meshVerticies;

    private ContactPoint[] _points;

    private float _currentScore;
    private bool _isDestroing;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();

        if (GetComponent<MeshCollider>())
            coll = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        startingVerticies = filter.mesh.vertices;
        meshVerticies = filter.mesh.vertices;
    }

    /// <summary>
    /// добавляет давление к точки
    /// </summary>
    /// <param name="point"></param>
    /// <param name="radius"></param>
    /// <param name="damageMultiply"></param>
    public void AddDepresion(Vector3 point, float radius, float damageMultiply)
    {
        for (int i = 0; i < meshVerticies.Length; i++)
        {
            Vector3 vertexPosition = meshVerticies[i];
            Vector3 pointPosition = transform.InverseTransformPoint(point);
            float distanceFromCollision = Vector3.Distance(vertexPosition, pointPosition);
            float distanceFromOriginal = Vector3.Distance(startingVerticies[i], vertexPosition);

            if (distanceFromCollision < radius &&
                distanceFromOriginal < maxDeform) // If within collision radius and within max deform
            {
                float falloff = 1 - (distanceFromCollision / radius) * damageFalloff;

                float xDeform = pointPosition.x * falloff;
                float yDeform = pointPosition.y * falloff;
                float zDeform = pointPosition.z * falloff;

                xDeform = Mathf.Clamp(xDeform, 0, maxDeform);
                yDeform = Mathf.Clamp(yDeform, 0, maxDeform);
                zDeform = Mathf.Clamp(zDeform, 0, maxDeform);

                Vector3 deform = new Vector3(xDeform, yDeform, zDeform);
                meshVerticies[i] -= deform * damageMultiply;
            }
        }

        UpdateMeshVerticies();
    }

    private void UpdateMeshVerticies()
    {
        filter.mesh.vertices = meshVerticies;
        coll.sharedMesh = filter.mesh;
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (var contactPoint in other.contacts)
        {
            AddDepresion(contactPoint.point, _deformRadius, _damageMultiplay);
        }
    }
}