using System;

namespace AppFrame.Utilities
{
    public class AttributeHelper
    {
        public static object[] GetAttribute(Type type,object obj)
        {
            return obj.GetType().GetCustomAttributes(type, false);
        }
        public static object GetFirstAttribute(Type type, object obj)
        {
            object[] objects =  obj.GetType().GetCustomAttributes(type, false);
            if(objects.Length>0)
            {
                return objects[0];
            }
            return null;
            
        }
    }
}
