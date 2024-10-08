﻿using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[Navigate]
[AddINotifyPropertyChangedInterface]
public class WithArgumentsViewModel : IDisposingAware
{
    private readonly IPresentationService _presentationService;

    public WithArgumentsViewModel(
        string message, 
        [Inject] IPresentationService presentationService)
    {
        Message = message;
        _presentationService = presentationService;
    }

    public bool BlockGoBack { get; set; }

    public string AlertMessage { get; set; } = string.Empty;

    public string Message { get; }

    public ICommand GoBackCommand => new AsyncRelayCommand(() => _presentationService.GoBackAsync());

    public void OnDisposing(PreBackwardEventArgs args)
    {
        if (BlockGoBack)
        {
            AlertMessage = "Go back blocked.";
            args.Cancel = true;
        }

        AlertMessage = string.Empty;
    }
}