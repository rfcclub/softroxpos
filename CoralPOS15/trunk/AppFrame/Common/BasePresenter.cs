using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common.Attributes;

namespace AppFrame.Common
{
    public class BasePresenter : BindableObject, IPresenter, IViewModel
    {
        private IView _view;

        //private IViewModel _viewModel;

        public BasePresenter()
        {
            StartUp();
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
