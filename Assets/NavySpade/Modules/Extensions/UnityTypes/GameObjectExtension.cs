using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Modules.Extensions.UnityTypes
{
    /// <summary>
    /// Helper methods for performing additional operations on <see cref="GameObject"/> instances.
    /// </summary>
    public static class GameObjectExtension
    {
        /// <summary>
        /// Builds a string that represents a <see cref="GameObject"/>'s position in the scene hierarchy.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to evaluate.</param>
        /// <returns>A string showing the parent/child chain from root-most <see cref="GameObject"/> to the given <see cref="GameObject"/>.</returns>
        public static string GetParentNameHierarchy(this GameObject gameObject)
        {
            var parentObj = gameObject.transform;
            var stack = new Stack<string>();

            while (parentObj != null)
            {
                stack.Push(parentObj.name);
                parentObj = parentObj.parent;
            }

            var nameHierarchy = "";
            while (stack.Count > 0)
            {
                nameHierarchy += stack.Pop() + "->";
            }

            return nameHierarchy;
        }
        
        /// <summary>
        /// Move root and all children to the specified layer.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to evaluate.</param>
        /// <param name="layer"></param>
        public static void MoveToLayer(this GameObject gameObject, int layer)
        {
            InternalMoveToLayer(gameObject.transform, layer);
        }

        /// <summary>
        /// Is the object's layer in the specified layermask.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to evaluate.</param>
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
        /// <param name="gameObject">The <see cref="GameObject"/> to evaluate.</param>
        /// <param name="tag"></param>
        /// <param name="needChildren"></param>
        public static void MoveToTag(this GameObject gameObject, string tag, bool needChildren)
        {
            InternalMoveToTag(gameObject.transform, tag, needChildren);
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

        #region Sintax

        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.TryGetComponent(out T _);
        }
        
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }

        public static T GetInChildrenOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponentInChildren<T>() ?? obj.AddComponent<T>();
        }

        #endregion
    }
}
