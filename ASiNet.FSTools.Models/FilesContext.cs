using System.Diagnostics;
using ASiNet.FSTools.Models.Entities;

namespace ASiNet.FSTools.Models;
public class FilesContext : IDisposable
{

    public FilesContext()
    {
        _watcher = new(() =>
        {
            var watcher = new FileSystemWatcher()
            {
                NotifyFilter = NotifyFilters.DirectoryName |
                       NotifyFilters.FileName |
                       NotifyFilters.Size |
                       NotifyFilters.LastWrite |
                       NotifyFilters.CreationTime,
                IncludeSubdirectories = false,
            };
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
            watcher.Changed += OnFileChanged;
            return watcher;
        });

    }

    private Lazy<FileSystemWatcher> _watcher;

    private FileAttributes _ignore = FileAttributes.Hidden;

    public IEnumerable<FileSystemEntry> GetEntries(string? dirPath = null)
    {
        _watcher.Value.EnableRaisingEvents = false;
        if (dirPath is null)
        {
            foreach (var drive in DriveInfo.GetDrives())
                yield return new(drive.Name, string.Empty, drive.RootDirectory.FullName, Enums.EntryType.Drive);
            yield break;
        }
        if (Directory.Exists(dirPath))
        {
            foreach (var item in Directory.EnumerateDirectories(dirPath).Where(x => !File.GetAttributes(x).HasFlag(_ignore)))
                yield return new(Path.GetFileName(item), string.Empty, item, Enums.EntryType.Folder);
            foreach (var item in Directory.EnumerateFiles(dirPath).Where(x => !File.GetAttributes(x).HasFlag(_ignore)))
                yield return new(Path.GetFileNameWithoutExtension(item), Path.GetExtension(item), item, Enums.EntryType.File);
            _watcher.Value.Path = dirPath;
            _watcher.Value.EnableRaisingEvents = true;
        }
        yield break;
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {

    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {

    }

    private void OnFileDeleted(object sender, FileSystemEventArgs e)
    {

    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {

    }

    public void Dispose()
    {
        if (_watcher.IsValueCreated)
            _watcher.Value.Dispose();
        GC.SuppressFinalize(this);
    }
}
