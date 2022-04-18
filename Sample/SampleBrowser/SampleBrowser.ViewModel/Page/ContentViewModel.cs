﻿using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class ContentViewModel
{
    private readonly IPresentationService _presentationService;

    public ContentViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string Message { get; set; } = "Default Message";
    public ICommand GoBackCommand => new AsyncRelayCommand(() => _presentationService.GoBackAsync());
}