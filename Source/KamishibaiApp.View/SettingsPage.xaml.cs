using System.Windows.Controls;
using KamishibaiApp.App;
using KamishibaiApp.Application;
using KamishibaiApp.ViewModel;
using Microsoft.Extensions.Options;

namespace KamishibaiApp.View
{
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }

    public class DesignSettingsViewModel : SettingsViewModel
    {
        public DesignSettingsViewModel() 
            : base(new Option<AppConfig>(), default!, default!, default!)
        {
        }

        public class Option<TValue> : IOptions<TValue> where TValue : class
        {
            public TValue Value { get; } = default!;
        }
    }
}
