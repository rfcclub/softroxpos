using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrameDemo.Module.Login;
using Spring.Context;
using Spring.Context.Support;

namespace AppFrameDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // spring config
            System.Activator.CreateInstance(typeof (LoginPresenter));
            IApplicationContext ctx = ContextRegistry.GetContext();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
