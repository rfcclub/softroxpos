namespace AppFrame.Core
{
    public interface IView
    {
        IPresenter Presenter { get; set; }
        void Show();
        void Hide();
        void Close();
        void DoLoad();
    }
}
