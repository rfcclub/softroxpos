using System;
using AppFrame.Core.Attributes;

namespace AppFrame.Core
{
    public class BasePresenter : BindableObject, IPresenter
    {
        private IView _view;

        public ModelBindingCache BindingCache { get; set; }
        //private IViewModel _viewModel;

        public BasePresenter()
        {
            StartUp();
            BindingCache = new ModelBindingCache();
        }

        public IView View
        {
            get { return _view; }
            set
            {
                _view = value;
            }
        }

        public void StartUp()
        {
            Type type = GetType();
            object[] viewModelAttributes = type.GetCustomAttributes(typeof(ViewModelAttribute), false);
            object[] viewAttributes = type.GetCustomAttributes(typeof(ViewAttribute), false);
            
            // Dont need to create ViewModel because Presenter is merged to ViewModel
            //if (viewModelAttributes.Count() > 0)
            //{
            //    ViewModelAttribute attribute = (ViewModelAttribute)viewModelAttributes[0];
            //    Type vmType = attribute.AttachViewModel;
            //    object obj = Activator.CreateInstance(vmType);
            //    ViewModel = (IViewModel)obj;
            //}

            OnStartUp();            
        }

        public virtual void OnStartUp(){ }

        public void Close()
        {
            OnClose();
        }

        public virtual void OnClose() {}
    }
}
