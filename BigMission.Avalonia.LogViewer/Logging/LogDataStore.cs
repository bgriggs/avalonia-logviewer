using Avalonia.Threading;
using System.Collections.ObjectModel;

namespace LogViewer.Core;

public class LogDataStore : ILogDataStore
{
    // Force to static to prevent multiple instances of the log data store in the application. Multiple are being
    // created in the service initialization which effectively lose logs.
    private static readonly ObservableCollection<LogModel> entries = [];
    public ObservableCollection<LogModel> Entries => entries;

    public async void AddEntry(LogModel logModel)
    {
        await Dispatcher.UIThread.InvokeAsync(() => Entries.Add(logModel));
    }
}