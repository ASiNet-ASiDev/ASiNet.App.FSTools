using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ASiNet.FSTools.Controls;

public partial class FileSystemEntriesList : Grid
{
    public FileSystemEntriesList()
    {
        
        InitializeComponent();
    }

    public readonly static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty OpenCommandProperty = DependencyProperty.Register(nameof(OpenCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectionChangedProperty = DependencyProperty.Register(nameof(SelectionChanged), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty IsSelectedItemsProperty = DependencyProperty.Register(nameof(IsSelectedItems), typeof(bool), typeof(FileSystemEntriesList), new PropertyMetadata(null));

    public IEnumerable? ItemsSource
    {
        get { return GetValue(ItemsSourceProperty) as IEnumerable; }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public ICommand? OpenCommand
    {
        get { return GetValue(OpenCommandProperty) as ICommand; }
        set { SetValue(OpenCommandProperty, value); }
    }

    public ICommand? SelectionChanged
    {
        get { return GetValue(SelectionChangedProperty) as ICommand; }
        set { SetValue(SelectionChangedProperty, value); }
    }

    public IList? SelectedItems
    {
        get { return GetValue(SelectedItemsProperty) as IList; }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public bool IsSelectedItems
    {
        get { return (bool)GetValue(IsSelectedItemsProperty); }
        set { SetValue(IsSelectedItemsProperty, value); }
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(ContextMenu): 
                if(e.OldValue is FilesContextMenu fcm)
                {
                    fcm.SelectAllFiles -= SelectAllFiles;
                    fcm.UnselectAllFiles -= UnselectAllFiles;
                }
                if (e.NewValue is FilesContextMenu fcm1)
                {
                    fcm1.SelectAllFiles += SelectAllFiles;
                    fcm1.UnselectAllFiles += UnselectAllFiles;
                }

                break;
        }
        base.OnPropertyChanged(e);
    }

    private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            var parent = sender as ListViewItem;
            var dc = parent?.DataContext;
            if (OpenCommand?.CanExecute(dc) ?? false)
                OpenCommand?.Execute(dc);
        }
        catch
        {

        }
    }

    private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var lv = (ListView)sender;
        if(lv.SelectedItems.Count > 0)
            IsSelectedItems = true;
        else
            IsSelectedItems = false;
       

        if (SelectedItems is null)
            return;
        try
        {
            foreach (var removed in e.RemovedItems)
            {
                SelectedItems.Remove(removed);
            }

            foreach (var added in e.AddedItems)
            {
                SelectedItems.Add(added);
            }
            var changed = e.AddedItems.Count - e.RemovedItems.Count;
            if (SelectionChanged?.CanExecute(changed) ?? false)
                SelectionChanged?.Execute(changed);
        }
        catch
        {

        }
    }

    private void SelectAllFiles()
    {
        ItemsList.SelectAll();
    }

    private void UnselectAllFiles()
    {
        ItemsList.UnselectAll();
    }
}
