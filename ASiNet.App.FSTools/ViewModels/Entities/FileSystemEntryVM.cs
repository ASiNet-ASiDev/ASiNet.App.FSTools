using ASiNet.FSTools.Controls.Interfaces;
using ASiNet.FSTools.Models.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels.Entities;
public partial class FileSystemEntryVM(FileSystemEntry entry) : ObservableObject, IFileSystemEntriesListItem
{
    [ObservableProperty]
    public partial string Name { get; set; } = entry.Name;
    [ObservableProperty]
    public partial string Extension { get; set; } = entry.Extension;
    [ObservableProperty]
    public partial DateTime CreateTime { get; set; } = entry.CreateTime;
    [ObservableProperty]
    public partial DateTime EditTime { get; set; } = entry.EditTime;
    [ObservableProperty]
    public partial long Size { get; set; } = entry.Size;
    [ObservableProperty]
    public partial string Type { get; set; } = entry.Type;

    public FileSystemEntry Entry { get; } = entry;

    public void Rename(string? newName, string? newExtension)
    {
        if(Entry.Rename(newName, newExtension))
        {
            Name = Entry.Name;
            Extension = Entry.Extension;
        }
        
    }
}
