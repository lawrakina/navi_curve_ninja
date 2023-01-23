using UnityEngine;

namespace Core
{
    public class RuntimeDispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateUpdater()
        {
            var obj = new GameObject("Mono Dispatcher").AddComponent<RuntimeDispatcher>();
            DontDestroyOnLoad(obj);
        }
    
        public static RuntimeDispatcher Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
