using System;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade.Modules.Configuration.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubConfigAttribute : ConfigAttribute
    {
        public ObjectConfig Parent { get; }
        
        public SubConfigAttribute(ObjectConfig parent, string name, int order = -1) : base(name, order)
        {
            Parent = parent;
        }
    }
}