using System;
using MathCatalogue;

// ReSharper disable once CheckNamespace
namespace MachineLearningCatalogue
{
    public class SingleLayerNeuralNetwork
    {
        private Random Random { get; }
        private int InputNodes { get; }
        private int HiddenNodes { get; }
        private int OutputNodes { get; }
        public double LearningRate { get; }

        public Matrix WeightsIh { get; set; }
        public Matrix WeightsHo { get; set; }
        public Matrix BiasIh { get; set; }
        public Matrix BiasHo { get; set; }

        private Activator Activator { get; }
        public SingleLayerNeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes, Activator activator
            , double learningRate = 0.1)
        {
            Random = new Random();
            InputNodes = inputNodes;
            HiddenNodes = hiddenNodes;
            OutputNodes = outputNodes;
            Activator = activator;
            LearningRate = learningRate;
            WeightsIh = Random.NextMatrix(HiddenNodes, InputNodes);
            BiasIh = Random.NextMatrix(HiddenNodes, 1);

            WeightsHo = Random.NextMatrix(OutputNodes, HiddenNodes);
            BiasHo = Random.NextMatrix(OutputNodes, 1);
        }

        public void Train(double[] inputs, double[] targets)
        {
            var hidden = WeightsIh.Product(new Matrix(inputs)).Add(BiasIh).Map(Activator.Activate);
            var outputs = WeightsHo.Product(hidden).Add(BiasHo).Map(Activator.Activate);

            var outputErrors = new Matrix(targets).Subtract(outputs);
            var gradients = outputs.Map(Activator.Derive).Hadamard(outputErrors).Product(LearningRate);
            var weightHoDeltas = gradients.Product(hidden.Transpose());

            var hiddenErrors = WeightsHo.Transpose().Product(outputErrors);
            var hiddenGradient = hidden.Map(Activator.Derive).Hadamard(hiddenErrors).Product(LearningRate);
            var weightIhDeltas = hiddenGradient.Product(new Matrix(inputs).Transpose());

            WeightsHo = WeightsHo.Add(weightHoDeltas);
            BiasHo = BiasHo.Add(gradients);
            WeightsIh = WeightsIh.Add(weightIhDeltas);
            BiasIh = BiasIh.Add(hiddenGradient);
        }

        public Matrix Predict(params double[] inputs)
        {
            // Generating Hidden
            var hidden = WeightsIh.Product(new Matrix(inputs)).Add(BiasIh).Map(Activator.Activate);

            // Generating Output
            var outputs = WeightsHo.Product(hidden).Add(BiasHo).Map(Activator.Activate).Transpose();

            return outputs;
        }
    }
}