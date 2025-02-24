using System.Collections.ObjectModel;
using System.Windows;
using ASiNet.FSTools.Controls;
using ASiNet.FSTools.SplitWorkspace.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.FSTools.ViewModels;

public partial class MainWindowVM : ObservableObject
{
    public MainWindowVM()
    {
        (FrameworkElement, object?) p1 = (new FileExplorerPage(), new FileExplorerPageVM());
        (FrameworkElement, object?) p2 = (new FileExplorerPage(), new FileExplorerPageVM());
        p1.Item1.DataContext = p1.Item2;
        p2.Item1.DataContext = p2.Item2;
        PagesContent.Add(p1);
        PagesContent.Add(p2);

        var vm = new VWWindowVM()
        {
            AreaViewModel = new VWAreaVM()
        };
        vm.AreaViewModel.Contents.Add(new AreaWindowVM() { Content = new FileExplorerPage() { DataContext = new FileExplorerPageVM() } });
        vm.AreaViewModel.Contents.Add(new AreaWindowVM() { Content = new FileExplorerPage() { DataContext = new FileExplorerPageVM() } });
        vm.AreaViewModel.Contents.Add(new AreaWindowVM() { Content = new FileExplorerPage() { DataContext = new FileExplorerPageVM() } });
        vm.AreaViewModel.Contents.Add(new AreaWindowVM() { Content = new FileExplorerPage() { DataContext = new FileExplorerPageVM() } });
        var vww = new ASiNet.FSTools.VirtualWorkspace.Windows.VirtualWorkspaceWindow()
        { 
            DataContext = vm
        };
        vww.Show();
    }

    [ObservableProperty]
    public partial SplitMode SplitMode { get; set; }

    public ObservableCollection<(FrameworkElement, object?)> PagesContent { get; } = [];


    [RelayCommand]
    private void SetSplitMode(SplitMode splitMode)
    {
        SplitMode = splitMode;
    }

}
