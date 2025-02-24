using ASiNet.FSTools.VirtualWorkspace.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels;
public partial class VWWindowVM : ObservableObject, IVirtualWorkspaceWindowViewModel
{
    [ObservableProperty]
    public partial IVirtualWorkspaceAreaViewModel AreaViewModel { get; set; }
}