using Algorithm.Data;
using SingleNeuronVisualisation.MVVM.View;
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

namespace SingleNeuronVisualisation.MVVM
{
    /// <summary>
    /// Logika interakcji dla klasy Charts.xaml
    /// </summary>
    public partial class Charts : Page
    {
        public MLData ml { get; set; }
        public static Charts instance { get; set; }
        public Charts()
        {
            InitializeComponent();
            instance = this;
        }

        int DataPointsCount = 0;

        public static void AddResultsWrapper(float train, float test) => instance.AddResults(train, test);

        public void AddResults(float train, float test)
        {
            listTraining.AddLast(train);
            listTesting.AddLast(test);
            DataPointsCount++;

            Line_train.Points.Add(new Point(DataPointsCount*10, train * Viewport.ActualHeight));
            Line_test.Points.Add(new Point(DataPointsCount*10, test * Viewport.ActualHeight));
        }

        LinkedList<float> listTraining = new ();
        LinkedList<float> listTesting = new ();
        
        
    }
}
