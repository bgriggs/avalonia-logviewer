using Avalonia.Controls;
using Avalonia.LogicalTree;
using LogViewer.Core;
using System.Collections.Specialized;

namespace BigMission.Avalonia.LogViewer;

// Log Viewer is a consolidation of: https://www.codeproject.com/Articles/5357417/LogViewer-Control-for-WinForms-WPF-and-Avalonia-in#custom-microsoft-logger-implementation
public partial class LogViewerControl : UserControl
{
    public LogViewerControl()
    {
        InitializeComponent();
    }

    private ILogDataStoreImpl? vm;
    private LogModel? item;

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is null)
            return;

        vm = (ILogDataStoreImpl)DataContext;
        vm.DataStore.Entries.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        => item = MyDataGrid.ItemsSource.Cast<LogModel>().LastOrDefault();

    private void OnLayoutUpdated(object? sender, EventArgs e)
    {
        if (CanAutoScroll.IsChecked != true || item is null)
            return;

        MyDataGrid.ScrollIntoView(item, null);
        item = null;
    }

    private void OnDetachedFromLogicalTree(object? sender, LogicalTreeAttachmentEventArgs e)
    {
        if (vm is null) return;
        vm.DataStore.Entries.CollectionChanged -= OnCollectionChanged;
    }
}
