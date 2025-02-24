using System.Collections.ObjectModel;
using System.Drawing;
using ASiNet.FSTools.VirtualWorkspace.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels;
public partial class VWAreaVM : ObservableObject, IVirtualWorkspaceAreaViewModel
{
    public ObservableCollection<IAreaWindowViewModel> Contents { get; } = [];

    [ObservableProperty]
    public partial double Scale { get; set; }

    [ObservableProperty]
    public partial Point Position { get; set; }
}