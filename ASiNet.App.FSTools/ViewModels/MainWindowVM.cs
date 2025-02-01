using System.Collections.ObjectModel;
using ASiNet.App.FSTools.ViewModels.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.FSTools.ViewModels;

public partial class MainWindowVM : ObservableObject
{
    [ObservableProperty]
    public partial bool IsSelectedItems { get; set; }

    public ObservableCollection<FileSystemEntryVM> Items { get; } =
        [new(new() { Name = "Test1", Type = "File" }), new(new() { Name = "Test2", Type = "File" }), new(new() { Name = "Test3", Type = "File" })];

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
