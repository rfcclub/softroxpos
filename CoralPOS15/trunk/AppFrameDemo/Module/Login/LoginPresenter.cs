using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using AppFrame.Common.Attributes;

namespace AppFrameDemo.Module.Login
{
    [View(typeof(LoginView))]
    [ToolBarItem("Login")]
    public class LoginPresenter : BasePresenter
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public void Login()
        {
            
            
            string username = Username;
            string password = Password;
            if(username.Equals("admin") && password.Equals("admin123"))
            {
                AppFrameController.Instance.ShowMessage("Login successfully");
                Username = "UmBo";
                //NotifyPropertyChanged("Username");
            }
            else
            {
                AppFrameController.Instance.ShowMessage("Login Fail !");
            }
        }
    }
}
