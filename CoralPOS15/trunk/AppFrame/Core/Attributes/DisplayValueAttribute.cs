using System;

namespace AppFrame.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DisplayValueAttribute : Attribute
    {
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }

        public DisplayValueAttribute(string displayMember,string valueMember)
        {
            DisplayMember = displayMember;
            ValueMember = valueMember;
        }
    }
}
