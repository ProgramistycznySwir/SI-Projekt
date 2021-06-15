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
using Algorithm;

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
            DataContext = new PointsDataContext(8);
        }

        private void Window_SizeChanged()
        {
            // calculates incorrect when window is maximized
            PointsDisplay.Width;
            VerticalLine.
        }

        public class PointsDataContext
        {
            public string StrokeLineWidth { get; set; }
            public PointsDataContext(int width)
            {
                StrokeLineWidth = width.ToString();
            }
        }
    }
}
