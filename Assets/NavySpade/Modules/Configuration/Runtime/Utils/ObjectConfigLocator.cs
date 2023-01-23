using System;
using System.Collections.Generic;
using System.Linq;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade.Modules.Configuration.Runtime.Utils
{
    public static class ObjectConfigLocator
    {
        /// <summary>
        /// Gets all configs in the assembly (also creates if there are none).
        /// </summary>
        /// <returns></returns>
        public static List<ObjectConfig> GetAllConfigs()
        {
            return (from domain in AppDomain.CurrentDomain.GetAssemblies()
                from type in domain.GetTypes()
                where type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(ObjectConfig))
                select ObjectConfig.GetConfig(type)).ToList();
        }

        public static List<T> GetAllConfigs<T>() where T : ObjectConfig<T>
        {
            return (from domain in AppDomain.CurrentDomain.GetAssemblies()
                from type in domain.GetTypes()
                where type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(ObjectConfig<T>))
                select ObjectConfig<T>.Instance).ToList();
        }
    }
}