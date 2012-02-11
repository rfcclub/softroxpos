using System.ComponentModel;

namespace AppFrame.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string propertyName);
    }
}
