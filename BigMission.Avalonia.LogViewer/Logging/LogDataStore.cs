using Avalonia.Threading;
using System.Collections.ObjectModel;

namespace LogViewer.Core;

public class LogDataStore : ILogDataStore
{
    // Force to static to prevent multiple instances of the log data store in the application. Multiple are being
    // created in the service initialization which effectively lose logs.
    private static readonly ObservableCollection<LogModel> entries = [];
    private readonly int? _maxLogEntries;
    private readonly DispatcherPriority _dispatcherPriority;

    public ObservableCollection<LogModel> Entries => entries;

    public LogDataStore() : this(null, DispatcherPriority.Background)
    {
    }

    public LogDataStore(int? maxLogEntries, DispatcherPriority dispatcherPriority)
    {
        _maxLogEntries = maxLogEntries;
        _dispatcherPriority = dispatcherPriority;
    }

    public async void AddEntry(LogModel logModel)
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            AddEntryInternal(logModel);
            return;
        }

        try
        {
            await Dispatcher.UIThread.InvokeAsync(() => AddEntryInternal(logModel), _dispatcherPriority);
        }
        catch (TaskCanceledException)
        {
            // Expected during application shutdown when dispatcher is being torn down - ignore
        }
    }

    private void AddEntryInternal(LogModel logModel)
    {
        Entries.Add(logModel);

        if (_maxLogEntries.HasValue && Entries.Count > _maxLogEntries.Value)
        {
            Entries.RemoveAt(0);
        }
    }
}