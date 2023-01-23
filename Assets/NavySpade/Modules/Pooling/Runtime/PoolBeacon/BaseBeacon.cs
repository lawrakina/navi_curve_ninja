using LP.ComponentStorage;
using UnityEngine;

namespace NavySpade.Modules.Pooling.Runtime.PoolBeacon
{
    public class BaseBeacon<T> : Beacon where T: Component
    {
        public PoolInGame<T> Pool { get; private set; }
        public T Component { get; private set; }

        /// <summary>
        /// Initialize Beacon
        /// </summary>
        /// <param name="pool">pool in game</param>
        /// <param name="component">Component Value</param>
        /// <param name="baseMono"></param>
        public void Initialize(PoolInGame<T> pool, T component, PoolBeacon baseMono)
        {
            MonoMain = baseMono;
            Component = component;
            Pool = pool;
#if CStorage
            ComponentStorage.Add(baseMono);
#endif
        }
        /// <summary>
        /// Return object to the pool.
        /// </summary>
        public void ReturnToThePool()
        {
            Pool.Return(Component);
        }
    
        private void OnDestroy()
        {
#if CStorage
            ComponentStorage.Remove(MonoMain);
#endif
        }
    }
}