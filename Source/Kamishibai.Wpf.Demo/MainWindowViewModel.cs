using System;
using System.Diagnostics;
using System.Windows.Input;
using RedSheeps.Input;

namespace Kamishibai.Wpf.Demo
{
    public class MainWindowViewModel : IViewModel
    {
        public MainWindowViewModel()
        {
            Debug.WriteLine("MainWindowViewModel.Ctor()");
        }
        public IPresentationService PresentationService { private get; set; }

        public ICommand ShowWindowCommand => new Command(OnShowWindow);

        public ICommand ShowDialogCommand => new Command(() =>
        {
            PresentationService.ShowDialogAsync<SecondWindowViewModel>(x =>
            {
                Debug.WriteLine($"MainWindowViewModel#ShowDialogAsync() x:{x}");
            });
        });

        private void OnShowWindow()
        {
            PresentationService.ShowWindowAsync<SecondWindowViewModel>(
                x => x.Message = $"Hello, Second Window! {DateTime.Now}");
        }
    }
}