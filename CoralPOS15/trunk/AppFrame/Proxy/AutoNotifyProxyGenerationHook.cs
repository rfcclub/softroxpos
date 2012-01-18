using System;
using System.Reflection;
using Castle.DynamicProxy;
using AppFrame.Common.Attributes;
using AppFrame.Binding;

namespace AppFrame.Proxy
{
    public class AutoNotifyProxyGenerationHook : IProxyGenerationHook
    {
        private const string SetPrerix = "set_";
        private const string GetPrefix = "get_";

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            if (NotProperty(methodInfo)) return false;

            if (IsDecoratedWithDoNotNotifyChangesAttribute(methodInfo)) return false;

            //if (IsCommandProperty(methodInfo)) return false;

            return true;
        }        
 
        private bool NotProperty(MethodBase methodInfo)
        {
            return !methodInfo.IsSpecialName || 
                   !(methodInfo.Name.StartsWith(SetPrerix) || methodInfo.Name.StartsWith(GetPrefix));
        }

        //private bool IsCommandProperty(MethodInfo methodInfo)
        //{
        //    var pi = GetPropertyInfoForSetterMethod(methodInfo);
        //    return (pi != null && pi.PropertyType.IsAssignableFrom(typeof(ICommand)));
        //}

        private bool IsDecoratedWithDoNotNotifyChangesAttribute(MethodInfo methodInfo)
        {
            var pi = GetPropertyInfoForSetterMethod(methodInfo);
            return (pi != null && pi.GetAttribute<DoNotNotifyChangesAttribute>() != null);
        }

        private PropertyInfo GetPropertyInfoForSetterMethod(MethodInfo methodInfo)
        {
            var propertyName = methodInfo.Name.Substring(SetPrerix.Length);
            return methodInfo.DeclaringType.GetProperty(propertyName);
        }

        public void MethodsInspected()
        {
            // RESERVED
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
            // RESERVED
        }

        #region IProxyGenerationHook Members

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            // RESERVED
        }

        #endregion
    }
}
