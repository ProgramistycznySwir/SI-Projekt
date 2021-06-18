using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Algorithm.Data;
using Algorithm.Marshalling;

namespace SingleNeuronVisualisation.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy Points.xaml
    /// </summary>
    public partial class Points : Page
    {
        public static List<Dataset> Datasets_train { get; set; }
        public static List<Dataset> Datasets_test { get; set; }

        public static PointsDataContext context { get; set; }
        public static Points instance { get; set; }

        //public static Dataset CurrentlySelectedDataset { get; set; }

        // public ObservableCollection<MLData> fileList = new ObservableCollection<MLData>();

        public Points()
        {
            InitializeComponent();
            context = new PointsDataContext();
            DataContext = context;
            instance = this;

            //DataContext = this;
        }

        public static void Setup()
        {
            instance.PointsList_train.ItemsSource = MainWindow.data.Datasets_train;
            instance.PointsList_test.ItemsSource = MainWindow.data.Datasets_test;
        }

        //void RenderPoints()
        //{
        //    if(CurrentlySelectedDataset)
        //    PointsDisplay.Children.Add()
        //}

        private void btn_Move_Click(object sender, RoutedEventArgs e)
        {
            if (PointsTabs.SelectedItem == Train)
            {
                int index = instance.PointsList_train.SelectedIndex;
                // Jest na odwrót w drugim parametrze.
                MainWindow.data.MoveDataset(index, false);
                PointsList_train.Items.Refresh();
            }
            else
            {
                int index = instance.PointsList_test.SelectedIndex;
                // Jest na odwrót w drugim parametrze.
                MainWindow.data.MoveDataset(index, true);
                PointsList_test.Items.Refresh();
            }
        }
        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            // Clear display.
            PointsDisplay.Children.RemoveRange(0, PointsDisplay.Children.Count);

            if (PointsTabs.SelectedItem is null)
                return;

            Dataset selectedDataset = (Dataset)(PointsTabs.SelectedItem == Train 
                    ? instance.PointsList_train 
                    : instance.PointsList_test)
                .SelectedItem;

            //Vector2 size = (PointsDisplay.ActualWidth, PointsDisplay.ActualHeight);
            Vector2 size = (PointsDisplay.RenderSize.Width, PointsDisplay.RenderSize.Height);
            Vector2 middle = size/2;


            double angle = selectedDataset.Solution;
            Line line = new();
            line.Stroke = Brushes.Green;
            line.StrokeThickness = 3;
            (line.X1, line.Y1) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI))*-1000;
            (line.X2, line.Y2) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI))*1000;
            PointsDisplay.Children.Add(line);

            foreach(var position in selectedDataset.GetPoints())
            {
                Ellipse point = new();
                point.Fill = Brushes.Black;
                (point.Width, point.Height) = (10, 10);
                point.VerticalAlignment = VerticalAlignment.Bottom;
                point.HorizontalAlignment = HorizontalAlignment.Left;

                // Normalized position.
                Vector2 nPosition = (position * 0.95 + 1) / 2;
                int x = (int)(nPosition.X * size.X) - 5;
                int y = (int)(nPosition.Y * size.Y) - 5;

                point.Margin = new Thickness(x, 0, 0, y);

                PointsDisplay.Children.Add(point);
            }
        }

        public class PointsDataContext
        {
            public string StrokeLineWidth { get; set; }
            // Jeszcze nie wiem co to robi.
            public List<Dataset> FileStore { get; set; }

            public PointsDataContext()
            {
                StrokeLineWidth = "4";
            }
        }

        //public ObservableCollection<MLData> FileStore
        //{
        //    get { return fileList; }
        //}
    }
}
