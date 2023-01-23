using System;
using System.Collections.Generic;
using System.Reflection;
using NavySpade.Modules.Configuration.Runtime.Attributes;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade.Modules.Configuration.Editor.Utils
{
    internal class ConfigOrderComparer : IComparer<ObjectConfig>
    {
        public int Compare(ObjectConfig config1, ObjectConfig config2)
        {
            if (config1 == null || config2 == null)
            {
                throw new NullReferenceException();
            }
            
            var type1 = config1.GetType();
            var type2 = config2.GetType();

            var attribute1 = type1.GetCustomAttribute(typeof(ConfigAttribute)) as ConfigAttribute;
            var attribute2 = type2.GetCustomAttribute(typeof(ConfigAttribute)) as ConfigAttribute;

            if (attribute1 != null && attribute2 != null)
            {
                return attribute1.CompareTo(attribute2);
            }

            if (attribute1 == null && attribute2 != null)
            {
                return 1;
            }

            if (attribute1 != null)
            {
                return -1;
            }

            return string.CompareOrdinal(config1.name, config2.name);
        }
    }
}