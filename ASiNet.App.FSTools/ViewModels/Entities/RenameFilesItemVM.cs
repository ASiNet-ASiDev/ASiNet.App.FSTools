using ASiNet.FSTools.Controls.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels.Entities;
public partial class RenameFilesItemVM(FileSystemEntryVM item) : ObservableObject, IRenameFilesItem
{
    public IFileSystemEntriesListItem Item { get; } = item;

    public string OldName { get; } = item.Entry.Name;

    public string OldExtension { get; } = item.Entry.Extension;

    public bool ExtensionIsAvailable { get; } = true;

    [ObservableProperty]
    public partial string? NewName { get; set; }
    [ObservableProperty]
    public partial string? NewExtension { get; set; }
}
