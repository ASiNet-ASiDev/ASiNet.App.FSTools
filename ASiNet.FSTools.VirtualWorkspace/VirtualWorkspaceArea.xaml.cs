using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ASiNet.FSTools.VirtualWorkspace;

public partial class VirtualWorkspaceArea : Canvas
{
    public VirtualWorkspaceArea()
    {
        InitializeComponent();
        PanelRoot.SizeChanged += OnCanvasSizeChanged;
    }

    private Point? _oldMousePos;

    private void AreaRoot_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        var position = e.GetPosition(AreaRoot);
        ScaleArea(position, e.Delta >= 0 ? 1.1 : 0.9);
    }

    private void AreaRoot_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var pos = e.GetPosition(PanelRoot);
            if (_oldMousePos is null)
            {
                _oldMousePos = pos;
                return;
            }
            var newPos = _oldMousePos.Value - pos;
            MoveArea(newPos);
            _oldMousePos = pos;
        }
        else
        {
            _oldMousePos = null;
        }
    }


    private void MoveArea(Vector offset)
    {
        var matrix = matrixTransform.Matrix;
        offset.Negate();
        matrix.Translate(offset.X, offset.Y);
        matrixTransform.Matrix = matrix;
    }

    private void ScaleArea(Point pos, double scale)
    {
        var matrix = matrixTransform.Matrix;
        matrix.ScaleAtPrepend(scale, scale, pos.X, pos.Y);
        matrixTransform.Matrix = matrix;
    }



    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var l = (PanelRoot.ActualWidth - AreaRoot.ActualWidth) / 2;
        var h = (PanelRoot.ActualHeight - AreaRoot.ActualHeight) / 2;
        SetTop(AreaRoot, h);
        SetLeft(AreaRoot, l);
        PanelRoot.SizeChanged -= OnCanvasSizeChanged;
    }
}
