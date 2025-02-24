using System.Windows;

namespace ASiNet.FSTools.VirtualWorkspace.Windows;

public partial class VirtualWorkspaceWindow : Window
{
    public VirtualWorkspaceWindow()
    {
        InitializeComponent();
        Left = 0;
        Top = 0;
        Width = SystemParameters.WorkArea.Width;
        Height = SystemParameters.WorkArea.Height;
    }


    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
    }
}
