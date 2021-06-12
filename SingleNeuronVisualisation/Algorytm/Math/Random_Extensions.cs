using System;
using System.Linq;
using static System.Linq.Enumerable;

namespace MathCatalogue
{
    public static class Random_Extensions
    {
        public static Random CommonUseRNG = new Random();

        public static double[] NextDoubles(this Random random, int n)
            => Range(0, n).Select(_ => random.NextDouble()).ToArray();

        public static Matrix NextMatrix(this Random random, int n, int m)
        {
            var result = new Matrix(n, m);
            for (var r = 0; r < n; ++r)
                for (var c = 0; c < m; ++c)
                    result[r, c] = random.NextDouble();
            return result;
        }
    }
}
