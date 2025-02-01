namespace ASiNet.FSTools.Models.Entities;
public class FileSystemEntry
{
    public string Name { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string Path { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public DateTime EditTime { get; set; }

    public long Size { get; set; }

    public string Type { get; set; } = null!;
}
