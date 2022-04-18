using System;
using System.Linq;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public static class ApplicationService
    {
        private static IApplication _current;

        internal static IApplication Current
        {
            get => _current;
            set
            {
                if (_current != null)
                    _current.ModalPopped -= OnModalPopped;

                _current = value;
                _current.ModalPopped += OnModalPopped;
            }
        }

        internal static IBehaviorInjector BehaviorInjector { get; set; } = new BehaviorInjector();
        
        public  static void Initialize(Application application)
        {
            Current = new ApplicationAdapter(application);
        }

        public static void SetMainPage(Page page)
        {
            SetMainPage<object>(page);
        }
        
        public static void SetMainPage<T>(Page page, T parameter = default(T))
        {
			BehaviorInjector.Inject(page);

            page.OnInitialize(parameter);
            Current.MainPage = page;
            page.OnLoaded();
        }

        private static void OnModalPopped(object sender, ModalPoppedEventArgs e)
        {
            var currentPage = Current.MainPage.Navigation.ModalStack.Last();
            currentPage.OnLoaded();
            e.Modal.OnClosed();
        }
        
        internal interface IApplication
        {
            event EventHandler<ModalPoppedEventArgs> ModalPopped;
            Page MainPage { get; set; }
        }

        internal class ApplicationAdapter : IApplication
        {
            private readonly Application _currentApplication;

            Page IApplication.MainPage
            {
                get => _currentApplication.MainPage;
                set => _currentApplication.MainPage = value;
            }

            public event EventHandler<ModalPoppedEventArgs> ModalPopped;
            public ApplicationAdapter(Application currentApplication)
            {
                if(currentApplication == null) return;

                _currentApplication = currentApplication;
                _currentApplication.ModalPopped += (sender, args) => ModalPopped?.Invoke(sender, args);
            }
        }
        
    }
}