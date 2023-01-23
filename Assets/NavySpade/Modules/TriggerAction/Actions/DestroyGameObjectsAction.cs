using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Deletes a number of GameObject's.
    /// </summary>
    public class DestroyGameObjectsAction : ActionBase
    {
        [SerializeField] private GameObject[] _gameObjects;

        public override void Fire()
        {
            foreach (var obj in _gameObjects)
            {
                Destroy(obj);
            }
        }
    }
}
