using UnityEngine;

namespace Main.Levels.AdditionData
{
    public class Background : ILevelExtensionData
    {
        [field:SerializeField] public GameObject Prefab { get; private set; }

        private GameObject _instance;
        
        public void Apply()
        {
            _instance = GameObject.Instantiate(Prefab);
        }

        public void Clear()
        {
            GameObject.Destroy(_instance);
        }
    }
}