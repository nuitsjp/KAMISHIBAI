namespace SampleBrowser.ViewModel
{
    public class MenuItem
    {
        public MenuItem(string name, Type viewModel)
        {
            Name = name;
            ViewModel = viewModel;
        }

        public string Name { get; }

        public Type ViewModel { get; }
    }
}
