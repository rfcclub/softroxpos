using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy;
using AppFrame.Common;
using AppFrame.Common.Attributes;
using AppFrame.Binding;

namespace AppFrame.Proxy
{
    public class AutoNotifyPropertyChangedInterceptor : Castle.DynamicProxy.IInterceptor 
    {
        private const string SetPrerix = "set_";

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            if (!invocation.Method.Name.StartsWith(SetPrerix)) return;
            if (!(invocation.Proxy is IViewModel)) return;

            var methodInfo = invocation.Method;
            var model = (IViewModel)invocation.Proxy;
            model.NotifyPropertyChanged(methodInfo.Name.Substring(SetPrerix.Length));

            ChangeNotificationForDependentProperties(methodInfo, model);
        }

        private void ChangeNotificationForDependentProperties(MethodInfo methodInfo, IViewModel model)
        {
            if (NoAdditionalProperties(methodInfo)) return;

            string[] properties = GetAdditionalPropertiesToChangeNotify(methodInfo);

            foreach (string propertyName in properties)
                model.NotifyPropertyChanged(propertyName);
        }

        private bool NoAdditionalProperties(MethodInfo methodInfo)
        {
            var pi = GetPropertyInfoForSetterMethod(methodInfo);
            return (pi == null || pi.GetAttribute<NotifyChangeForAttribute>() == null);
        }

        private string[] GetAdditionalPropertiesToChangeNotify(MethodInfo methodInfo)
        {
            var pi = GetPropertyInfoForSetterMethod(methodInfo);
            var attribute = pi.GetAttribute<NotifyChangeForAttribute>();
            return attribute.NotifyChangeFor;
        }

        private PropertyInfo GetPropertyInfoForSetterMethod(MethodInfo methodInfo)
        {
            var propertyName = methodInfo.Name.Substring(SetPrerix.Length);
            return methodInfo.DeclaringType.GetProperty(propertyName);
        }
    }  
}
