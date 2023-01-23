using UnityEngine;

namespace NavySpade.Modules.Pooling.Runtime
{
    public class PoolBehaviorBase : MonoBehaviour
    {
        [Header("Key to keep in the pool ")]
        public string PoolKey;
        [Header("parent for folding the object pool ")]
        public Transform PoolParent;
        [Header("Preloading object count")]
        public int InitializeCount = 10;
        [Header("Do i need a beacon ")]
        public bool needPoolBeacon = true;
        [Header("[Module] need add object to the Component Storage")]
        public bool needSaveCS = true;
    }

    public interface IPoolServiceConfiguration
    {
        int SizeLimit { get; }
        
        int PreAllocatedElements { get; }
    }
}
