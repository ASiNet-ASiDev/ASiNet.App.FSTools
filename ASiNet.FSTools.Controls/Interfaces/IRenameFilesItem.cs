namespace ASiNet.FSTools.Controls.Interfaces;
public interface IRenameFilesItem
{
    public string OldName { get; }

    public string OldExtension { get; }

    public string? NewName { get; set; }

    public string? NewExtension { get; set; }

    public bool ExtensionIsAvailable { get; }
}
