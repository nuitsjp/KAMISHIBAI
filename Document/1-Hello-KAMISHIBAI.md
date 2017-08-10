[[Home]](../README-ja.md) > Hello, KAMISHIBAI

# Hello, KAMISHIBAI  

Apply KAMISHIBAI to Xamarin.Forms project , create a simple sample application. Purpose is to understand the following.  

* How to apply KAMISHIBAI to applications  
* Implementation of Page navigation with type safe arguments  
   
# Contents

* [Precondition](#precondition)  
* [Overview of the application](#overview-of-the-application)  
* [Steps to create](#steps-to-create)  

# Precondition  

Here we assume that you meet the following.  

* Xamarin development environment is already built  
* You have created an application with one or more Page navigation in Xamarin.Forms  
* You have created an application that binds View Model properties to view

# Overview of the application  

Create an application as follows.  

![](1-Hello-KAMISHIBAI/008.gif)

On the initial Page ListView displays the list of Fruit.  
When the Fruit is selected, Page navigation to show the selected Fruit.  

[The completion code is here.](https://github.com/nuitsjp/KAMISHIBAI-Samples)  
Look at ListViewSample.  


# Steps to create  

1. [Create Xamarin.Forms project](#create-xamarinforms-project)  
2. [Install KAMISHIBAI for Xamarin.Forms from NuGet](#install-kamishibai-for-xamarinforms-from-nuget)  
3. [Create Fruit list Page and change initial Page navigation](#create-fruit-list-page-and-change-initial-page-navigation)  
4. [Implementation of Page after selecting a Fruit and navigation](#implementation-of-page-after-selecting-a-fruit-and-navigation)  
5. [Application Fine Fix](#application-fine-fix)  

## Create Xamarin.Forms project  

Let's select "Cross-Platform" > "Cross Platform App (Xamarin)" from the project template and create the name as "ListViewSample".  

If you do, it will be successful if the following Page is displayed.  

<img src="https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Document/1-Hello-KAMISHIBAI/002.png" width="300"/>

## Install KAMISHIBAI for Xamarin.Forms from NuGet  

Next, install the KAMISHIBAI from NuGet.  
KAMISHIBAI is created in .NET Standard. Associated libraries should be installed.  

## Create Fruit list Page and change initial Page navigation  

Let's start creating the application.    
First, create the Views folder and the ViewModels folder under the ListViewSample project.    

Then add the first page to the Views folder.  
Let's create FruitsListPage.xaml.  

After creation, let's add the title "Fruits List" to the open XAML.  

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ListViewSample.Views.FruitsListPage"
             Title="Fruits List">
    ...
```

And let's fix the KAMISHIBAI initialization and initial Page navigation.   
Please open the App.xaml.cs file.  
The constructor contains the following code.  

```cs
public App()
{
    InitializeComponent();

    MainPage = new ListViewSample.MainPage();
}
```

This is fixed as follows.    

```cs
public App()
{
    InitializeComponent();

    ApplicationService.Initialize(this);
    ApplicationService.SetMainPage(new NavigationPage(new FruitsListPage()));
}
```

Now, let's run the app. You should see it as follows.    

<img src="https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Document/1-Hello-KAMISHIBAI/005.jpg" width="300"/>

KAMISHIBAI could be incorporated.    

So will implement the contents of the FruitsListPage.  
First, create the following three classes in the ViewModels folder:    

1. ViewModelBase
2. Fruit
3. FruitsListPageViewModel

ViewModelBase will be the base class of ViewModel, which implemented INotifyPropertyChanged.    

```cs
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(field, value)) return false;

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
```

The Fruit class is the item to display in the list.  
Implements the name and color as a property.  

```cs
public class Fruit
{
    public string Name { get; set; }
    public Color Color { get; set; }
}
```

It is FruitsListPageViewModel.  
This time, the contents of the list are fixed.    

```cs
public class FruitsListPageViewModel : ViewModelBase
{
    public IList<Fruit> Fruits { get; } = new List<Fruit>();

    public FruitsListPageViewModel()
    {
        Fruits.Add(new Fruit { Name = "Apple", Color = Color.Red });
        Fruits.Add(new Fruit { Name = "Orange", Color = Color.Orange });
        Fruits.Add(new Fruit { Name = "Pineapple", Color = Color.Orange });
        Fruits.Add(new Fruit { Name = "Banana", Color = Color.Goldenrod });
        Fruits.Add(new Fruit { Name = "Peach", Color = Color.DeepPink });
        Fruits.Add(new Fruit { Name = "Mango", Color = Color.Goldenrod });
        Fruits.Add(new Fruit { Name = "Melon", Color = Color.Green });
        Fruits.Add(new Fruit { Name = "Grape", Color = Color.Purple });
        Fruits.Add(new Fruit { Name = "Strawberry", Color = Color.Red });
    }
}
```

Last modified FruitsListPage.xaml. Displays the contents of the ViewModel to ListView.    
Sets the FruitsListPageViewModel to the BindingContext. And displays the contents of the Fruits of the FruitsListPageViewModel in the ListView.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:ListViewSample.ViewModels;assembly=ListViewSample"
             x:Class="ListViewSample.Views.FruitsListPage"
             Title="Fruits List">
    <ContentPage.BindingContext>
        <viewModels:FruitsListPageViewModel/>
    </ContentPage.BindingContext>
    <ListView ItemsSource="{Binding Fruits}">
        <ListView.ItemTemplate>
            <DataTemplate>
                <TextCell Text="{Binding Name}" TextColor="{Binding Color}"/>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
</ContentPage>
```

When you do this, it appears as follows:  

<img src="https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Document/1-Hello-KAMISHIBAI/006.jpg" width="300"/>

Once Fruitslistpage is finished, let's create the next Page.  

## Implementation of Page after selecting a Fruit and navigation  

First, let's create from ViewModel. Under the ViewModels folder, create a FruitDetailPageViewModel class.  
Creates a property to display the selected fruit.  

```cs
public class FruitDetailPageViewModel : ViewModelBase
{
    private Fruit _fruit;
    public Fruit Fruit
    {
        get => _fruit;
        set => SetProperty(ref _fruit, value);
    }
}
```

It continues to create an FruitDetailPage.xaml under the Views folder. And be implemented as follows.  

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:ListViewSample.ViewModels;assembly=ListViewSample"
             x:Class="ListViewSample.Views.FruitDetailPage"
             Title="Fruit Detail">
    <ContentPage.BindingContext>
        <viewModels:FruitDetailPageViewModel/>
    </ContentPage.BindingContext>
    <StackLayout Orientation="Horizontal" HorizontalOptions="Center" VerticalOptions="Center">
        <BoxView HeightRequest="10" WidthRequest="10" Color="{Binding Fruit.Color}"/>
        <Label Text="{Binding Fruit.Name}"/>
    </StackLayout>
</ContentPage>
```

FruitDetailPageViewModel is bound to BindingContext. And you can see that the Fruit property is bound to Boxview and label.

Now let's finally navigation is the implementation.  
ViewModel the navigation source page, fix the FruitsListPageViewModel.  
Adds properties to the INavigationRequest make Page navigation request.  
This time, Pass the Fruit during Page navigation selected as a parameter. Therefore, you define the type parameters in the form of Fruit.

```cs
public INavigationRequest<Fruit> RequestDetail { get; } = new NavigationRequest<Fruit>();
```

Add code to call the RequestDetail, when then Fruit has been selected.  

```cs
private Fruit _selectedFruit;
public Fruit SelectedFruit
{
    get => _selectedFruit;
    set
    {
        if (SetProperty(ref _selectedFruit, value) && value != null)
        {
            RequestDetail.RaiseAsync(value);
        }
    }
}
```

Only this code to request navigation. Page navigation are not. Page navigation process is described on the XAML side. Open the FruitsListPage.xaml.  

Change the following two points.  

1. Adding PushAsync Behaviors for Page Transitions  
2. Bind SelectedFruit to a ListView SelectedItem

```cs
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:ListViewSample.ViewModels;assembly=ListViewSample"
             xmlns:mvvm="clr-namespace:Kamishibai.Xamarin.Forms.Mvvm;assembly=Kamishibai.Xamarin.Forms"
             xmlns:views="clr-namespace:ListViewSample.Views;assembly=ListViewSample"
             x:Class="ListViewSample.Views.FruitsListPage"
             Title="Fruits List">
    <ContentPage.BindingContext>
        <viewModels:FruitsListPageViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Behaviors>
        <mvvm:PushAsync Request="{Binding RequestDetail}" x:TypeArguments="views:FruitDetailPage" />
    </ContentPage.Behaviors>
    <ListView ItemsSource="{Binding Fruits}" 
              SelectedItem="{Binding SelectedFruit, Mode=TwoWay}">
        ...
```

Following code is extracted only a part of the Behavior to execute PushAsync.  

```cs
<mvvm:PushAsync Request="{Binding RequestDetail}" x:TypeArguments="views:FruitDetailPage" />
```

You'll understand that is defined by using the PushAsync method from RequestDetail Page navigation requests as they occur, the navigation to FruitDetailPage.  

You can now navigation to Page, but you must receive the selected Fruit parameter in the navigation destination.    
Parameter is possible to receive by implementing the OnInitialize method of the IPageInitializeAware interface.  
Open FruitDetailPageViewModel and implement IPageInitializeAware&lt;Fruit>. 

```cs
public class FruitDetailPageViewModel : ViewModelBase, IPageInitializeAware<Fruit>
{
    public void OnInitialize(Fruit fruit) => Fruit = fruit;
```

Now, the Page navigation has been implemented.  

## Application Fine Fix  

It looks well, completed in the implementation in the future. But looking back the Page and the selected rows remain selected. Also, the selected row cannot be re-selected.  

![](1-Hello-KAMISHIBAI/007.gif)

Clear the selection state. Let's use the FruitsListPage exit event this time. FruitsListPageViewModel implements IPageUnloadedAware.

```cs
public class FruitsListPageViewModel : ViewModelBase, IPageUnloadedAware
{
    public void OnUnloaded() => SelectedFruit = null;
```

After becoming a non-selective State continuously, you can select the same row.  

![](1-Hello-KAMISHIBAI/008.gif)

In this way, KAMISHIBAI includes a function to notify the event at the necessary timing of the Page navigation.
[Completed code is available here ListViewSample.](https://github.com/nuitsjp/KAMISHIBAI-Samples)

This is the end of the introductory.  
Now let's look at the [reference](2-Reference-ja.md) and actually create the application.  
