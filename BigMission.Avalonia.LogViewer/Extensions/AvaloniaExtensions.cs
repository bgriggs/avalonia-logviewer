using LogViewer.Core;
using LogViewer.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BigMission.Avalonia.LogViewer.Extensions;

public static class ServicesExtension
{
    public static HostApplicationBuilder AddLogViewer(this HostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ILogDataStore>(sp =>
        {
            var config = sp.GetService<IOptionsMonitor<DataStoreLoggerConfiguration>>();
            if (config != null)
            {
                var currentConfig = config.CurrentValue;
                return new LogDataStore(currentConfig.MaxLogEntries, currentConfig.DispatcherPriority);
            }
            return new LogDataStore();
        });
        builder.Services.AddSingleton<LogViewerControlViewModel>();

        return builder;
    }
}
