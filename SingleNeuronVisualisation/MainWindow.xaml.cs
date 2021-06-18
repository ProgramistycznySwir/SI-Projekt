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
using Algorithm;
using SingleNeuronVisualisation.MVVM;

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

        private void btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Throw this 3 lines out.
            data = new MLData(System.IO.Path.Combine(Environment.CurrentDirectory, "test.arff"));
            InitializeNeuralNetwork(data);
            return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() is true)
            {
                string filename = openFileDialog.FileName;
                data = new MLData(filename);
                InitializeNeuralNetwork(data);
            }
        }

        private void btn_Step_Click(object sender, RoutedEventArgs e)
        {
            TeachAlgorithm(1, true);
        }

        private void InitializeNeuralNetwork(MLData data)
        {
            network = new(
                inputNodes: data.DatasetSize * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);

            Neuron.DrawNeuronsWrapper();
            Points.Setup();
        }
                

        public void TeachAlgorithm(int iterations, bool calculatePredictions = false)
        {
            for (int i = iterations; i > 0; i--)
            {
                // Training:
                for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                    network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution });

                // Calculating errors:
                if (calculatePredictions is false)
                    continue;
                int correctPredictions_train = 0; int correctPredictions_test = 0;
                foreach (var dataset in data.Datasets_train)
                    if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First))
                        correctPredictions_train++;
                foreach (var dataset in data.Datasets_test)
                    if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First))
                        correctPredictions_test++;
                Charts.AddResultsWrapper(correctPredictions_train / data.Datasets_train.Count,
                    correctPredictions_test / data.Datasets_test.Count);
            }
        }
    }
}

