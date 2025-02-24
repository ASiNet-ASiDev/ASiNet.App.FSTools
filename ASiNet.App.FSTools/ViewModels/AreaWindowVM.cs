using System.Windows;
using ASiNet.FSTools.VirtualWorkspace.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.FSTools.ViewModels;
public partial class AreaWindowVM : ObservableObject, IAreaWindowViewModel
{
    [ObservableProperty]
    public partial UIElement? Content { get; set; }
    [ObservableProperty]
    public partial double Width { get; set; }
    [ObservableProperty]
    public partial double Height { get; set; }
    [ObservableProperty]
    public partial Point Position { get; set; }
    [ObservableProperty]
    public partial bool IsMinimize { get; set; }
    [ObservableProperty]
    public partial bool IsPinned { get; set; }
}