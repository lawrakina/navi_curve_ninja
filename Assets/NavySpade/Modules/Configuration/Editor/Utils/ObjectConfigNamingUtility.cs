using System.Reflection;
using System.Text.RegularExpressions;
using NavySpade.Modules.Configuration.Runtime.Attributes;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade.Modules.Configuration.Editor.Utils
{
    internal static class ObjectConfigNamingUtility
    {
        internal static string GetInspectorName(ObjectConfig config)
        {
            var type = config.GetType();
            if (TryGetConfigNameFromAttribute(type, out var configName) == false)
            {
                configName = type.Name;
            }

            var nameWithSpaces = Regex.Replace(configName, "([a-z])([A-Z])", "$1 $2");
            var finalName = EditorConfig.Instance.StripConfigTypeFromName
                ? nameWithSpaces.Replace("Config", "")
                : nameWithSpaces;

            return finalName;
        }

        private static bool TryGetConfigNameFromAttribute(MemberInfo info, out string configName)
        {
            if (info.GetCustomAttribute(typeof(ConfigAttribute)) is ConfigAttribute attribute)
            {
                configName = attribute.Name;
                return true;
            }

            configName = string.Empty;
            return false;
        }
    }
}