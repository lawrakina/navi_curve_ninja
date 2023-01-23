using LP.ComponentStorage;
using NavySpade.Modules.Pooling.Runtime.PoolBeacon;
using UnityEngine;

namespace NavySpade.Modules.Pooling.Runtime.Utility
{
    public static class PoolUtility
    {
        public static void ReturnToPool_Beacon<T>(GameObject gameObject) where T : Component
        {
#if CStorage
            var beacon = ComponentStorage.GetElement<PoolBeacon.PoolBeacon>(gameObject);
            var currentBeacon = beacon.ThisBeacon as BaseBeacon<T>;
            currentBeacon.ReturnToThePool();
#endif
        }

        public static void ReturnToThePool<T>(this T component) where T : Component
        {
            component.Return();
        }

        public static void ReturnToThePool<T>(this T component, string key) where T : Component
        {
            PoolHandler.Return(key, component);
        }
    }
}