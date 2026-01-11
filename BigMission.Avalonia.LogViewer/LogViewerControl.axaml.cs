using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using LogViewer.Core;
using System.Collections.Specialized;

namespace BigMission.Avalonia.LogViewer;

// Log Viewer is a consolidation of: https://www.codeproject.com/Articles/5357417/LogViewer-Control-for-WinForms-WPF-and-Avalonia-in#custom-microsoft-logger-implementation
public partial class LogViewerControl : UserControl
{
    private ILogDataStoreImpl? vm;
    private LogModel? item;


    public LogViewerControl()
    {
        InitializeComponent();
    }
  

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

    private async void OnDataGridDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (e.Source is not Control control)
            return;

        // Find the DataGridRow that was double-clicked
        var row = control.FindLogicalAncestorOfType<DataGridRow>();
        if (row?.DataContext is not LogModel logModel)
            return;

        // Build comma-separated string matching the visible columns
        var csvData = $"{logModel.Timestamp},{logModel.LogLevel},{logModel.State},{logModel.Exception}";

        // Copy to clipboard
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard != null)
        {
            await clipboard.SetTextAsync(csvData);

            // Show success notification
            await ShowClipboardNotificationAsync();
        }
    }

    private async Task ShowClipboardNotificationAsync()
    {
        var notification = this.FindControl<Border>("ClipboardNotification");
        if (notification != null)
        {
            notification.IsVisible = true;
            await Task.Delay(1000);
            notification.IsVisible = false;
        }
    }
}
