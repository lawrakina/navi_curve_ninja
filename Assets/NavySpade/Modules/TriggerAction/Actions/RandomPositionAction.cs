using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Repositions the named GameObject somewhere randomly between the specified MinPosition and MaxPosition.
    /// </summary>
    public class RandomPositionAction : ActionBase
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Vector3 _positionMin = Vector3.zero;
        [SerializeField] private Vector3 _positionMax = Vector3.one;

        private void Start()
        {
            if (_target == null)
                _target = gameObject;
        }

        public override void Fire()
        {
            var x = Random.Range(_positionMin.x, _positionMax.x);
            var y = Random.Range(_positionMin.y, _positionMax.y);
            var z = Random.Range(_positionMin.z, _positionMax.z);
            
            _target.transform.localPosition = new Vector3(x, y, z);
        }
    }
}