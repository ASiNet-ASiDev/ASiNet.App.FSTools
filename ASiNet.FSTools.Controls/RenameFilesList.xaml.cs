using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASiNet.FSTools.Controls;
/// <summary>
/// Логика взаимодействия для RenameFilesList.xaml
/// </summary>
public partial class RenameFilesList : Grid
{
    public RenameFilesList()
    {
        InitializeComponent();
    }

    public readonly static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(RenameFilesList), new PropertyMetadata(null));


    public IEnumerable? ItemsSource
    {
        get { return GetValue(ItemsSourceProperty) as IEnumerable; }
        set { SetValue(ItemsSourceProperty, value); }
    }
}
