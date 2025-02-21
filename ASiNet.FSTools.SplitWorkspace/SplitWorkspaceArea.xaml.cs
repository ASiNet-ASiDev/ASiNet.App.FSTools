using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using ASiNet.FSTools.SplitWorkspace.Enums;

namespace ASiNet.FSTools.SplitWorkspace;
public partial class SplitWorkspaceArea : Grid
{
    public SplitWorkspaceArea()
    {
        InitializeComponent();
        SetSplitMod(SplitMode);
    }

    public readonly static DependencyProperty SplitModeProperty = DependencyProperty.Register(nameof(SplitMode), typeof(SplitMode), typeof(SplitWorkspaceArea), new PropertyMetadata(null));

    public readonly static DependencyProperty ContentsProperty = DependencyProperty.Register(nameof(Contents), typeof(IEnumerable), typeof(SplitWorkspaceArea), new PropertyMetadata(null));

    public SplitMode SplitMode
    {
        get { return (SplitMode)GetValue(SplitModeProperty); }
        set { SetValue(SplitModeProperty, value); }
    }

    public IEnumerable Contents
    {
        get { return (IEnumerable)GetValue(ContentsProperty); }
        set { SetValue(ContentsProperty, value); }
    }

    private List<SplitContainer> _containers = [];

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(SplitMode):
                SetSplitMod((SplitMode)e.NewValue);
                break;
            case nameof(Contents):
                if (e.OldValue is ObservableCollection<(FrameworkElement, object?)> oldObsList)
                    oldObsList.CollectionChanged -= OnContentChanged;
                if (e.NewValue is ObservableCollection<(FrameworkElement, object?)> newObsList)
                    newObsList.CollectionChanged += OnContentChanged;

                if (e.NewValue is IEnumerable<(FrameworkElement, object?)> collection)
                    SetContent(collection);
                break;
        }
        base.OnPropertyChanged(e);
    }

    private void OnContentChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var newCollection = e.NewItems as IList<(FrameworkElement, object?)>;
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add or NotifyCollectionChangedAction.Replace:
                if (e.NewStartingIndex >= _containers.Count)
                    return;
                if (newCollection is null)
                    return;
                var changedCount = e.NewItems?.Count;
                var changedLastIndex = e.NewStartingIndex + changedCount;
                var lastChangeIndex = _containers.Count > changedLastIndex ? _containers.Count : changedLastIndex;

                for (int i = e.NewStartingIndex; i < lastChangeIndex; i++)
                {
                    _containers[i].SetContent(newCollection[i].Item1);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldStartingIndex >= _containers.Count)
                    return;
                var r_changedCount = e.OldItems?.Count;
                var r_changedLastIndex = e.OldStartingIndex + r_changedCount;
                var r_lastChangeIndex = _containers.Count > r_changedLastIndex ? _containers.Count : r_changedLastIndex;
                for (int i = e.NewStartingIndex; i < r_lastChangeIndex; i++)
                {
                    _containers[i].SetContent(null);
                }
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                break;
        }
    }

    private void SetContent(IEnumerable<(FrameworkElement, object?)>? collection)
    {
        if (collection is null)
            return;
        var i = 0;
        foreach (var view in collection)
        {
            if (_containers.Count > i)
                _containers[i].SetContent(view.Item1);
            i++;
        }
    }

    private void SetSplitMod(SplitMode splitMode)
    {
        switch (splitMode)
        {
            case SplitMode.NoSplit: NoSplit(); break;
            case SplitMode.VerticalTwoAreas: VDITA(); break;
            case SplitMode.HorizontalTwoAreas: HDITA(); break;
            case SplitMode.HorizontalTwoAndVerticalButtonOneAreas: HTAVBOA(); break;
            case SplitMode.HorizontalTwoAndVerticalTopOneAreas: HTAVTOA(); break;
        }
        SetContent(Contents as IEnumerable<(FrameworkElement, object?)>);
    }

    private void NoSplit()
    {
        ClearArea();
        _ = CreatePagesContainers(1).ToArray();
    }

    private void VDITA()
    {
        ClearArea();

        CreateColumns(2);
        var pc = CreatePagesContainers(2).ToArray();
        SetColumn(pc[1], 2);
    }

    private void HDITA()
    {
        ClearArea();

        CreateRows(2);
        var pc = CreatePagesContainers(2).ToArray();
        SetRow(pc[1], 2);
    }

    private void HTAVBOA()
    {
        ClearArea();

        CreateColumns(2, 2);
        CreateRows(2, 2);
        var pc = CreatePagesContainers(3).ToArray();
        SetColumn(pc[1], 2);

        SetRow(pc[2], 2);
        SetColumnSpan(pc[2], 2);

    }

    private void HTAVTOA()
    {
        ClearArea();

        CreateColumns(2, 2);
        CreateRows(2, 2);
        var pc = CreatePagesContainers(3).ToArray();

        SetColumnSpan(pc[0], 2);


        SetColumn(pc[2], 2);

        SetRow(pc[1], 2);
        SetRow(pc[2], 2);
    }

    private IEnumerable<SplitContainer> CreatePagesContainers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var pc = new SplitContainer(i);
            _containers.Add(pc);
            Children.Add(pc);
            SetZIndex(pc, 2);
            yield return pc;
        }
    }

    private void CreateColumns(int areasCount, int rows = 1)
    {
        for (int i = 0; i < areasCount; i++)
        {
            ColumnDefinitions.Add(new());
            if (i < areasCount - 1)
            {
                var splitter = new VerticalSplitter();
                Children.Add(splitter);
                SetZIndex(splitter, 1);
                SetColumn(splitter, i);
                SetRowSpan(splitter, rows);
            }
        }
    }

    private void CreateRows(int areasCount, int columns = 1)
    {
        for (int i = 0; i < areasCount; i++)
        {
            RowDefinitions.Add(new());
            if (i < areasCount - 1)
            {
                var splitter = new HorizontalSplitter();
                Children.Add(splitter);
                SetZIndex(splitter, 1);
                SetRow(splitter, i);
                SetColumnSpan(splitter, columns);
            }
        }
    }

    private void ClearArea()
    {
        Children.Clear();
        _containers.ForEach(x => x.SetContent(null));
        _containers.Clear();
        ColumnDefinitions.Clear();
        RowDefinitions.Clear();
    }
}
