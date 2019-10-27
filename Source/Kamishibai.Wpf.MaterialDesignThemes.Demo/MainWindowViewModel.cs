using System;
using System.Windows.Input;
using RedSheeps.Input;

namespace Kamishibai.Wpf.MaterialDesignThemes.Demo
{
    public class MainWindowViewModel : IViewModel
    {
        public IPresentationService PresentationService { get; } = new PresentationService();
        public ICommand ShowWindowCommand => new Command(OnShowWindow);
        public ICommand ShowDialogCommand => new Command(OnShowDialog);

        private void OnShowWindow()
        {
            PresentationService.ShowWindowAsync<SecondWindowViewModel>(
                x => x.Message = $"Hello, Second Window! {DateTime.Now}");
        }

        private void OnShowDialog()
        {
            PresentationService.ShowDialogAsync<UserDialogViewModel>(
                x => x.Message = $"Hello, Dialog Host! {DateTime.Now}");
        }
    }
}