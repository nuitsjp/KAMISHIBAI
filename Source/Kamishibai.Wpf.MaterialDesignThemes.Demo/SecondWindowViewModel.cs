using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kamishibai.Wpf.MaterialDesignThemes.Demo
{
    public class SecondWindowViewModel : ViewModelBase
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