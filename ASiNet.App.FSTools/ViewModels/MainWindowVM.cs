using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using ASiNet.App.FSTools.View;
using ASiNet.App.FSTools.ViewModels.Entities;
using ASiNet.FSTools.Models;
using ASiNet.FSTools.Models.Entities;
using ASiNet.FSTools.Models.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.FSTools.ViewModels;

public partial class MainWindowVM : ObservableObject
{
    public MainWindowVM()
    {
        _fc = new();
       _fc.GetEntries().ForEach(x => Items.Add(new(x)));
    }

    [ObservableProperty]
    public partial bool IsSelectedItems { get; set; }

    [ObservableProperty]
    public partial bool IsFocusedSearchBox { get; set; }

    [ObservableProperty]
    public partial string CurrentPath { get; set; } = string.Empty;

    public ObservableCollection<FileSystemEntryVM> Items { get; } = [];

    public ObservableCollection<string> SearchResultItems {get; } = [];

    public List<FileSystemEntryVM> SelectedItems { get; } = [];

    private FilesContext _fc;

    private static char[] _invalidPathChars = Path.GetInvalidPathChars();

    [RelayCommand]
    private void OpenFile(FileSystemEntryVM item)
    {
        if(item.Entry.Type is EntryType.Folder or EntryType.Drive)
        {
            CurrentPath = item.Entry.Path;
            Items.ForEach(x => x.Dispose());
            Items.Clear();
            _fc.GetEntries(item.Entry.Path).ForEach(x => Items.Add(new(x)));
        }
    }

    [RelayCommand]
    private void OpenFiles()
    {

    }
    [RelayCommand]
    private void RenameFiles()
    {
        var renameWindow = new RenameFilesWindow() { DataContext = new RenameFilesWindowVM(SelectedItems) };
        renameWindow.ShowDialog();
    }
    [RelayCommand]
    private void CopyFiles()
    {

    }
    [RelayCommand]
    private void DeleteFiles()
    {

    }
    [RelayCommand]
    private void MoveFiles()
    {

    }

    [RelayCommand]
    private void SelectedSearchItem(string? path)
    {
        if(path is null)
            return;
        if(Directory.Exists(path))
        {
            CurrentPath = path;
            Items.ForEach(x => x.Dispose());
            Items.Clear();
            _fc.GetEntries(path).ForEach(x => Items.Add(new(x)));
        }
    }


    [RelayCommand]
    private void BackToParent()
    {
        Items.Clear();
        var dName = Path.GetDirectoryName(CurrentPath);
        _fc.GetEntries(dName).ForEach(x => Items.Add(new(x)));
        CurrentPath = dName ?? string.Empty;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(CurrentPath))
        {
            if (CurrentPath.IndexOfAny(_invalidPathChars) >= 0)
                CurrentPath = string.Concat(CurrentPath.Where(x => !_invalidPathChars.Contains(x)));
        }
        if(e.PropertyName == nameof(IsFocusedSearchBox) || (IsFocusedSearchBox && e.PropertyName == nameof(CurrentPath)))
        {
            if (IsFocusedSearchBox)
            {
                SearchResultItems.Clear();
                DirectorySearch.Search(CurrentPath).ForEach(x => SearchResultItems.Add(x));
            }
        }
        base.OnPropertyChanged(e);
    }
}
