using System;
using System.Collections.Generic;
using System.Linq;
using NavySpade.Modules.Extensions.Exceptions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NavySpade.Modules.Extensions.CsharpTypes
{
     /// <summary>
    /// <see cref="IEnumerable{T}"/> extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Syntax

        /// <summary>
        /// Returns true if collection is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
            => source == null || !source.Any();

        /// <summary>
        /// Returns specified value if source is null/empty/else same.
        /// </summary>
        public static IEnumerable<T> Or<T>(this IEnumerable<T> source, IEnumerable<T> or) =>
            source.IsNullOrEmpty() ? or : source;

        /// <summary>
        /// Returns empty if enumerable is null else same enumerable.
        /// </summary>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
            => source ?? Enumerable.Empty<T>();

        /// <summary>
        /// Returns empty if enumerable is null else same enumerable.
        /// </summary>
        public static IEnumerable<T> EmptyIfDefault<T>(this IEnumerable<T> source) => source.OrEmpty();

        #endregion

        #region Random

        /// <summary>
        /// Return random element.
        /// </summary>
        /// <param name="elements">list</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns>random element</returns>
        public static T RandomElement<T>(this IEnumerable<T> elements)
        {
            elements.ThrowIfNull("Collection is Null!");
            return elements.Any() == false
                ? default(T)
                : elements.ElementAt(Random.Range(0, elements.Count()));
        }

        #endregion

        #region Searching

        /// <summary>
        /// Search nearest point.
        /// </summary>
        /// <param name="enumerable">Point as components</param>
        /// <param name="point">Position</param>
        /// <typeparam name="T">Point type</typeparam>
        /// <returns>Nearest/closest point</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Transform FindNearestPoint<T>(this IEnumerable<T> enumerable, Vector3 point) where T : Component
        {
            var distance = float.PositiveInfinity;
            Transform result = null;

            foreach (var component in enumerable)
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
            {
                throw new ArgumentNullException(nameof(enumerable), "Collection argument empty!");
            }

            return result;
        }

        #endregion
    }
}