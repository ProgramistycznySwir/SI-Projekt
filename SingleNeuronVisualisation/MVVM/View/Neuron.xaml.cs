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
        public static TextBlock nodeTexts { get; set; }

        public Neuron()
        {
            InitializeComponent();
            instance = this;
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
            // First add lines.
            for (int i = 0; i < inputNodeCount; i++)
            {
                Image inputNode = new();
                inputNode.Width = 60;
                inputNode.Height = 60;
                inputNode.Margin = new Thickness(positions[i].X, positions[i].Y, 0, 0);

                //inputNode.HorizontalAlignment = HorizontalAlignment.Center;
                //inputNode.VerticalAlignment = VerticalAlignment.Center;
                //inputNode.Source = new BitmapImage(new Uri(@"\Images\Input.png"));
                inputNode.Source = new BitmapImage(new Uri(@"C:\USB SZTYK BEKAP 11-03-2021\Semestr4\Sztuczna Inteligencja\Projekt\SingleNeuronVisualisation\Images\Input.png"));
                //inputNode.Fill = Brushes.Black;
                MainCanvas.Children.Add(inputNode);
            }
            // Then nodes.
            return;
            for (int i = 0; i < inputNodeCount; i++)
            {
                Line inputNode = new();
                inputNode.Width = 60;
                inputNode.Height = 60;


                MainCanvas.Children.Add(inputNode);
            }            
        }
    }
}
