# Avalonia LogViewer
Log Viewer is a consolidation of: https://www.codeproject.com/Articles/5357417/LogViewer-Control-for-WinForms-WPF-and-Avalonia-in#custom-microsoft-logger-implementation

![alt text](https://github.com/bgriggs/avalonia-logviewer/blob/main/Screenshot%202024-05-11%20124043.png?raw=true)

A Log Viewer control for Avalonia applications that supports multiple logging frameworks including Microsoft.Extensions.Logging.

Nuget: https://www.nuget.org/packages/BigMission.Avalonia.LogViewer/

## Dependency Injection Setup Example
``` csharp
var builder = Host.CreateApplicationBuilder();
builder.AddLogViewer().Logging.AddDefaultDataStoreLogger(static c => c.MaxLogEntries = 250);
```

View Model Setup Example:
``` csharp
public LogViewerControlViewModel LogViewer { get; }

// Constructor
public MainWindowViewModel(LogViewerControlViewModel logViewer)
{
    LogViewer = logViewer;
}
```

## XAML Usage
Reference the namespace:
``` xml
xmlns:lc="clr-namespace:BigMission.Avalonia.LogViewer;assembly=BigMission.Avalonia.LogViewer"
```
Add the control with data binding:
``` xml
<lc:LogViewerControl DataContext="{Binding LogViewer}" Grid.Row="2" Margin="-12,0,-12,0"/>
```
