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

        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual List<UserInf> UserInfo { get; set; }

        public virtual void Login()
        {
            string username = Username;
            string password = Password;
            if(username.Equals("admin") && password.Equals("admin123"))
            {
                AppFrameController.Instance.ShowMessage("Login successfully");
                Username = "UmBo";
                //NotifyPropertyChanged("Username");
                List<UserInf> list = new List<UserInf>();
                for (int i = 0; i < 10; i++)
                {
                    UserInf userInf = new UserInf
                                          {
                                              Username = i.ToString(),
                                              Password = i.ToString()
                                          };
                    list.Add(userInf);
                }
                UserInfo = list;
                
            }
            else
            {
                AppFrameController.Instance.ShowMessage("Login Fail !");
            }
        }
    }

    public class UserInf
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserInf()
        {
        }

        public UserInf(string username,string password)
        {
            Username = username;
            Password = password;
        }
    }
}
