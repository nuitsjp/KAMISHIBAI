using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf
{
    public class PresentationService : IPresentationService
    {
        private static Func<Type, object> viewProvider = CreateView;
        protected PresentationService(Window window)
        {
            Window = window;
        }

        private Window Window { get; set; }

        public static void Setup(Application application)
        {
            application.Activated += ApplicationOnActivated;
        }

        public static void SetViewProvider(Func<Type, object> provider) => viewProvider = provider;

        private static void ApplicationOnActivated(object sender, EventArgs e)
        {
            var mainWindow = ((Application) sender).MainWindow;
            var dataContext = mainWindow?.DataContext;
            if (dataContext is IViewModel viewModel)
            {
                viewModel.PresentationService = new PresentationService(mainWindow);
            }
        }

        public Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = CreateView<TViewModel>();
            if (newObject is Window window)
            {
                initializeViewModel(window.DataContext as TViewModel);
                window.Show();
                return Task.Run(() => (IView)new View(window));
            }
            else
            {
                throw new ArgumentException($"{typeof(TViewModel)} is not window.");
            }
        }

        public virtual Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = CreateView<TViewModel>();
            if (newObject is Window window)
            {
                initializeViewModel(window.DataContext as TViewModel);
                window.ShowDialog();
                return Task.Run(() => (IView)new View(window));
            }
            else
            {
                throw new ArgumentException($"{typeof(TViewModel)} is not window.");
            }
        }

        protected static object CreateView<TViewModel>() => viewProvider(typeof(TViewModel));

        private static object CreateView(Type viewModelType)
        {
            var viewModelName = viewModelType.FullName;
            var viewName = viewModelName.Substring(0, viewModelName.Length - "ViewModel".Length);

            var viewType = viewModelType.Assembly.GetType(viewName);
            if (viewType == null) throw new ArgumentException($"No matching type found for {viewModelType}");

            return Activator.CreateInstance(viewType);
        }
    }
}