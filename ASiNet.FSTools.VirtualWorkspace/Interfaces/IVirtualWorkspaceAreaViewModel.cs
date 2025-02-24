using System.Collections.ObjectModel;
using System.Drawing;

namespace ASiNet.FSTools.VirtualWorkspace.Interfaces;
public interface IVirtualWorkspaceAreaViewModel
{

    public ObservableCollection<IAreaWindowViewModel> Contents { get; }

    public double Scale { get; set; }

    public Point Position { get; set; }
}