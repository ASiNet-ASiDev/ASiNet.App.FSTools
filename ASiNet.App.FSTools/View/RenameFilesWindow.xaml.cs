using System;
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
using System.Windows.Shapes;

namespace ASiNet.App.FSTools.View;
/// <summary>
/// Логика взаимодействия для RenameFilesWindow.xaml
/// </summary>
public partial class RenameFilesWindow : Window
{
    public RenameFilesWindow()
    {
        InitializeComponent();
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OkBtn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
