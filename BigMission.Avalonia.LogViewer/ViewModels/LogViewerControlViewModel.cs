namespace LogViewer.Core.ViewModels;

public class LogViewerControlViewModel : ObservableObject, ILogDataStoreImpl
{
    public LogViewerControlViewModel(ILogDataStore dataStore)
    {
        DataStore = dataStore;
    }

    public ILogDataStore DataStore { get; set; }
}