using System;

namespace AppFrame.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ToolBarItemAttribute : Attribute
    {
        public string ToolBarImage { get; set; }
        public string ToolBarName { get; set; }
        public bool IsOverride { get; set; }

        public ToolBarItemAttribute(string toolBarName,string toolBarImage = "NoImage",bool isOverride = false)
        {
            ToolBarName = toolBarName;
            ToolBarImage = toolBarImage;
            IsOverride = isOverride;
        }
    }
}
