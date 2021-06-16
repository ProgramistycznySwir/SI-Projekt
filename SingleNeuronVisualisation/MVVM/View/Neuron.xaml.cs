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
    /// Logika interakcji dla klasy Neuron.xaml
    /// </summary>
    public partial class Neuron : Page
    {
        // Singleton
        public static Neuron instance { get; private set; }

        public Neuron()
        {
            InitializeComponent();
            instance = this;
        }

        // Just wrapper
        public static void DrawNeuronsWrapper() => instance.DrawNeurons();

        public void DrawNeurons()
        {
            int inputNodeCount = MainWindow.data.DatasetSize;
            for (int i = 0; i < inputNodeCount; i++)
            {
                Image inputNode = new();
                inputNode.Width = 60;
                inputNode.Height = 60;
                //inputNode.HorizontalAlignment = HorizontalAlignment.Center;
                //inputNode.VerticalAlignment = VerticalAlignment.Center;
                inputNode.Source = new BitmapImage(new Uri(@"\Images\Input.png"));
                //inputNode.Fill = Brushes.Black;
                MainCanvas.Children.Add(inputNode);
            }            
        }
    }
}
