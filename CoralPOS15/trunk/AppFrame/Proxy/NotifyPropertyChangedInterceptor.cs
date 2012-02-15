using System.Reflection;
using AppFrame.Core;
using AppFrame.Core.Attributes;
using Castle.Core;
using Castle.DynamicProxy;
using AppFrame.Common;
using AppFrame.Binding;
using AopAlliance.Intercept;
using System.Collections;

namespace AppFrame.Proxy
{
    public class NotifyPropertyChangedInterceptor : IMethodInterceptor
    {
        private const string SetPrefix = "set_";        

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
            var propertyName = methodInfo.Name.Substring(SetPrefix.Length);
            return methodInfo.DeclaringType.GetProperty(propertyName);
        }

        #region IMethodInterceptor Members

        public object Invoke(IMethodInvocation invocation)
        {
            object obj = invocation.Proceed();            
            if (!invocation.Method.Name.StartsWith(SetPrefix)) return obj;
            if (!(invocation.Proxy is IViewModel)) return obj;            
            var methodInfo = invocation.Method;
            //var model = (IViewModel)invocation.Proxy;
            var model = (IViewModel)invocation.Proxy;
            model.NotifyPropertyChanged(methodInfo.Name.Substring(SetPrefix.Length));
            ChangeNotificationForDependentProperties(methodInfo, model);
            if (invocation.Proxy is BasePresenter)
            {
                ModelBindingCache cache = ((BasePresenter)invocation.Proxy).BindingCache;
                System.Windows.Forms.BindingSource binding = cache.Get(methodInfo.Name.Substring(SetPrefix.Length));
                if (binding != null)
                {
                    string dataMember = binding.DataMember;
                    object objects = binding.DataSource;
                    binding.DataSource = null;

                    binding.DataSource = objects;
                    binding.DataMember = dataMember;
                }
            }
            return obj;
        }

        #endregion
    }  
}
