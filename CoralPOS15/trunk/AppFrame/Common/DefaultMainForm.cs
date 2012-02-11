using System;
using System.Windows.Forms;
using AppFrame.Core;

namespace AppFrame.Common
{
    public partial class DefaultMainForm : Form,IMainForm
    {
        public DefaultMainForm()
        {
            InitializeComponent();
            OnCreate();
        }

        public void OnCreate()
        {
            AppFrameController.Instance.ContainerControl = this.mainPanel;
            AppFrameController.Instance.ScanToolBar(this.GetType().Assembly);
            AppFrameController.Instance.ScanToolBar(AppDomain.CurrentDomain.GetAssemblies());
            AppFrameController.Instance.UpdateToolBar(this.GetType().Assembly,this.mainToolStrip);
        }

        public void OnClose()
        {
            // do nothing
        }
    }
}
