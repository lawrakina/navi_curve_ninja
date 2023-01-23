using UnityEngine;

namespace NavySpade.Modules.Utils.Setters
{
    public class PhysicsMaterialSetter : MonoBehaviour
    {
        [SerializeField] private PhysicMaterial _material = default;
        [SerializeField] private Collider[] _colliders = default;

        public void SetMaterial()
        {
            foreach (var collider in _colliders)
            {
                collider.material = _material;
            }
        }

        public void Reset()
        {
            _colliders = GetComponentsInChildren<Collider>();
        }
    }
}