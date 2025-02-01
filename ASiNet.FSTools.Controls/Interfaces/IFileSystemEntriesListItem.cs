namespace ASiNet.FSTools.Controls.Interfaces;
public interface IFileSystemEntriesListItem
{
    public string Name { get; }

    public string Extension { get; }

    public DateTime CreateTime { get; }

    public DateTime EditTime { get; }

    public long Size { get; }

    public string Type { get; }
}
