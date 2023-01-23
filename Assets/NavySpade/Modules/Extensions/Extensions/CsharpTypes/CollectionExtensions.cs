using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Extensions.CsharpTypes
{
    /// <summary>
    /// Работа с коллекциями
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// return random element
        /// </summary>
        /// <param name="elements">list</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns>random element</returns>
        public static T RandomElement<T>(this List<T> elements)
        {
            if (elements.Count == 0)
            {
                return default(T);
            }

            return elements[Random.Range(0, elements.Count)];
        }
    
        /// <summary>
        /// return random element
        /// </summary>
        /// <param name="elements">list</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns>random element</returns>
        public static T RandomElement<T>(this IEnumerable<T> elements)
        {
            if (elements.Any() == false)
            {
                return default(T);
            }

            return elements.ElementAt(Random.Range(0, elements.Count()));
        }
    
        public static void Shuffle<T> (this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = Random.Range(0, n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        /// <summary>
        /// поиск ближайщей точки
        /// </summary>
        /// <param name="array">точки как компоненты</param>
        /// <param name="point">точка</param>
        /// <typeparam name="T">тип</typeparam>
        /// <returns>ближайщая точка</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Transform FindClosestPoint<T>(this IEnumerable<T> array, Vector3 point) where T : Component
        {
            float distance = float.PositiveInfinity;
            Transform result = null;

            foreach (var component in array)
            {
                var currentTransform = component.transform;
                var currentDistance = Vector3.Distance(currentTransform.position, point);

                if (currentDistance < distance)
                {
                    result = currentTransform;
                    distance = currentDistance;
                }
            }

            if (result == null)
                throw new ArgumentNullException(nameof(array), "array argument empty");

            return result;
        }
    
        public static T Next<T>(this List<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index < collection.Count - 1)
                index++;
            else
                index = 0;

            return collection[index];
        }

        public static T Previous<T>(this List<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index > 0)
                index--;
            else
                index = collection.Count - 1;

            return collection[index];
        }
    }
}
