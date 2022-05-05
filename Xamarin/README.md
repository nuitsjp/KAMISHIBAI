[Japanese](README-ja.md)

[![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Images/logo_wide.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Xamarin/README.md)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Forms is Page navigation library that supports MVVM patterns.  
KAMISHIBAI will grant the following features to Xamarin.Forms.

* Consistent event notification on Page navigation  
* Generic Type Parameters on Page navigation  
* Compatibility of MVVM Pattern and direct manipulation feeling against INavigation  

And KAMISHIBAI does not limit Xamarin.Forms. All possible Navigation in Xamarin.Forms is also possible in KAMISHIBAI.

# Overview  

The feature of KAMISHIBAI's Navigation Service is that it is not a monolithic structure. Navigation is separated into two roles.

1. Send Page navigation request from View Model to View  
2. Based on Page navigation request, navigation Page on the View layer  

Please see specific examples.  

## Send Page navigation request from View Model to View  

Use INavigationRequest to make Page navigation request.  
When executing Command, it is sending RequestSecondPage request.  

```cs
public ICommand NavigateCommand => new Command(() => RequestSecondPage.RaiseAsync());

public INavigationRequest RequestSecondPage { get; } = new NavigationRequest();
```

## Based on Page navigation request, navigation Page on the View layer  

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage ...>
    <ContentPage.Behaviors>
        <mvvm:PushModalAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
    </ContentPage.Behaviors>
    <StackLayout>
        <Button Text="Navigate to Second" Command="{Binding NavigateCommand}"/>
    </StackLayout>
</ContentPage>
```
Notice the PushModalAsync Behavior.  

```xaml
<mvvm:PushModalAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
```
Based on RequestSecondPage Navigation request, it is defined to make a Modal Navigation to SecondPage.  
PushModalAsync navigation Page with [Kamisibai.Xamarin.Forms.Navigator](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Navigator.cs).  
Navigator wraps Xamarin.Forms.INavigation and adds navigation parameter and navigation event to INavigation.  

## Consistent event notification on Page navigation  

Using KAMISHIBAI, you can receive events with consistency and recursion.

Look at the following code.

```cs
var masterDetailPage = new MasterDetailPage();
masterDetailPage.Master = new ContentPage {Title = "Master"};
var tabbedPage = new TabbedPage();
tabbedPage.Children.Add(new NavigationPage(new ContentPage()){ Title = "Tab 1"});
tabbedPage.Children.Add(new NavigationPage(new ContentPage()){ Title = "Tab 2"});
tabbedPage.Children.Add(new NavigationPage(new ContentPage()){ Title = "Tab 3"});

navigator.PushModalAsync(masterDetailPage);
```

Events can be received on all pages of transition source and destination.  
The number of Page to navigation to... 9？  
Perhaps I will make a mistake. But KAMISHIBAI makes no mistake.  

## Generic Type Parameters on Page navigation  

KAMISHIBAI, pass the parameters on navigation easily and safely.  

An example of passing a DateTime as a parameter.  

```cs
public INavigationRequest<DateTime> RequestSecondPage { get; } = new NavigationRequest<DateTime>();

public Task Navigate(DateTime selectedDate)
{
    return RequestSecondPage.RaiseAsync(selectedDate);
}
```

And receive the following.  
Implement IPageInitializeAware and receive it with OnInitialize method.  

```cs
public class SecondPageViewModel : IPageInitializeAware<DateTime>, INotifyPropertyChanged
{
    ...
    public void OnInitialize(DateTime selectedDate)
    {
        SelectedDate = selectedDate;
    }
```

Were you interested in KAMISHIBAI?  
KAMISHIBAI offers the following content to support you.    
Let's use Xamarin.Forms with the most flexible Page navigation library!  

## Contents

1. [How to install](#how-to-install)
2. Documents
    1. [Hello, KAMISHIBAI](Document/1-Hello-KAMISHIBAI.md)  
    2. [Reference](Document/2-Reference.md)
    2. [Architecture overview](Document/3-Architecture-Overview.md)
3. [Samples](https://github.com/nuitsjp/KAMISHIBAI-Samples)

## How to install  

Install from [NuGet](https://www.nuget.org/packages/Kamishibai.Xamarin.Forms).  

```txt
> Install-Package Kamishibai.Xamarin.Forms
```
