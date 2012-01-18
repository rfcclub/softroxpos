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
using AppFrame.Binding;
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
            }
        }

        protected void BindToViewModel()
        {
            //Type viewModelType = ViewModel.GetType();
            //Type presenterType = Presenter.GetType();
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    BindingHelper.AutoBindMethod(control, Presenter);
                    continue;
                }
                //BindingHelper.AutoBindProperty(control, Presenter);
                BindingHelper.AutoBindDataProperty(control, Presenter);
                //BindingHelper.AutoBindTextBoxProperty(control, ViewModel);
                
            }
            OnBinding();
        }

        public virtual void OnBinding() {}

        public virtual void OnLoaded() {}

        public void DoLoad()
        {

            // bind viewmodel to view
            
            // bind method

            this.Load += BaseViewControlLoad;
        }

        void BaseViewControlLoad(object sender, EventArgs e)
        {
            BindToViewModel();
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
