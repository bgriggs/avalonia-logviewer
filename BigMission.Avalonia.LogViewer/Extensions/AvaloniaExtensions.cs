using LogViewer.Core;
using LogViewer.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BigMission.Avalonia.LogViewer.Extensions;

public static class ServicesExtension
{
    public static HostApplicationBuilder AddLogViewer(this HostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ILogDataStore, LogDataStore>();
        builder.Services.AddSingleton<LogViewerControlViewModel>();

        return builder;
    }
}
