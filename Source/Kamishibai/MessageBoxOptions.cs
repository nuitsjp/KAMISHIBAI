namespace Kamishibai;

[Flags]
public enum MessageBoxOptions
{
    None = 0,
    DefaultDesktopOnly = 0x20000,
    RightAlign = 0x80000,
    RtlReading = 0x100000,
    ServiceNotification = 0x200000
}