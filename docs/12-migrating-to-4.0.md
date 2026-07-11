# Migrating from KAMISHIBAI 3.x to 4.0

KAMISHIBAI 4.0 contains breaking changes in file dialog dependency injection and package dependencies. The existing `IPresentationService`, `IPresentationServiceBase`, `IWindowService`, `OpenFileDialogContext`, and `SaveFileDialogContext` interfaces remain unchanged, but upgrading without reviewing the following items can change runtime behavior.

## Breaking changes

### `WindowService` requires `IFileDialogService`

`WindowService` now receives an `IFileDialogService` through its constructor. Applications that use `KamishibaiApplicationBuilder.Build()` receive the default registration automatically. Applications that instantiate `WindowService` directly or construct their own service graph must register and supply `IFileDialogService`.

```cs
services.AddSingleton<IFileDialogService, MyFileDialogService>();
```

### The default file dialog implementation has changed

KAMISHIBAI 3.x used `Microsoft-WindowsAPICodePack-Shell`. KAMISHIBAI 4.0 uses a direct wrapper over the public Windows Common Item Dialog COM API by default. Calls through `IPresentationService.OpenFile`, `IPresentationService.SaveFile`, and `IWindowService` do not need to change, but subtle differences in native validation, exception details, and option behavior may exist.

The direct implementation has these known limitations:

- `AllowPropertyEditing` is not applied because the public Common Item Dialog API has no corresponding switch.
- `IsExpandedMode` is mapped to `FOS_DEFAULTNOMINIMODE`, which Windows 7 and later do not support.
- Windows itself can change dialog presentation and behavior between operating system versions.

### The Windows API Code Pack is no longer a transitive dependency

`Kamishibai.View` no longer depends on `Microsoft-WindowsAPICodePack-Shell`. Applications that used Code Pack types through KAMISHIBAI's transitive dependency must add their own explicit dependency or install the compatibility package described below.

## Preserving the KAMISHIBAI 3.x behavior

Install the compatibility package when the existing Code Pack implementation, including its existing quirks and exception behavior, must be preserved.

```powershell
dotnet add package Kamishibai.FileDialogs.WindowsApiCodePack --version 4.0.0
```

Register it before calling `Build()`.

```cs
builder.Services.AddWindowsApiCodePackFileDialogs();
var app = builder.Build();
```

Installing the package without calling `AddWindowsApiCodePackFileDialogs()` does not change the active implementation.

The compatibility package uses the same `OpenFile` and `SaveFile` implementation as KAMISHIBAI 3.1.0 and pins `Microsoft-WindowsAPICodePack-Shell` to version 1.1.4. It therefore restores that package as a transitive dependency. Consumers must review and accept the [Windows API Code Pack license terms](https://github.com/contre/Windows-API-Code-Pack-1.1/blob/main/LICENSE).

## Choosing an upgrade path

|Requirement|Action|
|--|--|
|Use the new default implementation|Upgrade the KAMISHIBAI packages. Existing `OpenFile` and `SaveFile` call sites remain unchanged.|
|Preserve the 3.x implementation|Install `Kamishibai.FileDialogs.WindowsApiCodePack` and call `AddWindowsApiCodePackFileDialogs()` before `Build()`.|
|Use an application-specific dialog|Implement and register `IFileDialogService` before `Build()`.|
|Use Code Pack APIs directly|Add an explicit Code Pack dependency; do not rely on the dependency from `Kamishibai.View`.|

See also the [KAMISHIBAI 4.0.0 release notes](releases/4.0.0.md).

[<< Animation of navigation](11-animation-of-navigation.md) | [Menu](01-table-of-contents.md)
