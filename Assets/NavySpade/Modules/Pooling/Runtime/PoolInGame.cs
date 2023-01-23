using LP.ComponentStorage;
using NavySpade.Modules.Pooling.Runtime.PoolBeacon;
using UnityEngine;

namespace NavySpade.Modules.Pooling.Runtime
{
    public abstract class PoolInGame<T> : PoolBehaviorBase where T : Component
    {
        [SerializeField] private ObjectPool<T> currentPool;
 
        [Header("Prefab")]
        [SerializeField] private T pooledObject;
    
        /// <summary>
        /// Initialize and preload pool
        /// </summary>
        public virtual void Initialize()
        {
            Debug.LogError("Initialize");
            currentPool = pooledObject.AddPool<T>(PoolKey, InitializeCount, PoolParent);
        
        }

        /// <summary>
        /// get an object from the pool 
        /// </summary>
        /// <returns>T Object </returns>
        public virtual T GetObject()
        {
            var tValue = currentPool.Get();
            if (needPoolBeacon)
            {
                if (tValue.TryGetComponent(out PoolBeacon.PoolBeacon beacon) == false)
                {
                    beacon = tValue.gameObject.AddComponent<PoolBeacon.PoolBeacon>();
                }

                var beaconToInit = new BaseBeacon<T>();
                beacon.ThisBeacon = beaconToInit;
                beaconToInit.Initialize(this, tValue, beacon);
            }

            if (needSaveCS)
            {
#if CStorage
                ComponentStorage.Add(tValue);
#endif
            }
            return tValue;
        }

        /// <summary>
        /// return object to the Pool
        /// </summary>
        /// <param name="returnValue">T Object</param>
        public virtual void Return(T returnValue)
        {
            currentPool.Return(returnValue);
        }
    

    }
}
