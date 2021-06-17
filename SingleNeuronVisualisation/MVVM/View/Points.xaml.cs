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
        //public static List<Dataset> Datasets_train { get; set; }
        //public static List<Dataset> Datasets_test { get; set; }
       
        // public ObservableCollection<MLData> fileList = new ObservableCollection<MLData>();

        public Points()
        {
            InitializeComponent();

            DataContext = new PointsDataContext(4);

            //DataContext = this;
        }

        public static void Setup()
        {
            //Datasets_train = MainWindow.data.Datasets_train;
            //Datasets_test = MainWindow.data.Datasets_test;
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, SelectionChangedEventArgs e) { }

        private void listView1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        public class PointsDataContext
        {
            public string StrokeLineWidth { get; set; }
            // Jeszcze nie wiem co to robi.
            public List<Point> FileStore { get; set; }

            public PointsDataContext(int width)
            {
                StrokeLineWidth = width.ToString();
            }
        }

        //public ObservableCollection<MLData> FileStore
        //{
        //    get { return fileList; }
        //}
    }
}
