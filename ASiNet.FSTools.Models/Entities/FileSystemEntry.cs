using ASiNet.FSTools.Models.Enums;

namespace ASiNet.FSTools.Models.Entities;
public class FileSystemEntry(string name, string extension, string path, EntryType type, DateTime createTime, DateTime editTime, long size, long totalFreeSpace = 0)
{
    public string Name { get; private set; } = name;

    public string Extension { get; private set; } = extension;

    public string Path { get; private set; } = path;

    public DateTime CreateTime { get; private set; } = createTime;

    public DateTime EditTime { get; private set; } = editTime;

    public long Size { get; private set; } = size;

    public long TotalFreeSpace { get; private set; } = totalFreeSpace;

    public EntryType Type { get; private set; } = type;


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
