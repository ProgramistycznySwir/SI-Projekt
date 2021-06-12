using System;

namespace MachineLearningCatalogue
{
    public class Activator
    {
        private Activator(Func<double, double> activate, Func<double, double> derive)
            => (Activate, Derive) = (activate, derive);

        public static readonly Activator Sigmoid = new Activator(SigmoidFunction, SigmoidDerivative);
        public static readonly Activator ReLu = new Activator(ReLuFunction, ReLuDerivative);
        public readonly Func<double, double> Activate;
        public readonly Func<double, double> Derive;
        private static double SigmoidFunction(double x)
            => 1 / (1 + Math.Exp(-x));
        private static double SigmoidDerivative(double y)
            => y * (1 - y);
        private static double ReLuFunction(double x)
            => Math.Max(0, x);
        private static double ReLuDerivative(double y)
            => y >= 0 ? 1 : 0; // Truly undefined at y==0; No need to care
    }
}
