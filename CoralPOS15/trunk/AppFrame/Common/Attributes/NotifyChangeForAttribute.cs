using System;

namespace AppFrame.Common.Attributes
{
    public class NotifyChangeForAttribute : Attribute
    {
        public string[] NotifyChangeFor { get; private set; }

        public NotifyChangeForAttribute(params string[] notifyChangeFor)
        {
            NotifyChangeFor = notifyChangeFor;
        }
    }
}
