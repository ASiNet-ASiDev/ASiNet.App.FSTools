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
using ASiNet.FSTools.Controls.Enums;

namespace ASiNet.FSTools.Controls
{
    /// <summary>
    /// Логика взаимодействия для PagesView.xaml
    /// </summary>
    public partial class PagesView : Grid
    {
        public PagesView()
        {
            InitializeComponent();
            SetSplitMod(SplitMode);
        }

        public readonly static DependencyProperty SplitModeProperty = DependencyProperty.Register(nameof(SplitMode), typeof(PagesViewSplitMode), typeof(PagesView), new PropertyMetadata(null));

        public PagesViewSplitMode SplitMode
        {
            get { return (PagesViewSplitMode)GetValue(SplitModeProperty); }
            set { SetValue(SplitModeProperty, value); }
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.Property.Name)
            {
                case nameof(SplitMode):
                    SetSplitMod((PagesViewSplitMode)e.NewValue);
                    break;
            }
            base.OnPropertyChanged(e);
        }

        private void SetSplitMod(PagesViewSplitMode splitMode)
        {
            switch (splitMode)
            {
                case PagesViewSplitMode.VerticalTwoAreas: VDITA(); break;
                case PagesViewSplitMode.HorizontalTwoAreas: HDITA(); break;
                case PagesViewSplitMode.HorizontalTwoAndVerticalButtonOneAreas: HTAVBOA(); break;
                case PagesViewSplitMode.HorizontalTwoAndVerticalTopOneAreas: HTAVTOA(); break;
            }
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

            SetColumn(pc[1], 2);
            SetRow(pc[1], 2);
            SetRow(pc[2], 2);
        }

        private IEnumerable<PageContainer> CreatePagesContainers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var pc = new PageContainer();
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
                if(i < areasCount - 1)
                {
                    var splitter = new VerticalPageAreaSplitter();
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
                    var splitter = new HorizontalPageAreaSplitter();
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
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
        }
    }
}
