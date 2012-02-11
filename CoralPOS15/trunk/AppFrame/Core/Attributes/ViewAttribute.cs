using System;

namespace AppFrame.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewAttribute : Attribute
    {
        public Type ViewScreen { get; set; }
        public ViewAttribute(Type type)
        {
            ViewScreen = type;
        }
    }
}
