using System.ComponentModel;

namespace AppFrame.Core
{
    public interface IPresenter : INotifyPropertyChanged
    {
        IView View { get; set; }
        //IViewModel ViewModel { get; set; }
    }
}
