using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Common;
using AppFrame.Common.Attributes;

namespace AppFrameDemo.Module.Login
{
    [View(typeof(LoginView))]
    [ViewModel(typeof(LoginViewModel))]
    [ToolBarItem("Login")]
    public class LoginPresenter : BasePresenter
    {
        public void Login()
        {
            
            LoginViewModel loginViewModel = (LoginViewModel) ViewModel;
            string username = loginViewModel.Username;
            string password = loginViewModel.Password;
            if(username.Equals("admin") && password.Equals("admin123"))
            {
                AppFrameController.Instance.ShowMessage("Login successfully");
            }
            else
            {
                AppFrameController.Instance.ShowMessage("Login Fail !");
            }
        }
    }
}
