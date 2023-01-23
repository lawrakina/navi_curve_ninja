using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Deletes a GameObject.
    /// </summary>
    public class DestroyGameObjectAction : ActionBase
    {
        [SerializeField] private GameObject _gameObject;

        public override void Fire()
        {
            Destroy(_gameObject);
        }
    }
}
