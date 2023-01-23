using UnityEngine;

namespace Core.Extensions.UnityTypes
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }

        public static T GetInChildrenOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponentInChildren<T>() ?? obj.AddComponent<T>();
        }

        /// <summary>
        /// Move root and all children to the specified layer.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="layer"></param>
        public static void MoveToLayer(this GameObject root, int layer)
        {
            InternalMoveToLayer(root.transform, layer);
        }

        /// <summary>
        /// Is the object's layer in the specified layermask.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask)
        {
            return ((mask.value & (1 << gameObject.layer)) > 0);
        }

        // Recursive calls.
        private static void InternalMoveToLayer(Transform root, int layer)
        {
            root.gameObject.layer = layer;

            foreach (Transform child in root)
            {
                InternalMoveToLayer(child, layer);
            }
        }

        /// <summary>
        /// Sets tags for the object and/or its children.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="tag"></param>
        /// <param name="needChildren"></param>
        public static void MoveToTag(this GameObject root, string tag, bool needChildren)
        {
            InternalMoveToTag(root.transform, tag, needChildren);
        }

        private static void InternalMoveToTag(this Transform root, string tag, bool needChildren)
        {
            root.gameObject.tag = tag;
        
            if (needChildren == false)
            {
                return;
            }
        
            foreach (Transform child in root)
            {
                InternalMoveToTag(child, tag,true);
            }
        }
    }
}
