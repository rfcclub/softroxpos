using System;

namespace AppFrame.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class ViewModelAttribute : Attribute
    {
        public Type AttachViewModel { get; set; }
        public ViewModelAttribute(Type type)
        {
            AttachViewModel = type;
        }
    }
}
