namespace Kamishibai.Wpf.MaterialDesignThemes.Demo
{
    public class UserDialogViewModel : ViewModelBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
    }
}