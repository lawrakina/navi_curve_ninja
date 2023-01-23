using System.Collections.Generic;
using System.Reflection;
using NavySpade.Modules.Configuration.Runtime.Attributes;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Extensions.CsharpTypes;

namespace NavySpade.Modules.Configuration.Editor.Utils
{
    internal static class ObjectConfigCategoryUtility
    {
        internal static Dictionary<string, List<ObjectConfig>> SortConfigsByCategory(IEnumerable<ObjectConfig> configs)
        {
            var result = new Dictionary<string, List<ObjectConfig>>();
            
            foreach (var config in configs)
            {
                var type = config.GetType();
                var category = GetConfigCategory(type);

                if (result.ContainsKey(category) == false)
                {
                    result.Add(category, new List<ObjectConfig>());
                }

                result[category].Add(config);
            }

            return result;
        }

        internal static IEnumerable<string> GetAllCategories(IEnumerable<ObjectConfig> configs)
        {
            var categories = new List<string>();
            
            foreach (var config in configs)
            {
                var type = config.GetType();
                var category = GetConfigCategory(type);

                if (categories.Contains(category) == false)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }
        
        private static string GetConfigCategory(MemberInfo info)
        {
            var category = ConfigGroupAttribute.DefaultGroup;
            if (info.GetCustomAttribute(typeof(ConfigGroupAttribute)) is ConfigGroupAttribute attribute)
            {
                category = attribute.Name;
            }

            return category;
        }
    }
}