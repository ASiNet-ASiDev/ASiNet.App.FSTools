using ASiNet.FSTools.Models.Enums;

namespace ASiNet.FSTools.Models.Entities;
public class FileSystemEntry(string name, string extension, string path, EntryType type)
{
    public string Name { get; private set; } = name;

    public string Extension { get; private set; } = extension;

    public string Path { get; private set; } = path;

    public DateTime CreateTime { get; private set; }

    public DateTime EditTime { get; private set; }

    public long Size { get; private set; }

    public EntryType Type { get; private set; } = type;

    public static FileSystemEntry FromPath(string path) =>
        new(System.IO.Path.GetFileNameWithoutExtension(path),
            System.IO.Path.GetExtension(path) ?? string.Empty,
            path, EntryType.Folder);

    public bool Rename(string? newName, string? extension = null)
    {
        if(newName is null && extension is null)
            return true;
        if(newName == Name && (extension is null || extension == Extension))
            return true;
        if (File.Exists(Path))
        {
            try
            {
                var newFileName = $"{newName ?? Name}.{extension ?? Extension}";
                var newFilePath = $"{System.IO.Path.GetDirectoryName(Path)}{System.IO.Path.DirectorySeparatorChar}{newFileName}";
                File.Move(Path, newFilePath);
                Name = newName ?? Name;
                Extension = extension ?? Extension;
                Path = newFilePath;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            var newFileName = $"{newName ?? Name}.{extension ?? Extension}";
            var newFilePath = $"{System.IO.Path.GetDirectoryName(Path) ?? string.Empty}{System.IO.Path.DirectorySeparatorChar}{newFileName}";
            Name = newName ?? Name;
            Extension = extension ?? Extension;
            Path = newFilePath;
            return true;
        }
    }
}
