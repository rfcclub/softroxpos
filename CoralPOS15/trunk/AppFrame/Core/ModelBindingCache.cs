using System.Collections.Generic;
using System.Windows.Forms;

namespace AppFrame.Core
{
    public class ModelBindingCache
    {
        IDictionary<string, System.Windows.Forms.BindingSource> _bindMap;        
        
        public ModelBindingCache()
        {            
            _bindMap = new Dictionary<string, System.Windows.Forms.BindingSource>();
        }
        public ModelBindingCache(object model, IDictionary<string, System.Windows.Forms.BindingSource> bindMap)
        {            
            _bindMap = bindMap;
        }

        public void Put(string bindingProp,BindingSource binding)
        {
            _bindMap[bindingProp] = binding;
        }

        public BindingSource Get(string bindingProp)
        {
            if (!_bindMap.ContainsKey(bindingProp)) return null;
            return _bindMap[bindingProp];
        }

        public void Clear()
        {
            _bindMap.Clear();
        }

        public void Open()
        {
            _bindMap = new Dictionary<string, System.Windows.Forms.BindingSource>();
        }
        public void Close()
        {        
            _bindMap.Clear();
            _bindMap = null;
        }

        public bool IsClose()
        {
            return _bindMap == null;
        }
    }
}
