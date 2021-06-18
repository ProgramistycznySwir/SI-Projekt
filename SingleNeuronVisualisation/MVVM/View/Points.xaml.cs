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
using System.Windows.Threading;
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

        Vector2 size, middle;
        Line predictedLine;

        public Points()
        {
            instance = this;

            InitializeComponent();
            context = new PointsDataContext();
            DataContext = context;
        }

        public static void Setup()
        {
            instance.PointsList_train.ItemsSource = MainWindow.data.Datasets_train;
            instance.PointsList_test.ItemsSource = MainWindow.data.Datasets_test;
        }


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
            size = (PointsDisplay.RenderSize.Width, PointsDisplay.RenderSize.Height);
            middle = size/2;


            // Desired line:
            double angle = selectedDataset.Solution;
            Line line = new();
            line.Stroke = Brushes.Green;
            line.StrokeThickness = 3;
            (line.X1, line.Y1) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI))*-1000;
            (line.X2, line.Y2) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI))*1000;
            PointsDisplay.Children.Add(line);

            // Predicted line:
            predictedLine = new();
            predictedLine.Stroke = Brushes.Red;
            predictedLine.StrokeThickness = 3;
            (predictedLine.X1, predictedLine.Y1) = middle + new Vector2(Math.Cos(0 * Math.PI), Math.Sin(-0 * Math.PI)) * -1000;
            (predictedLine.X2, predictedLine.Y2) = middle + new Vector2(Math.Cos(0 * Math.PI), Math.Sin(-0 * Math.PI)) * 1000;
            PointsDisplay.Children.Add(predictedLine);


            foreach (var position in selectedDataset.GetPoints())
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
        private void brn_Predict_Click(object sender, RoutedEventArgs e)
        {
            int index;
            if (PointsTabs.SelectedItem == Train)
            {
                index = PointsList_train.SelectedIndex;
                if (index >= MainWindow.data.Datasets_train.Count || index < 0)
                    return;
                UpdateLine(MainWindow.network.Predict(MainWindow.data.Datasets_train[index].PointsData).First);
            }
            else
            {
                index = PointsList_test.SelectedIndex;
                if (index >= MainWindow.data.Datasets_test.Count || index < 0)
                    return;
                UpdateLine(MainWindow.network.Predict(MainWindow.data.Datasets_test[index].PointsData).First);
            }
        }

        public void UpdateLine(double angle)
        {
            (predictedLine.X1, predictedLine.Y1) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI)) * -1000;
            (predictedLine.X2, predictedLine.Y2) = middle + new Vector2(Math.Cos(angle * Math.PI), Math.Sin(-angle * Math.PI)) * 1000;
            //(predictedLine.X1, predictedLine.Y1) = (-100, -100);
            //(predictedLine.X2, predictedLine.Y2) = (100, 100);
            //predictedLine.HorizontalAlignment = HorizontalAlignment.Left;
            //predictedLine.VerticalAlignment = VerticalAlignment.Bottom;
            PointsDisplay.Children.Remove(predictedLine);
            PointsDisplay.Children.Add(predictedLine);
        }

        public class PointsDataContext
        {
            public string StrokeLineWidth { get; set; }

            public PointsDataContext()
            {
                StrokeLineWidth = "4";
            }
        }
    }
}
