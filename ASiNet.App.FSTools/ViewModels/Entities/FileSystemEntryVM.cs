using System.Windows.Media;
using ASiNet.FSTools.Controls.Interfaces;
using ASiNet.FSTools.Models.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels.Entities;
public partial class FileSystemEntryVM : ObservableObject, IFileSystemEntriesListItem, IDisposable
{
    public FileSystemEntryVM(FileSystemEntry entry)
    {
        Entry = entry;
        Name = entry.Name;
        Extension = entry.Extension;
        CreateTime = entry.CreateTime;
        EditTime = entry.EditTime;
        Size = entry.Size;
        Type = entry.Type.ToString();
        LoadIcon();
    }

    [ObservableProperty]
    public partial string Name { get; set; }
    [ObservableProperty]
    public partial string Extension { get; set; }
    [ObservableProperty]
    public partial DateTime CreateTime { get; set; }
    [ObservableProperty]
    public partial DateTime EditTime { get; set; }
    [ObservableProperty]
    public partial long Size { get; set; }
    [ObservableProperty]
    public partial string Type { get; set; }

    [ObservableProperty]
    public partial ImageSource? Icon { get; set; }

    public FileSystemEntry Entry { get; }

    private CancellationTokenSource _cts = new();

    private async void LoadIcon()
    {
        var icon = await _other.WinApi.FilesIcons.GetIconAsync(Entry.Path, _cts.Token);
        await App.Current.Dispatcher.InvokeAsync(() => Icon = icon);
    }

    public void Dispose()
    {
        _cts.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Rename(string? newName, string? newExtension)
    {
        if(Entry.Rename(newName, newExtension))
        {
            Name = Entry.Name;
            Extension = Entry.Extension;
        }
        
    }
}
