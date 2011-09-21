using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFrame.Common
{
    public interface IView
    {
        IPresenter Presenter { get; set; }
        IViewModel ViewModel { get; set; }
        void Show();
        void Hide();
        void Close();
        void DoLoad();
    }
}
