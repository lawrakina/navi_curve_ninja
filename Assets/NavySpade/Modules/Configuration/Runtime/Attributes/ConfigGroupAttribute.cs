using System;

namespace NavySpade.Modules.Configuration.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigGroupAttribute : Attribute
    {
        public const string DefaultGroup = "Core";

        public string Name { get; }

        public ConfigGroupAttribute(string name = DefaultGroup)
        {
            Name = name;
        }
    }
}