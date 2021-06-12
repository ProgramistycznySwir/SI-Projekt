using System;
using MathCatalogue;
using MachineLearningCatalogue;
using System.Collections.Generic;
using Algorytm.Marshalling;
using System.Linq;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // HowToUse101:

            // Stała tak żeby nie się nie powtarzać.
            const int PointsAtInput = 4;

            // Tworzona jest sieć neuronowa gdzie kolejno:
            //  - inputNodes to ilość punktów razy 2,
            //  - hiddenNodes to właśnie perceptron,
            //  - outputNodes to wyjście, czyli tylko 1,
            //  - activator to funkcja aktywacyjna, na razie zaimplementowane są 2 (aż nadto).
            SingleLayerNeuralNetwork network = new(
                inputNodes: PointsAtInput * 2, 
                hiddenNodes: 1, 
                outputNodes: 1, 
                activator: MachineLearningCatalogue.Activator.Sigmoid);

            // Tutaj tworzona jest lista punktów, jest to jedynie ze względów estetycznych, później ta lista jest
            //  przetwarzana w przyjazny sieci neuronowej format.
            List<Point> points_input = new()
                {
                    (-1, -1), (1, 1), (1, 0), (-1, 0),
                    (0, 1), (0.5, 1), (1, 0.5), (1, 0),
                };
            // Tutaj tworzona jest lista wartości docelowych.
            List<double> targets_input = new()
                {
                    0.75,
                    0.25,
                };

            // Tutaj obie listy z góry są przetwarzane w format który przyjmuje sieć neuronowa.
            var points = Point.Aggregate(points_input, PointsAtInput);
            var targets = targets_input.Select(item => new double[] { item }).ToList();


            // Tolerancja poniżej której sieć ma przestać się uczyć.
            double tolerance = 0.01f;
            // W obrębie tej pętli dochodzi do treningu:
            while(true)
            {
                // Tutaj pobierana jest przewidywana wartość na potrzeby stwierdzenia trafności algorytmu.
                //  To wszystko są operacje na macierzach, ale z racji, że macierz wyjściowa jest wymiarów 1x1 to można
                //  zwyczajnie pobrać pierwszą wartość za pomocą .First.
                double prediction = network.Predict(points[0]).First;
                // Dla celów samego wyświetlania.
                Console.WriteLine(prediction);
                // Sprawdzanie czy przewidziana wartość nie mieści się w granicach tolerancji.
                if (Math.Abs(prediction - targets[0][0]) < tolerance)
                    break;
                // W tych 2 pętlach dochodzi do właściwego treningu.
                // Ta pętla jest odpowiedzialna za iterację.
                for (int i = 0; i < 100; i++)
                    // Ta pętla odpowiedzialna jest za wsadzanie w sieć wszystkich wierszy.
                    for (int ii = points.Count - 1; ii >= 0; ii--)
                        network.Train(points[ii], targets[ii]);
            }
            
            // Wyświetlenie rezultatów:
            Console.WriteLine($"0: {network.Predict(points[0])}");
            Console.WriteLine($"1: {network.Predict(points[1])}");
        }
    }
}
