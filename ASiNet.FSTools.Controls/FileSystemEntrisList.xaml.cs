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
        _contextMenu = (FilesContextMenu)FindResource("MainContextMenu");
        _contextMenu.SelectAllFiles += SelectAllFiles;
        _contextMenu.UnselectAllFiles += UnselectAllFiles;
    }

    private FilesContextMenu _contextMenu;

    public readonly static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty OpenCommandProperty = DependencyProperty.Register(nameof(OpenCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectionChangedProperty = DependencyProperty.Register(nameof(SelectionChanged), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));

    #region CONTEXT_MENU
    public readonly static DependencyProperty OpenFilesCommandProperty = DependencyProperty.Register(nameof(OpenFilesCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty RenameFilesCommandProperty = DependencyProperty.Register(nameof(RenameFilesCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty MoveFilesCommandProperty = DependencyProperty.Register(nameof(MoveFilesCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty CopyFilesCommandProperty = DependencyProperty.Register(nameof(CopyFilesCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty DeleteFilesCommandProperty = DependencyProperty.Register(nameof(DeleteFilesCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));
    public readonly static DependencyProperty BackToParentCommandProperty = DependencyProperty.Register(nameof(BackToParentCommand), typeof(ICommand), typeof(FileSystemEntriesList), new PropertyMetadata(null));


    public ICommand? OpenFilesCommand
    {
        get { return GetValue(OpenFilesCommandProperty) as ICommand; }
        set { SetValue(OpenFilesCommandProperty, value); }
    }

    public ICommand? RenameFilesCommand
    {
        get { return GetValue(RenameFilesCommandProperty) as ICommand; }
        set { SetValue(RenameFilesCommandProperty, value); }
    }

    public ICommand? MoveFilesCommand
    {
        get { return GetValue(MoveFilesCommandProperty) as ICommand; }
        set { SetValue(MoveFilesCommandProperty, value); }
    }

    public ICommand? CopyFilesCommand
    {
        get { return GetValue(CopyFilesCommandProperty) as ICommand; }
        set { SetValue(CopyFilesCommandProperty, value); }
    }

    public ICommand? DeleteFilesCommand
    {
        get { return GetValue(DeleteFilesCommandProperty) as ICommand; }
        set { SetValue(DeleteFilesCommandProperty, value); }
    }

    public ICommand? BackToParentCommand
    {
        get { return GetValue(BackToParentCommandProperty) as ICommand; }
        set { SetValue(BackToParentCommandProperty, value); }
    }

    #endregion
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

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(ItemsSource): ItemsList.SetValue(ListView.ItemsSourceProperty, e.NewValue); break;
            case nameof(OpenFilesCommand): _contextMenu.SetValue(FilesContextMenu.OpenFileCommandProperty, e.NewValue); break;
            case nameof(RenameFilesCommand): ItemsList.SetValue(FilesContextMenu.RenameFileCommandProperty, e.NewValue); break;
            case nameof(MoveFilesCommand): ItemsList.SetValue(FilesContextMenu.MoveFileCommandProperty, e.NewValue); break;
            case nameof(CopyFilesCommand): ItemsList.SetValue(FilesContextMenu.CopyFileCommandProperty, e.NewValue); break;
            case nameof(DeleteFilesCommand): ItemsList.SetValue(FilesContextMenu.DeleteFileCommandProperty, e.NewValue); break;
            case nameof(BackToParentCommand): ItemsList.SetValue(FilesContextMenu.BackToParentCommandProperty, e.NewValue); break;
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
            _contextMenu.FileSpecificMenuOptionsIsVisible = true;
        else
            _contextMenu.FileSpecificMenuOptionsIsVisible = false;
       

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
