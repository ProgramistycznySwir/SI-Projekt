//using Microsoft.Win32;
using Algorithm.Data;
using MachineLearningCatalogue;
using Microsoft.Win32;
using SingleNeuronVisualisation.MVVM.View;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SingleNeuronVisualisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string SourceFileName { get; set; }

        public static MLData data { get; private set; }
        public static SingleLayerNeuralNetwork network;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() is true)
            {
                string filename = openFileDialog.FileName;
                data = new MLData(filename);
                InitializeNeuralNetwork(data);
            }
        }

        private void InitializeNeuralNetwork(MLData data)
        {
            network = new(
                inputNodes: data.DatasetSize * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);

            Neuron.DrawNeuronsWrapper();
        }
    }
}
