using Algorithm.Marshalling;
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
        // Store so they can be updated.
        public static List<TextBlock> nodeTexts { get; set; }

        public Neuron()
        {
            InitializeComponent();
            instance = this;
            nodeTexts = new();
        }

        // Just wrapper
        public static void DrawNeuronsWrapper() => instance.DrawNeurons();

        public void DrawNeurons()
        {
            int inputNodeCount = MainWindow.data.DatasetSize * 2;
            Vector2[] positions = new Vector2[inputNodeCount];
            for (int i = 0; i < inputNodeCount; i++)
            {
                positions[i].X = 60;
                positions[i].Y = i * 60;
            }

            Point neuronPosition = MainNeuron.TranslatePoint(new Point(0,0), (VisualTreeHelper.GetParent(MainNeuron) as UIElement));

            // First add lines.
            for (int i = 0; i < inputNodeCount; i++)
            {
                Line line = new();
                line.X1 = positions[i].X + 30;
                line.Y1 = positions[i].Y + 30;
                line.X2 = neuronPosition.X + 30;
                line.Y2 = neuronPosition.Y + 30;
                line.StrokeThickness = 4;
                line.Stroke = Brushes.Gray;

                MainCanvas.Children.Add(line);
            }
            // Then nodes.
            for (int i = 0; i < inputNodeCount; i++)
            {
                Image inputNode = new();
                inputNode.Width = 60;
                inputNode.Height = 60;
                inputNode.Margin = new Thickness(positions[i].X, positions[i].Y, 0, 0);
                inputNode.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\Input.png")));

                TextBlock nodeText = new();
                nodeText.Margin = new Thickness(positions[i].X + 60, positions[i].Y, 0, 0);
                nodeText.Text = $"Input{i}:\n weight: NaN";

                //inputNode.Source = new BitmapImage(new Uri(@"C:\USB SZTYK BEKAP 11-03-2021\Semestr4\Sztuczna Inteligencja\Projekt\SingleNeuronVisualisation\Images\Input.png"));
                //inputNode.Fill = Brushes.Black;
                MainCanvas.Children.Add(inputNode);
                MainCanvas.Children.Add(nodeText);
                nodeTexts.Add(nodeText);
            }            
        }
    }
}
