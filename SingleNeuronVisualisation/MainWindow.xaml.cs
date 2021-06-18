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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() is true)
            {
                string filename = openFileDialog.FileName;
                data = new MLData(filename);
                InitializeNeuralNetwork(data);
            }
        }
        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            int PointsAtInput = 0, DatasetCount = 0;
            try
            {
                PointsAtInput = int.Parse(DatasetCountText.Text);
                DatasetCount = int.Parse(PointCountText.Text);
            }
            catch(Exception _)
            {
                MessageBox.Show("Zły format parametrów wejściowych!");
                return;
            }

            data = new(PointsAtInput);
            for (int i = 0; i < DatasetCount; i++)
                data.AddDataset(Dataset.CreateRandom(PointsAtInput));
            for (int i = 0; i < DatasetCount; i++)
                data.AddDataset(Dataset.CreateRandom(PointsAtInput), true);
            MainWindow.data = data;
            InitializeNeuralNetwork(data);
        }

        private void btn_Step_Click(object sender, RoutedEventArgs e)
        {
            TeachAlgorithm(1, true);
        }
        private void btn_100Step_Click(object sender, RoutedEventArgs e)
        {
            int howManySteps;
            if (int.TryParse(HowManyStepsText.Text, out howManySteps) is false)
                return;
            TeachAlgorithm(howManySteps, true);
        }
        private void btn_Auto_Click(object sender, RoutedEventArgs e)
        {
            float minimumCorrectness = 1;
            if (float.TryParse(MinimumCorrectnessText.Text, out minimumCorrectness) is false)
            {
                MessageBox.Show("Minimalna poprawność musi być liczbą w zasięgu 0-1.");
                return;
            }

            int iterations = 0;

            while(minimumCorrectness > TeachAlgorithm(100, true))
            {
                iterations++;
                if(iterations > 100)
                {
                    MessageBox.Show("Algorytm nie osiągnął zamierzonego błędu po 10_000 iteracji!");
                    return;
                }
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
            Points.Setup();
        }
                

        public float TeachAlgorithm(int iterations, bool calculatePredictions = false)
        {
            float correctness_train = 0;

            for (int i = iterations; i > 0; i--)
            {
                int correctPredictions_train = 0; int correctPredictions_test = 0;
                // Training:
                for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                    if (data.Datasets_train[ii].CheckIfSetIsDividedPropperlyBy(
                        network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution }).First))
                        correctPredictions_train++;

                // Calculating errors:
                if (calculatePredictions is false)
                    continue;
                foreach (var dataset in data.Datasets_test)
                    if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First))
                        correctPredictions_test++;

                correctness_train = data.Datasets_train.Count is 0 ? 0 : correctPredictions_train / (float)data.Datasets_train.Count;
                float correctness_test = data.Datasets_test.Count is 0 ? 0 : correctPredictions_test / (float)data.Datasets_test.Count;
                //Charts.AddResultsWrapper(correctness_train, correctness_test);
                var temp = Charts.instance;
                Charts.instance.AddResults(correctness_train, correctness_test);
                CorrectnessText.Text = $"Model correctness:  Train: {(int)(correctness_train*100)}%  Test: {(int)(correctness_test*100)}%";
            }

            Neuron.RefreshWeightsWrapper();
            return correctness_train;
        }
    }
}

