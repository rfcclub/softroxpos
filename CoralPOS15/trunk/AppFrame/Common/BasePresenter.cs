using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common.Attributes;

namespace AppFrame.Common
{
    public class BasePresenter : IPresenter
    {
        private IView _view;

        private IViewModel _viewModel;

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

        public IViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        

        public void StartUp()
        {
            Type type = GetType();
            object[] viewModelAttributes = type.GetCustomAttributes(typeof(ViewModelAttribute), false);
            object[] viewAttributes = type.GetCustomAttributes(typeof(ViewAttribute), false);
            if (viewModelAttributes.Count() > 0)
            {
                ViewModelAttribute attribute = (ViewModelAttribute)viewModelAttributes[0];
                Type vmType = attribute.AttachViewModel;
                object obj = Activator.CreateInstance(vmType);
                ViewModel = (IViewModel)obj;
            }

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
