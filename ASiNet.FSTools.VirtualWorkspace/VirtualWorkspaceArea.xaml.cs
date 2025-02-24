using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASiNet.FSTools.VirtualWorkspace.Interfaces;
using ASiNet.FSTools.VirtualWorkspace.Interfaces.Controller;

namespace ASiNet.FSTools.VirtualWorkspace;

public partial class VirtualWorkspaceArea : Canvas, IMovementElement, IScaledElement
{
    public VirtualWorkspaceArea()
    {
        Controller = new();
        Scale = 1;
        InitializeComponent();
        PanelRoot.SizeChanged += OnCanvasSizeChanged;
        Controller.StartScaleElement(this);
    }

    public readonly static DependencyProperty ContentsProperty = DependencyProperty.Register(nameof(Contents), typeof(IEnumerable), typeof(VirtualWorkspaceArea), new PropertyMetadata(null));

    public readonly static DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(VirtualWorkspaceArea), new PropertyMetadata(null));

    public readonly static DependencyProperty PositionProperty = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(VirtualWorkspaceArea), new PropertyMetadata(null));

    public AreaController Controller { get; }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
    }


    public IEnumerable Contents
    {
        get { return (IEnumerable)GetValue(ContentsProperty); }
        set { SetValue(ContentsProperty, value); }
    }

    public double Scale
    {
        get { return (double)GetValue(ScaleProperty); }
        set { SetValue(ScaleProperty, value); }
    }

    public Point Position
    {
        get { return (Point)GetValue(PositionProperty); }
        set { SetValue(PositionProperty, value); }
    }

    public double MaxZoom { get; set; } = 2;
    public double MinZoom { get; set; } = 0.15;

    private List<AreaWindow> _windows = [];

    private void AreaRoot_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        Controller.InvokeScale(e.GetPosition(AreaRoot), e.Delta >= 0 ? 1.1 : 0.9);
    }

    private void AreaRoot_MouseMove(object sender, MouseEventArgs e)
    {
        if(!Controller.ContainsMovementElement())
            return;
        var pos = e.GetPosition(PanelRoot);
        Controller.InvokeMovement(pos, Scale);
    }

    private void AreaRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (!Controller.ContainsMovementElement())
            Controller.StartMovement(e.GetPosition(PanelRoot), this, false);
    }

    private void AreaRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (Controller.ContainsMovementElement())
            Controller.EndMovement();
    }

    private void AreaRoot_MouseLeave(object sender, MouseEventArgs e)
    {
        if (Controller.ContainsMovementElement())
            Controller.EndMovement();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if(e.Property.Name == nameof(Contents))
        {
            if (e.OldValue is ObservableCollection<IAreaWindowViewModel> oldObsList)
                oldObsList.CollectionChanged -= OnContentChanged;
            if (e.NewValue is ObservableCollection<IAreaWindowViewModel> newObsList)
                newObsList.CollectionChanged += OnContentChanged;

            if (e.NewValue is IEnumerable<IAreaWindowViewModel> collection)
                SetContent(collection);
        }
        base.OnPropertyChanged(e);
    }

    private void OnContentChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (var item in e.NewItems!)
                {
                    var newWin = new AreaWindow(this) { DataContext = item, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                    AreaRoot.Children.Add(newWin);
                    _windows.Add(newWin);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                foreach (var item in e.OldItems!)
                {
                    var win = _windows.FirstOrDefault(x => x.DataContext == item);
                    if(win is null)
                        continue;
                    AreaRoot.Children.Remove(win);
                    _windows.Remove(win);
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                break;
        }
    }

    private void SetContent(IEnumerable<IAreaWindowViewModel>? collection)
    {
        if (collection is null)
            return;
        AreaRoot.Children.Clear();
        _windows.Clear();
        foreach (var item in collection)
        {
            var newWin = new AreaWindow(this) { DataContext = item, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            AreaRoot.Children.Add(newWin);
            _windows.Add(newWin);
        }
    }

    public void MoveElement(Vector offset, double scale)
    {
        var matrix = matrixTransform.Matrix;
        offset.Negate();
        matrix.Translate(offset.X, offset.Y);
        matrixTransform.Matrix = matrix;
        Position = new(matrix.OffsetX, matrix.OffsetY);
    }

    public void ScaleElement(Point position, double delta)
    {
        var matrix = matrixTransform.Matrix;
        matrix.ScaleAtPrepend(delta, delta, position.X, position.Y);
        matrixTransform.Matrix = matrix;
        Scale = matrix.M11;
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var l = (PanelRoot.ActualWidth - AreaRoot.ActualWidth) / 2;
        var h = (PanelRoot.ActualHeight - AreaRoot.ActualHeight) / 2;
        Canvas.SetTop(AreaRoot, h);
        Canvas.SetLeft(AreaRoot, l);
        PanelRoot.SizeChanged -= OnCanvasSizeChanged;
    }
}
