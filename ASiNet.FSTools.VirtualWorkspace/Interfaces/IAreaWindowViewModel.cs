using System.Windows;

namespace ASiNet.FSTools.VirtualWorkspace.Interfaces;
public interface IAreaWindowViewModel
{
    public UIElement? Content { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public Point Position { get; set; }

    public bool IsMinimize { get; set; }

    public bool IsPinned { get; set; }
}
