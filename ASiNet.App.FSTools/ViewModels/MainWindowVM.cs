using System.Collections.ObjectModel;
using ASiNet.App.FSTools.View;
using ASiNet.App.FSTools.ViewModels.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.FSTools.ViewModels;

public partial class MainWindowVM : ObservableObject
{
    [ObservableProperty]
    public partial bool IsSelectedItems { get; set; }

    public ObservableCollection<FileSystemEntryVM> Items { get; } =
        [new(new("test 1", ".txt", "file")), new(new("test 2", ".txt", "file")), new(new("test 3", ".txt", "file"))];

    public List<FileSystemEntryVM> SelectedItems { get; } = [];

    [RelayCommand]
    private void OpenFile(FileSystemEntryVM item)
    {

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
    private void BackToParent()
    {

    }

}
