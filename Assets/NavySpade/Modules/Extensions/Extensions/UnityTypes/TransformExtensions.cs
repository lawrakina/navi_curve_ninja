using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Extensions.UnityTypes
{
    public static class TransformExtensions
    {
        #region SetPos
        
        public static void SetZPosLocal(this Transform transform, float zValue)
        {
            var lp = transform.localPosition;
            lp.z = zValue;
            transform.localPosition = lp;
        }

        public static void SetYPosLocal(this Transform transform, float yValue)
        {
            var lp = transform.localPosition;
            lp.y = yValue;
            transform.localPosition = lp;
        }

        public static void SetXPosLocal(this Transform transform, float xValue)
        {
            var lp = transform.localPosition;
            lp.x = xValue;
            transform.localPosition = lp;
        }

        public static void SetXPos(this Transform transform, float xValue)
        {
            var lp = transform.position;
            lp.x = xValue;
            transform.position = lp;
        }

        public static void SetYPos(this Transform transform, float yValue)
        {
            var lp = transform.position;
            lp.y = yValue;
            transform.position = lp;
        }

        public static void SetXZPosLocal(this Transform transform, float x, float z)
        {
            var lp = transform.localPosition;
            lp.x = x;
            lp.z = z;
            transform.localPosition = lp;
        }

        public static void SetXZPosLocal(this Transform transform, Transform newLp)
        {
            var lp = transform.localPosition;
            lp.x = newLp.localPosition.x;
            lp.z = newLp.localPosition.z;
            transform.localPosition = lp;
        }

        public static void SetXZpos(this Transform transform, Transform newLp)
        {
            var lp = transform.position;
            lp.x = newLp.position.x;
            lp.z = newLp.position.z;
            transform.position = lp;
        }
        
        #endregion
        #region Sided

        public static Vector3 backward(this Transform transform)
        {
            return -transform.forward;
        }

        public static Vector3 down(this Transform transform)
        {
            return -transform.up;
        }

        public static Vector3 left(this Transform transform)
        {
            return -transform.right;
        }
        
        #endregion
        #region FindClosed
        
        [CanBeNull] public static T FindClosed<T>(this List<T> array, Vector3 point) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            if (array == null)
                return null;

            T component;
            for (var i = 0; i < array.Count; i++)
            {
                component = array[i];
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this T[] array, Vector3 point) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            if (array == null)
                return null;

            T component;
            for (var i = 0; i < array.Length; i++)
            {
                component = array[i];
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this IEnumerable<T> array, Vector3 point) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            foreach (var component in array)
            {
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        
        [CanBeNull] public static T FindClosed<T>(this List<T> array, Vector3 point, float maxDistance) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Count; i++)
            {
                var component = array[i];
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this T[] array, Vector3 point, float maxDistance) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Length; i++)
            {
                var component = array[i];
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this IEnumerable<T> array, Vector3 point, float maxDistance) where T : Component
        {
            float distance = float.PositiveInfinity;
            T result = null;

            foreach (var component in array)
            {
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        
        [CanBeNull] public static T FindClosed<T>(this List<T> array, Vector3 point, IterationFilter<T> filter) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Count; i++)
            {
                var component = array[i];
                if (filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if (filter.InvokeConditions(component) == false)
                    continue;

                if (filter.IsFirstResult)
                    return component;

                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this T[] array, Vector3 point, IterationFilter<T> filter) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Length; i++)
            {
                var component = array[i];
                if (filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if (filter.InvokeConditions(component) == false)
                    continue;

                if (filter.IsFirstResult)
                    return component;

                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this IEnumerable<T> array, Vector3 point, IterationFilter<T> filter) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            foreach (var component in array)
            {
                if(filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if(filter.InvokeConditions(component) == false)
                    continue;

                if (filter.IsFirstResult)
                    return component;

                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        
        [CanBeNull] public static T FindClosed<T>(this List<T> array, Vector3 point, IterationFilter<T> filter, float maxDistance) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Count; i++)
            {
                var component = array[i];
                if (filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if(filter.InvokeConditions(component) == false)
                    continue;

                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (filter.IsFirstResult)
                    return component;

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this T[] array, Vector3 point, IterationFilter<T> filter, float maxDistance) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            for (var i = 0; i < array.Length; i++)
            {
                var component = array[i];
                if (filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if(filter.InvokeConditions(component) == false)
                    continue;

                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (filter.IsFirstResult)
                    return component;

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        [CanBeNull] public static T FindClosed<T>(this IEnumerable<T> array, Vector3 point, IterationFilter<T> filter, float maxDistance) where T : Component
        {
            var distance = float.PositiveInfinity;
            T result = null;

            foreach (var component in array)
            {
                if(filter.Ignoring != null && filter.Ignoring.Contains(component))
                    continue;
                if(filter.InvokeConditions(component) == false)
                    continue;
                
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);
                
                if (currentDistance > maxDistance)
                {
                    continue;
                }

                if (filter.IsFirstResult)
                    return component;

                if (currentDistance < distance)
                {
                    result = component;
                    distance = currentDistance;
                }
            }

            return result;
        }
        #endregion

        public static Vector3 DirectionTo(this Transform transform, Vector3 target)
        {
            return (target - transform.position).normalized;
        }
    }
}
