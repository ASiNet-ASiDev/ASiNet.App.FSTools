using System.Windows;
using System.Windows.Controls;

namespace ASiNet.FSTools.Controls
{
    /// <summary>
    /// Логика взаимодействия для PageContainer.xaml
    /// </summary>
    public partial class PageContainer : Grid
    {
        public PageContainer(int index)
        {
            Index = index;
            InitializeComponent();
            TitleText.Text = $"Container #{index}";
        }

        public int Index { get; }

        private FrameworkElement? _currentChild;

        public void SetContent(FrameworkElement? element)
        {
            if (_currentChild is not null)
                Root.Children.Remove(_currentChild);
            if (element is not null)
            {
                Root.Children.Add(element);
                _currentChild = element;
                Panel.SetZIndex(element, 1);
            }
        }

    }
}
