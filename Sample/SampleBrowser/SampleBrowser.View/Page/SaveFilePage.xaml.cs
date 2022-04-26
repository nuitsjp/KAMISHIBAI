using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// OpenFilePage.xaml の相互作用ロジック
/// </summary>
public partial class SaveFilePage
{
    public SaveFilePage()
    {
        InitializeComponent();
    }
}

public class DesignSaveFilePageViewModel : SaveFileViewModel
{
    public DesignSaveFilePageViewModel() : base(default!)
    {
    }
}