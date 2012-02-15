using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Core;

namespace AppFrameDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            OnCreate();
        }

        private void OnCreate()
        {
            AppFrameController.Instance.ContainerControl = this.mainPanel;
            AppFrameController.Instance.ScanToolBar(this.GetType().Assembly);
            AppFrameController.Instance.ScanToolBar(AppDomain.CurrentDomain.GetAssemblies());
            AppFrameController.Instance.ScanAndUpdateToolBar(this.GetType().Assembly,this.mainToolStrip);
        }
    }
}
