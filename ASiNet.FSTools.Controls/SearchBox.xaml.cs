using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ASiNet.FSTools.Controls;
/// <summary>
/// Логика взаимодействия для SearchBox.xaml
/// </summary>
public partial class SearchBox : Grid
{
    public SearchBox()
    {
        
        InitializeComponent();
        SearchTextBox.GotFocus += OnGotFocus;
        SearchTextBox.LostFocus += OnLostFocus;
    }

    public readonly static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(SearchBox), new PropertyMetadata(null));
    public readonly static DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(SearchBox), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectedItemCommandProperty = DependencyProperty.Register(nameof(SelectedItemCommand), typeof(ICommand), typeof(SearchBox), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(string), typeof(SearchBox), new PropertyMetadata(null));
    public readonly static DependencyProperty FocusedProperty = DependencyProperty.Register(nameof(Focused), typeof(bool), typeof(SearchBox), new PropertyMetadata(null));

    public readonly static DependencyProperty ToParentCommandProperty = DependencyProperty.Register(nameof(ToParentCommand), typeof(ICommand), typeof(SearchBox), new PropertyMetadata(null));

    public IEnumerable? ItemsSource
    {
        get { return GetValue(ItemsSourceProperty) as IEnumerable; }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public string? Text
    {
        get { return GetValue(TextProperty) as string; }
        set { SetValue(TextProperty, value); }
    }

    public bool Focused
    {
        get { return (bool)GetValue(FocusedProperty); }
        set { SetValue(FocusedProperty, value); }
    }

    public ICommand? SelectedItemCommand
    {
        get { return GetValue(SelectedItemCommandProperty) as ICommand; }
        set { SetValue(SelectedItemCommandProperty, value); }
    }

    public ICommand? ToParentCommand
    {
        get { return GetValue(ToParentCommandProperty) as ICommand; }
        set { SetValue(ToParentCommandProperty, value); }
    }

    public string? SelectedItem
    {
        get { return GetValue(SelectedItemProperty) as string; }
        set { SetValue(SelectedItemProperty, value); }
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItemCommand?.CanExecute(ItemsPopup.SelectedItem) ?? false)
        {
            SelectedItemCommand?.Execute(ItemsPopup.SelectedItem);
        }
    }

    private void OnGotFocus(object sender, RoutedEventArgs e)
    {
        Focused = true;
    }

    private void OnLostFocus(object sender, RoutedEventArgs e)
    {
        Focused = false;
    }

    private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(ToParentCommand?.CanExecute(null) ?? false)
            ToParentCommand?.Execute(null);
    }
}
