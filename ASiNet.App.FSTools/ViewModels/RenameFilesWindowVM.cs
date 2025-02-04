using System.Collections.ObjectModel;
using ASiNet.App.FSTools.ViewModels.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.FSTools.ViewModels;
public partial class RenameFilesWindowVM : ObservableObject
{
    public RenameFilesWindowVM(IEnumerable<FileSystemEntryVM> files)
    {
        files.ForEach(x => Items.Add(new(x)));
    }

    public ObservableCollection<RenameFilesItemVM> Items { get; } = [];


    [RelayCommand]
    private void Ok()
    {
        foreach (var item in Items)
        {
            ((FileSystemEntryVM)item.Item).Rename(string.IsNullOrWhiteSpace(item.NewName) ? null : item.NewName, string.IsNullOrWhiteSpace(item.NewExtension) ? null : item.NewExtension);
        }
    }

    [RelayCommand]
    private void Cancel()
    {

    }

}
