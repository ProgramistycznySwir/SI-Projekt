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

namespace SingleNeuronVisualisation.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy Points.xaml
    /// </summary>
    public partial class Points : Page
    {
        public Points()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonGeneratePoints_Click(object sender, RoutedEventArgs e)
        {
           
        }

        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            scrollViewer.CaptureMouse();
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
            }
        }

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }

        /*private PointCollection[] DataPoints = new PointCollection[3];
        private Brush[] DataBrushes =
            { Brushes.Red, Brushes.Green, Brushes.Blue };

        // Find the data point at this device coordinate location.
        // Return data_set = -1 if there is no point at this location.
        private void FindDataPoint(Point location,
            out int data_set, out int point_number)
        {
            // Check each data set.
            for (data_set = 0; data_set < DataPoints.Length; data_set++)
            {
                // Check this data set.
                for (point_number = 0;
                    point_number < DataPoints[data_set].Count;
                    point_number++)
                {
                    // See how far the location is from the data point.
                    Point data_point = DataPoints[data_set][point_number];
                    Vector vector = location - data_point;
                    double dist = vector.Length;
                    if (dist < 3) return;
                }
            }

            // We didn't find a point at this location.
            data_set = -1;
            point_number = -1;
        }

        private Ellipse DataEllipse = null;
        private Label DataLabel = null;

        // See if the mouse is over a data point.
        private void canGraph_MouseUp(
            object sender, MouseButtonEventArgs e)
        {
            // Find the data point at the mouse's location.
            Point mouse_location = e.GetPosition(scrollViewer);
            int data_set, point_number;
            FindDataPoint(mouse_location, out data_set, out point_number);
            if (data_set < 0) return;
            Point data_point = DataPoints[data_set][point_number];

            // Make the data ellipse if we haven't already.
            if (DataEllipse == null)
            {
                DataEllipse = new Ellipse();
                DataEllipse.Fill = null;
                DataEllipse.StrokeThickness = 1;
                DataEllipse.Width = 7;
                DataEllipse.Height = 7;
                scrollViewer.Children.Add(DataEllipse);
            }

            // Color and position the ellipse.
            DataEllipse.Stroke = DataBrushes[data_set];
            Canvas.SetLeft(DataEllipse, data_point.X - 3);
            Canvas.SetTop(DataEllipse, data_point.Y - 3);

            // Make the data label if we haven't already.
            if (DataLabel == null)
            {
                DataLabel = new Label();
                DataLabel.FontSize = 12;
                canGraph.Children.Add(DataLabel);
            }

            // Convert the data values back into world coordinates.
            Point world_point = DtoW(data_point);

            // Set the data label's text and position it.
            DataLabel.Content = "(" +
                world_point.X.ToString("0.0") + ", " +
                world_point.Y.ToString("0.0") + ")";
            DataLabel.Measure(new Size(double.MaxValue, double.MaxValue));
            Canvas.SetLeft(DataLabel, data_point.X + 4);
            Canvas.SetTop(DataLabel,
                data_point.Y - DataLabel.DesiredSize.Height);
        }

        private void canGraph_MouseMove(object sender, MouseEventArgs e)
        {
            // Find the data point at the mouse's location.
            Point mouse_location = e.GetPosition(scrollViewer);
            int data_set, point_number;
            FindDataPoint(mouse_location, out data_set, out point_number);

            // Display the appropriate cursor.
            if (data_set < 0)
                scrollViewer.Cursor = null;
            else
                scrollViewer.Cursor = Cursors.UpArrow;
        }*/
    }
}
