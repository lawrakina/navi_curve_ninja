using System;

namespace Core.Meta.Analytics
{
    /// <summary>
    /// позволяет показывать в эдиторе даже не проинициализированные переменные 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TrackableAttribute : Attribute
    {
        public TrackableAttribute(string key)
        {
            CustomName = key;
        }
        
        public string CustomName { get; }
    }
}