using System;

namespace AppFrame.Common.Attributes
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
