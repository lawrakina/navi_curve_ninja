using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Modules.Extensions.CsharpTypes
{
    /// <summary>
    /// <see cref="List{T}"/> extensions.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Return random element.
        /// </summary>
        /// <param name="elements">list</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns>random element</returns>
        public static T RandomElement<T>(this List<T> elements)
        {
            return elements.Count == 0 
                ? default(T)
                : elements[Random.Range(0, elements.Count)];
        }
        
        #region Iterations

        /// <summary>
        /// Returns next item int the list.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="current"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Next<T>(this List<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index < collection.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            return collection[index];
        }

        /// <summary>
        /// Returns previous item in the list.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="current"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Previous<T>(this List<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = collection.Count - 1;
            }

            return collection[index];
        }

        #endregion
        
        /// <summary>
        /// Removes the last <paramref name="n"/> item(s) in the list.
        /// </summary>
        public static void RemoveLast<T>(this IList<T> source, int n = 1)
        {
            for (var i = 0; i < n; i++)
            {
                source.RemoveAt(source.Count - 1);
            }
        }

        /// <summary>
        /// Returns a list of numbers from zero to length.
        /// </summary>
        /// <param name="lenght">Maximum number</param>
        /// <returns>List of integers</returns>
        public static List<int> GenerateIndexList(int lenght)
        {
            var indexList = new List<int>();

            var i = 0;
            while (i < lenght)
            {
                indexList.Add(i);
                i++;
            }

            return indexList;
        }
    }
}