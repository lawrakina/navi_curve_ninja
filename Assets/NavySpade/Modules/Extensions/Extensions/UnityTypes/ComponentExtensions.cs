using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolkit.Extensions.UnityTypes
{
    public static class ComponentExtensions
    {
        public static T GetSingleActive<T>(this IEnumerable<T> collection) where T : Component
        {
            return collection.FirstOrDefault(element => element.gameObject.activeInHierarchy);
        }
    }
}