using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AppFrame.Utilities;

namespace AppFrame.Common
{
    public partial class BaseView : UserControl, IView
    {
        public BaseView()
        {
            InitializeComponent();
        }

        private BindingSource ControlBindingSource { get; set; }
        private IPresenter _presenter;
        public IPresenter Presenter
        {
            get { return _presenter; }
            set
            {
                _presenter = value;
                _presenter.View = this;
                ViewModel = _presenter.ViewModel;
            }
        }

        private IViewModel _viewModel;

        public IViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                Binding();
            }
        }

        private void Binding()
        {
            OnBinding();
        }

        private void OnBinding()
        {
            //Type viewModelType = ViewModel.GetType();
            //Type presenterType = Presenter.GetType();
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    BindingHelper.AutoBindMethod(control,Presenter);
                    continue;
                }
                //BindingHelper.AutoBindProperty(control, ViewModel);
                BindingHelper.AutoBindDataProperty(control, ViewModel);
                //BindingHelper.AutoBindTextBoxProperty(control, ViewModel);
            }
        }

        public virtual void OnLoaded() {}

        public void DoLoad()
        {

            // bind viewmodel to view
            
            // bind method

            this.Load += new EventHandler(BaseViewControlLoad);
        }

        void BaseViewControlLoad(object sender, EventArgs e)
        {
            OnLoaded();
        }

        public void Close()
        {
            Dispose();
            OnClosed();
        }

        public virtual void OnClosed() { }
    }
}
