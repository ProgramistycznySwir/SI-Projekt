using System;
using MathCatalogue;
using MachineLearningCatalogue;
using System.Collections.Generic;
using Algorithm;
using System.Linq;
using Algorithm.Data;

// Ważne: kąt w całym projekcie jest rozumieny jako A w tym wzorze: 360deg = A*PI
//  Czyli A= 0.5 to kąt prosty.
//  Kąt także jest mierzony wbrew wskazówek zegara.


namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Presentation();
            //return;
            SimpleTest();
            return;
            while (true)
            {
                RandomTest();
                Console.ReadKey();
                Console.WriteLine("\n");
            }
            return;

            const string filename_test = @"test.arff";

            MLData data = new(4);
            // data.AddDataset(new Dataset(0.75, (-1,-1), (1,1), (1,0), (-1,0)));
            // data.AddDataset(new Dataset(0.25, (0,1), (0.5,1), (1,0.5), (1,0)));

            // data.SaveToFile(filename_test);

            data = new(filename_test);
            Console.WriteLine(data.Datasets_train[0]);
            Console.WriteLine(data.Datasets_train[1]);
            //data.datasets_test.Add();


            //MLData data = new();
            //data.LoadFromFile(filename);

            //foreach (var dataset in data.datasets)
            //    Console.WriteLine(dataset);

        }

        static void Presentation()
        {
            MLData data = new("data.arff");

            Dataset set = new(0.5, (1, 0), (1, 1), (-1, 1), (-1, -1));
            data.AddDataset(set);
            set.CalculateSolution();
            data.AddDataset(set);
            set = Dataset.CreateRandom(4);
            data.AddDataset(set);


            SingleLayerNeuralNetwork network = new(
                inputNodes: data.DatasetSize * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);

            double targetError = 0.01f;
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(">> Baseline training result" + prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution });
                //break;
            }
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    Console.WriteLine($"Baseline: Failed for {dataset}");


            network = new(
                inputNodes: data.DatasetSize * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);
            data = DataNormalization.EqualizePointsLenght(data);
            targetError = 0.01f;
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(">> Normalized training result" + prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution });
                //break;
            }
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    Console.WriteLine($"Normalized: Failed for {dataset}");
        }

        static void RandomTest()
        {
            const int PointsAtInput = 20;
            SingleLayerNeuralNetwork network = new(
                inputNodes: PointsAtInput * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);

            MLData baselineData = new(PointsAtInput);
            for(int i = 0; i < 20; i++)
                baselineData.AddDataset(Dataset.CreateRandom(PointsAtInput));
            for(int i = 0; i < 20; i++)
                baselineData.AddDataset(Dataset.CreateRandom(PointsAtInput), true);

            MLData data = baselineData;
            double targetError = 0.01f;
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] {data.Datasets_train[ii].Solution});
                break;
            }

            // Wyświetlenie rezultatów:
            int fails = 0; int failedTests = 0;
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    fails++;
            foreach (var dataset in data.Datasets_test)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    failedTests++;
            Console.WriteLine($"Baseline: Failed in {fails} checks.");
            Console.WriteLine($"    Tests: {failedTests}");


            network = new(
                inputNodes: PointsAtInput * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);
            data = DataNormalization.MirrorPoints(baselineData);
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] {data.Datasets_train[ii].Solution});
                break;
            }

            // Wyświetlenie rezultatów:
            fails = 0; failedTests = 0;
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    fails++;
            foreach (var dataset in data.Datasets_test)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    failedTests++;
            Console.WriteLine($"Mirror: Failed in {fails} checks.");
            Console.WriteLine($"    Tests: {failedTests}");



            network = new(
                inputNodes: PointsAtInput * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);
            data = DataNormalization.EqualizePointsLenght(baselineData);
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution });
                break;
            }

            // Wyświetlenie rezultatów:
            fails = 0; failedTests = 0;
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    fails++;
            foreach (var dataset in data.Datasets_test)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    failedTests++;
            Console.WriteLine($"EqualizedLenght: Failed in {fails} checks.");
            Console.WriteLine($"    Tests: {failedTests}");



            network = new(
                inputNodes: PointsAtInput * 2,
                hiddenNodes: 1,
                outputNodes: 1,
                activator: MachineLearningCatalogue.Activator.Sigmoid);
            data = DataNormalization.MirrorPoints(DataNormalization.EqualizePointsLenght(baselineData));
            while (true)
            {
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                Console.WriteLine(prediction);
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                for (int i = 1_000; i > 0; i--)
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] { data.Datasets_train[ii].Solution });
                break;
            }

            // Wyświetlenie rezultatów:
            fails = 0; failedTests = 0;
            foreach (var dataset in data.Datasets_train)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    fails++;
            foreach (var dataset in data.Datasets_test)
                if (dataset.CheckIfSetIsDividedPropperlyBy(network.Predict(dataset.PointsData).First) is false)
                    failedTests++;
            Console.WriteLine($"AllNormalization: Failed in {fails} checks.");
            Console.WriteLine($"    Tests: {failedTests}");
        }

        static void SimpleTest()
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

            // Tutaj tworzone są zbiory danych.
            MLData data = new(4);
            data.AddDataset(new Dataset(
                0.75, (-1, -1), (1, 1), (1, 0), (-1, 0) ));
            data.AddDataset(new Dataset(
                0.25, (0, 1), (0.5, 1), (1, 0.5), (1, 0) ));
            

            Console.WriteLine(data.Datasets_train[0].CheckIfSetIsDividedPropperlyBy(0.75));
            Console.WriteLine(data.Datasets_train[1].CheckIfSetIsDividedPropperlyBy(0.375));

            data.Datasets_train[0].CalculateSolution();
            Console.WriteLine(data.Datasets_train[0].Solution);
            data.Datasets_train[1].CalculateSolution();
            Console.WriteLine(data.Datasets_train[1].Solution);
            //return;


            // Tolerancja poniżej której sieć ma przestać się uczyć.
            double targetError = 0.01f;
            // W obrębie tej pętli dochodzi do treningu:
            while (true)
            {
                // Tutaj pobierana jest przewidywana wartość na potrzeby stwierdzenia trafności algorytmu.
                //  To wszystko są operacje na macierzach, ale z racji, że macierz wyjściowa jest wymiarów 1x1 to można
                //  zwyczajnie pobrać pierwszą wartość za pomocą .First.
                double prediction = network.Predict(data.Datasets_train[0].PointsData).First;
                // Dla celów samego wyświetlania.
                Console.WriteLine(prediction);
                // Sprawdzanie czy przewidziana wartość nie mieści się w granicach tolerancji.
                if (data.Datasets_train[0].CalculateError(prediction) < targetError)
                    break;
                // W tych 2 pętlach dochodzi do właściwego treningu.
                // Ta pętla jest odpowiedzialna za iterację.
                for (int i = 100; i > 0; i--)
                    // Ta pętla odpowiedzialna jest za wsadzanie w sieć wszystkich wierszy.
                    for (int ii = data.Datasets_train.Count - 1; ii >= 0; ii--)
                        network.Train(data.Datasets_train[ii].PointsData, new double[] {data.Datasets_train[ii].Solution});
            }

            network.WeightsHo.Info("WeightsHo");
            network.WeightsIh.Info("WeightsIh");
            network.BiasHo.Info("BiasHo");
            network.BiasIh.Info("BiasIh");
            // Wyświetlenie rezultatów:
            Console.WriteLine($"0: {network.Predict(data.Datasets_train[0].PointsData)}");
            Console.WriteLine($"1: {network.Predict(data.Datasets_train[1].PointsData)}");
        }
    }
}
