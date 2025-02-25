using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ASiNet.FSTools.VirtualWorkspace.Interfaces;
using ASiNet.FSTools.VirtualWorkspace.Interfaces.Controller;

namespace ASiNet.FSTools.VirtualWorkspace;


public partial class AreaWindow : Border, IMovementElement, IResizedElement
{
    public AreaWindow(VirtualWorkspaceArea area)
    {
        _area = area;
        InitializeComponent();
        Width = 500;
        Height = 300;
    }

    private Point? _oldMousePos;

    private VirtualWorkspaceArea _area;

    public readonly static DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(UIElement), typeof(AreaWindow), new PropertyMetadata(null));

    public readonly static DependencyProperty WindowWidthProperty = DependencyProperty.Register(nameof(WidowWidth), typeof(double), typeof(AreaWindow), new PropertyMetadata(null));

    public readonly static DependencyProperty WindowHeightProperty = DependencyProperty.Register(nameof(WidowHeight), typeof(double), typeof(AreaWindow), new PropertyMetadata(null));

    public readonly static DependencyProperty WindowPositionProperty = DependencyProperty.Register(nameof(WidowPosition), typeof(Point), typeof(AreaWindow), new PropertyMetadata(null));

    public readonly static DependencyProperty IsMinimizeProperty = DependencyProperty.Register(nameof(IsMinimize), typeof(bool), typeof(AreaWindow), new PropertyMetadata(null));

    public readonly static DependencyProperty IsPinnedProperty = DependencyProperty.Register(nameof(IsPinned), typeof(bool), typeof(AreaWindow), new PropertyMetadata(null));

    public UIElement? Content
    {
        get { return (UIElement?)GetValue(ContentProperty); }
        set { SetValue(ContentProperty, value); }
    }

    public double WidowWidth
    {
        get { return (double)GetValue(WindowWidthProperty); }
        set { SetValue(WindowWidthProperty, value); }
    }

    public double WidowHeight
    {
        get { return (double)GetValue(WindowHeightProperty); }
        set { SetValue(WindowHeightProperty, value); }
    }

    public Point WidowPosition
    {
        get { return (Point)GetValue(WindowPositionProperty); }
        set { SetValue(WindowPositionProperty, value); }
    }

    public bool IsMinimize
    {
        get { return (bool)GetValue(IsMinimizeProperty); }
        set { SetValue(IsMinimizeProperty, value); }
    }

    public bool IsPinned
    {
        get { return (bool)GetValue(IsPinnedProperty); }
        set { SetValue(IsPinnedProperty, value); }
    }


    private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(!_area.Controller.ContainsMovementElement())
            _area.Controller.StartMovement(e.GetPosition(_area), this);
    }

    private void Header_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!_area.Controller.ContainsCurrentMovementElement(this))
            _area.Controller.EndMovement();
    }

    private void ResizeHandler_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        if (!_area.Controller.ContainsResizeElement())
            _area.Controller.StartResizeElement(e.GetPosition(_area.AreaRoot), this);
    }

    private void ResizeHandler_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        if (_area.Controller.ContainsResizeElement())
            _area.Controller.EndResizeElement();
    }

    public void MoveElement(Vector offset, double scale)
    {
        var matrix = matrixTransform.Matrix;
        offset.Negate();
        matrix.Translate(offset.X, offset.Y);
        matrixTransform.Matrix = matrix;
    }

    public void ResizeElement(Vector offset, double scale)
    {
        var newOffset = offset;
        var oldPos = _area.AreaRoot.TransformToVisual(this).Transform(new Point(0, 0));
        Width -= newOffset.X;
        Height -= newOffset.Y;
        _area.AreaRoot.UpdateLayout();
        var newPos = _area.AreaRoot.TransformToVisual(this).Transform(new Point(0, 0));
        var pos = oldPos - newPos;
        MoveElement(pos, 1);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property.Name == nameof(Content))
        {
            if (e.OldValue is UIElement ouie)
                Root.Children.Remove(ouie);
            if (e.NewValue is UIElement nuie)
            {
                Root.Children.Add(nuie);
                Panel.SetZIndex(nuie, 1);
                Grid.SetRow(nuie, 2);
            }
        }
        if (e.Property.Name == nameof(DataContext))
        {
            if(e.NewValue is null)
                return;
            CreateBinding(this, DataContext, ContentProperty, nameof(IAreaWindowViewModel.Content), BindingMode.OneWay);
            CreateBinding(this, DataContext, WindowWidthProperty, nameof(IAreaWindowViewModel.Width), BindingMode.OneWayToSource);
            CreateBinding(this, DataContext, WindowHeightProperty, nameof(IAreaWindowViewModel.Height), BindingMode.OneWayToSource);
            CreateBinding(this, DataContext, WindowPositionProperty, nameof(IAreaWindowViewModel.Position), BindingMode.OneWayToSource);
            CreateBinding(this, DataContext, IsMinimizeProperty, nameof(IAreaWindowViewModel.IsMinimize), BindingMode.OneWayToSource);
            CreateBinding(this, DataContext, IsPinnedProperty, nameof(IAreaWindowViewModel.IsPinned), BindingMode.OneWayToSource);
        }
        base.OnPropertyChanged(e);
    }


    private static void CreateBinding(DependencyObject target, object src, DependencyProperty property, string path, BindingMode mode = BindingMode.TwoWay)
    {
        var bind = new Binding(path)
        {
            Source = src,
            Mode = mode,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        BindingOperations.SetBinding(target, property, bind);
    }
}
