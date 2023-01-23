using System;
using System.Collections.Generic;
using System.Linq;

namespace NavySpade.Modules.Utils.Singletons.Runtime.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
        /// <summary>
        /// Looks for [Singleton] tagged classes and returns all their names.
        /// </summary>
        /// <returns>List of class' with the <see cref="SingletonAttribute"/></returns>
        public static List<string> GetListOfSingletonClassNames()
        {
            return (from domain in AppDomain.CurrentDomain.GetAssemblies()
                from type in domain.GetTypes()
                where type.IsDefined(typeof(SingletonAttribute), true) && type.IsAbstract == false
                select type.Name).ToList();
        }
    }
}