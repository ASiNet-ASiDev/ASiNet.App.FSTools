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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASiNet.FSTools.Controls;
/// <summary>
/// Логика взаимодействия для FilesContextMenu.xaml
/// </summary>
public partial class FilesContextMenu : ContextMenu
{
    public FilesContextMenu()
    {
        InitializeComponent();
    }

    public readonly static DependencyProperty OpenFileCommandProperty = DependencyProperty.Register(nameof(OpenFileCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty RenameFileCommandProperty = DependencyProperty.Register(nameof(RenameFileCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty MoveFileCommandProperty = DependencyProperty.Register(nameof(MoveFileCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty CopyFileCommandProperty = DependencyProperty.Register(nameof(CopyFileCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty DeleteFileCommandProperty = DependencyProperty.Register(nameof(DeleteFileCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty SelectAllCommandProperty = DependencyProperty.Register(nameof(SelectAllCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty UnselectAllCommandProperty = DependencyProperty.Register(nameof(UnselectAllCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty BackToParentCommandProperty = DependencyProperty.Register(nameof(BackToParentCommand), typeof(ICommand), typeof(FilesContextMenu), new PropertyMetadata(null));
    public readonly static DependencyProperty FileSpecificMenuOptionsIsEnabledProperty = DependencyProperty.Register(nameof(FileSpecificMenuOptionsIsEnabled), typeof(bool), typeof(FilesContextMenu), new PropertyMetadata(null));

    public event Action? OpenFile;
    public event Action? RenameFile;
    public event Action? MoveFile;
    public event Action? CopyFile;
    public event Action? DeleteFile;
    public event Action? SelectAllFiles;
    public event Action? UnselectAllFiles;
    public event Action? BackToParentFile;

    public bool FileSpecificMenuOptionsIsEnabled
    {
        get { return (bool)GetValue(FileSpecificMenuOptionsIsEnabledProperty); }
        set { SetValue(FileSpecificMenuOptionsIsEnabledProperty, value); }
    }

    public ICommand? OpenFileCommand
    {
        get { return GetValue(OpenFileCommandProperty) as ICommand; }
        set { SetValue(OpenFileCommandProperty, value); }
    }

    public ICommand? RenameFileCommand
    {
        get { return GetValue(RenameFileCommandProperty) as ICommand; }
        set { SetValue(RenameFileCommandProperty, value); }
    }

    public ICommand? MoveFileCommand
    {
        get { return GetValue(MoveFileCommandProperty) as ICommand; }
        set { SetValue(MoveFileCommandProperty, value); }
    }

    public ICommand? CopyFileCommand
    {
        get { return GetValue(CopyFileCommandProperty) as ICommand; }
        set { SetValue(CopyFileCommandProperty, value); }
    }

    public ICommand? DeleteFileCommand
    {
        get { return GetValue(DeleteFileCommandProperty) as ICommand; }
        set { SetValue(DeleteFileCommandProperty, value); }
    }

    public ICommand? SelectAllCommand
    {
        get { return GetValue(SelectAllCommandProperty) as ICommand; }
        set { SetValue(SelectAllCommandProperty, value); }
    }

    public ICommand? UnselectAllCommand
    {
        get { return GetValue(UnselectAllCommandProperty) as ICommand; }
        set { SetValue(UnselectAllCommandProperty, value); }
    }

    public ICommand? BackToParentCommand
    {
        get { return GetValue(BackToParentCommandProperty) as ICommand; }
        set { SetValue(BackToParentCommandProperty, value); }
    }

    private void OpenFileMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if(OpenFileCommand?.CanExecute(null) ?? false)
            OpenFileCommand?.Execute(false);
        OpenFile?.Invoke();
    }

    private void RenameFileMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (RenameFileCommand?.CanExecute(null) ?? false)
            RenameFileCommand?.Execute(false);
        RenameFile?.Invoke();
    }

    private void MoveFileMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (MoveFileCommand?.CanExecute(null) ?? false)
            MoveFileCommand?.Execute(false);
        MoveFile?.Invoke();
    }

    private void CopyFileMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (CopyFileCommand?.CanExecute(null) ?? false)
            CopyFileCommand?.Execute(false);
        CopyFile?.Invoke();
    }

    private void DeleteFileMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteFileCommand?.CanExecute(null) ?? false)
            DeleteFileCommand?.Execute(false);
        DeleteFile?.Invoke();
    }

    private void SelectAllMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (SelectAllCommand?.CanExecute(null) ?? false)
            SelectAllCommand?.Execute(false);
        SelectAllFiles?.Invoke();
    }

    private void UnselectAllMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (UnselectAllCommand?.CanExecute(null) ?? false)
            UnselectAllCommand?.Execute(false);
        UnselectAllFiles?.Invoke();
    }

    private void BackToParentMenuBtn_Click(object sender, RoutedEventArgs e)
    {
        if (BackToParentCommand?.CanExecute(null) ?? false)
            BackToParentCommand?.Execute(false);
        BackToParentFile?.Invoke();
    }
}
