using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Meta.Analytics.Editor
{
    public static class AttributeCollector
    {
        public static NonInstalledVariableData[] GetDatas()
        {
            var propreties = GetTypesWithAttribute<TrackableAttribute>().ToArray();
            var array = new NonInstalledVariableData[propreties.Length];


            for (var i = 0; i < propreties.Length; i++)
            {
                var propertyInfo = propreties[i];
                array[i] = new NonInstalledVariableData
                {
                    Type = ParseType(propertyInfo.DeclaringType),
                    DefaultKey = propertyInfo.GetCustomAttribute<TrackableAttribute>().CustomName
                };
            }

            return array;
        }

        private static TrackingVariableType ParseType(Type type)
        {
            if (type == typeof(int))
                return TrackingVariableType.Int;
            else if (type == typeof(float))
            {
                return TrackingVariableType.Float;
            }

            return TrackingVariableType.Int;
        }

        private static IEnumerable<MemberInfo> GetTypesWithAttribute<T>() where T : Attribute
        {
            var assembly = Assembly.GetAssembly(typeof(T));
            foreach (var type in assembly.GetTypes())
            {
                var properties = type.GetTypeInfo().DeclaredFields
                    .Where(prop => prop.GetCustomAttributes(typeof(T), false).Length > 0).ToArray();

                foreach (var property in properties)
                {
                    yield return property;
                }
            }
        }
    }
}