# NuGet Package Structure and Overview

KAMISHIBAI publishes three packages on NuGet.

|Package|Role|
|--|--|
|Kamishibai|Core library. Includes interfaces for using navigation, etc.|
|Kamishibai.View|Includes classes used in the View layer, such as UI elements used in WPF.|
|Kamishibai.Hosting|Contains classes for hosting KAMISHIBAI on Generic Host.|

Kamishibai.Hosting is transitively dependent on all packages.

Therefore, the easiest way to use it is to add a reference to Kamishibai.Hosting. All functionality will be available.

However, when developing a product, KAMISHIBAI recommends splitting it into several projects, depending on the roles within the application. This is because if all code is placed in a single project, it is difficult to maintain proper management of dependencies between layers.

In KAMISHIBAI's sample "SampleBrowser" application, the project is divided as follows to limit the dependencies of each project.

![](/Images/components.png)

|Package|Role|
|--|--|
|SampleBrowser.ViewModel|Only Kamishibai is referenced; no WPF classes are referenced; ViewModel should not depend on WPF classes for strict adherence to testability and MVVM. However, it is not necessary to strictly adhere to it.|
|SampleBrowser.View|Referring to Kamishibai.View and transitively dependent on Kamishibai.|
|SampleBrowser.Hosting|Describe only the initialization process for hosting the application on Generic Host. All project packages directly or transitively dependent.|

Of course, this does not mean that you should only develop these projects. You may need to add domain projects, for example, as needed. For larger projects, it may be necessary to split the View and ViewModel as well.

Please see the sample code for reference.

# Navigation Overview

The key elements in KAMISHIBAI's navigation are represented below.

![](/Images/architecture.png)

KAMISHIBAI defines a NavigationFrame in the area to be navigated.

Navigation is achieved by displaying an arbitrary UserControl in the NavigationFrame.

A NavigationFrame can have a FrameName, and multiple NavigationFrames can be defined or nested within a single screen.

Navigation is achieved by specifying a FrameName.

```cs
_presentationService.NavigatePage2Async("FrameA");
```

The FrameName must be unique within the application. The default FrameName is an empty string and is optional.

Navigations are used by injecting an IPresentationService into the ViewModel.

IPresentationService is code-generated in the project where the class with the following attributes is located.

- NavigateAttribute
- OpenWindowAttribute
- OpenDialogAttribute

Note that if none of these attributes are used, IPresentationService will not be generated.

Now, let us explain more specifically how to use this service.

[<< Getting Started](02-getting-started.md) | [Configuration of Generic Host >>](04-hosting.md)