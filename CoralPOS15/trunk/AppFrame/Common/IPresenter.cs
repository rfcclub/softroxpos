using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFrame.Common
{
    public interface IPresenter
    {
        IView View { get; set; }
        IViewModel ViewModel { get; set; }
    }
}
