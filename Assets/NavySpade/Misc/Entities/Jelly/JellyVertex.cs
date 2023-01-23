using UnityEngine;

namespace Core.Entity.Jelly
{
    public class JellyVertex
    {
        public int verticeIndex;
        public Vector3 initialVertexPosition;
        public Vector3 currentVertexPosition;

        public Vector3 currentVelocity;

        public JellyVertex(int _vertexIndex, Vector3 _initialVertexPosition, Vector3 _currentVertexPosition, Vector3 _currentVertexVelocity)
        {
            verticeIndex = _vertexIndex;
            initialVertexPosition = _initialVertexPosition;
            currentVertexPosition = _currentVertexPosition;
            currentVelocity = Vector3.zero;
        }

        public Vector3 GetCurrentDisplacement()
        {
            return currentVertexPosition - initialVertexPosition;
        }

        public void UpdateVelocity(float _bounceSpeed)
        {
            currentVelocity = currentVelocity - GetCurrentDisplacement() * _bounceSpeed * Time.deltaTime;
        }

        public void Settle(float _stiffness)
        {
            currentVelocity *= 1f - _stiffness * Time.deltaTime;
        }

        public void ApplyPressureToVertex(Transform _transform, Vector3 _position, float _pressure)
        {
            Vector3 distanceVertexPoint = currentVertexPosition - _transform.InverseTransformPoint(_position);
            float adaptedPressure = _pressure / (1f + distanceVertexPoint.sqrMagnitude);
            float velocity = adaptedPressure * Time.deltaTime;
            currentVelocity += distanceVertexPoint.normalized * velocity;
        }
    }
}