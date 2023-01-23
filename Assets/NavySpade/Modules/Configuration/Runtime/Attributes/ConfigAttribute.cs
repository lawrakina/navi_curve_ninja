using System;

namespace NavySpade.Modules.Configuration.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute : Attribute, IComparable
    {
        private const int UndefinedOrderId = -1;
        
        public string Name { get; }
        private int OrderId { get; }

        public ConfigAttribute(string name, int order = UndefinedOrderId)
        {
            Name = name;
            OrderId = order;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ConfigAttribute otherAttribute) || otherAttribute.OrderId == UndefinedOrderId)
            {
                return -1;
            }

            if (OrderId == otherAttribute.OrderId)
            {
                return string.CompareOrdinal(Name, otherAttribute.Name);
            }
            
            return OrderId > otherAttribute.OrderId ? 1 : -1;
        }
    }
}