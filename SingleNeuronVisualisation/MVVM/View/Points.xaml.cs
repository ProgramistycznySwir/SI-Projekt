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


namespace SingleNeuronVisualisation.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy Points.xaml
    /// </summary>
    public partial class Points : Page
    {
        public static Points instance;

        public Points()
        {
            InitializeComponent();

            instance = this;

            DataContext = new PointsDataContext(4);
          
        }

        public static void InstanceTrainAndTest()
        {
            instance.DatatrainListBox.ItemsSource = MainWindow.data.Datasets_train;
            instance.DatatestListBox.ItemsSource = MainWindow.data.Datasets_test;
        }

       
        private void Button_Click(object sender, SelectionChangedEventArgs e) { }

        
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
