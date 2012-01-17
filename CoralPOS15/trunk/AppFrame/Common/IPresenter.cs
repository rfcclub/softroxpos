using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppFrame.Common
{
    public interface IPresenter : INotifyPropertyChanged
    {
        IView View { get; set; }
        //IViewModel ViewModel { get; set; }
    }
}
