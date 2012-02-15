using System;
using System.Reflection;
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
            AppFrameController.Instance.ScanToolBar(Assembly.GetEntryAssembly());
            AppFrameController.Instance.ScanToolBar(Assembly.GetExecutingAssembly());
            AppFrameController.Instance.ScanToolBar(Assembly.GetCallingAssembly());
            AppFrameController.Instance.ScanToolBar(AppDomain.CurrentDomain.GetAssemblies());
            AppFrameController.Instance.UpdateToolBar(this.mainToolStrip);
        }

        public void OnClose()
        {
            // do nothing
        }
    }
}
