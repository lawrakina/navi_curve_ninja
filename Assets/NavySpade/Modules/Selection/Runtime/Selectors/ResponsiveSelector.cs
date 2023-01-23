using NavySpade.Modules.Selection.Runtime.SceneInteraction;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Selectors
{
    public class ResponsiveSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private Selectable[] _selectables;
        [SerializeField] private float _threshold = 0.97f;

        private Transform _selection;

        public void Check(Ray ray)
        {
            _selection = null;

            var closest = 0f;

            for (var i = 0; i < _selectables.Length; i++)
            {
                var vector1 = ray.direction;
                var vector2 = _selectables[i].transform.position - ray.origin;

                var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);

                _selectables[i].LookPercentage = lookPercentage;

                if (lookPercentage > _threshold && lookPercentage > closest)
                {
                    closest = lookPercentage;
                    _selection = _selectables[i].transform;
                }
            }
        }

        public Transform GetSelection()
        {
            return _selection;
        }

        public void Reset()
        {
            _selectables = FindObjectsOfType<Selectable>();
        }
    }
}