using System;
using Castle.DynamicProxy;

namespace AppFrame.Proxy
{
    public class AutoNotifyPropertyChangedProxyCreator
    {
        private readonly ProxyGenerator _proxyGenerator;
        private readonly ProxyGenerationOptions _options;
        private readonly AutoNotifyPropertyChangedInterceptor _interceptor;

        public AutoNotifyPropertyChangedProxyCreator()
        {
            _options = new ProxyGenerationOptions(new AutoNotifyProxyGenerationHook());
            _interceptor = new AutoNotifyPropertyChangedInterceptor();
            _proxyGenerator = new ProxyGenerator(new PersistentProxyBuilder());            
        }

        public TViewModel Create<TViewModel>(params object[] constructorParameters)
        {
            return (TViewModel)Create(typeof (TViewModel), constructorParameters);
        }

        public object Create(Type type, params object[] constructorParameters)
        {
            return _proxyGenerator.CreateClassProxy(type, _options, constructorParameters, _interceptor);
        }
    }
}
