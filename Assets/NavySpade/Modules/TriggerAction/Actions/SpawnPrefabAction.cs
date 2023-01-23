using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Spawns a specified GameObject.
    /// These last two settings are useful for one-shot particle emitters.
    /// </summary>
    public class SpawnPrefabAction : ActionBase
    {
        [SerializeField] private GameObject _prefab;

        [Tooltip("Boolean that specifies if this gameObject should be the parent")] [SerializeField]
        private bool _attachToSelf;

        [field: Tooltip("Indicates the spawned object should eventually be deleted")]
        [field: SerializeField]
        public bool IsTemporary { get; private set; }

        [field: Tooltip("Indicates how long the object should live.")]
        [field: Min(0f)]
        [field: SerializeField]
        public float TemporaryLifespan { get; private set; }

        public override void Fire()
        {
            var instance = Instantiate(_prefab, transform.position, transform.rotation);

            if (IsTemporary)
                Destroy(instance, TemporaryLifespan);

            if (_attachToSelf)
                instance.transform.parent = transform;
        }
    }
}