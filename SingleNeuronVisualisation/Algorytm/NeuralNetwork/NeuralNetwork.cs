using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

using MathCatalogue;

namespace MachineLearningCatalogue
{
    public class NeuralNetwork
    {
        private Random Random { get; }
        public double LearningRate { get; }

        public List<int> LayerNodes { get; }
        public List<Matrix> Weights { get; }
        public List<Matrix> Biases { get; }
        private Activator Activator { get; }


        public NeuralNetwork(double learningRate, Activator activator, params int[] layerNodes)
        {
            if (layerNodes.Length < 3) 
                throw new ArgumentException("Neural Network needs at least 3 layers");
            Random = new Random();
            LayerNodes = layerNodes.ToList();
            LearningRate = learningRate;
            Activator = activator;

            Weights = Range(1, LayerNodes.Count - 1)
                .Select(i => Random.NextMatrix(LayerNodes[i], LayerNodes[i - 1])).ToList();
            Biases = Range(1, LayerNodes.Count - 1)
                .Select(i => Random.NextMatrix(LayerNodes[i], 1)).ToList();
        }

        public void Train(double[] inputs, double[] targets)
        {
            var memory = new List<Matrix>();

            var outputs = new Matrix(inputs);
            foreach (var (weight, bias) in Weights.Zip(Biases))
            {
                outputs = weight.Product(outputs).Add(bias).Map(Activator.Activate);
                memory.Add(outputs);
            }

            Matrix errors = null!;
            for (var i = 1; i <= memory.Count; ++i)
            {
                errors = i == 1
                    ? new Matrix(targets).Subtract(memory[^i])
                    : Weights[^(i - 1)].Transpose().Product(errors);
                Matrix gradients = memory[^i].Map(Activator.Derive).Hadamard(errors).Product(LearningRate);
                Matrix weightDeltas =
                    gradients.Product((i == memory.Count ? new Matrix(inputs) : memory[^(i + 1)]).Transpose());
                Weights[^i] = Weights[^i].Add(weightDeltas);
                Biases[^i] = Biases[^i].Add(gradients);
            }
        }

        public Matrix Predict(params double[] inputs)
        {
            var outputs = new Matrix(inputs);
            foreach (var (weight, bias) in Weights.Zip(Biases))
                outputs = weight.Product(outputs).Add(bias).Map(Activator.Activate);
            return outputs.Transpose();
        }
    }
}
