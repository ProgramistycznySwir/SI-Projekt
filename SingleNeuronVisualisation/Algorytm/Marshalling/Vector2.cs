using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MathCatalogue.Random_Extensions;

namespace Algorytm.Marshalling
{
    struct Vector2
    {
        public double X, Y;
        public Vector2(double x, double y)
            => (X, Y) = (x, y);

        // Do szybkiego generowanie punktów.
        public Vector2 Random => new Vector2(CommonUseRNG.NextDouble(), CommonUseRNG.NextDouble());

        /// <summary>
        /// Tworzy listę tablic na potrzeby uczenia sieci neuronowej.
        /// </summary>
        /// <param name="points"> List of datasets ready to be plugged into neural network. </param>
        /// <param name="chunkSize"> Each dataset size. </param>
        public static List<double[]> Aggregate(List<Vector2> points, int chunkSize)
        {
            if (points.Count % chunkSize is not 0)
                throw new IndexOutOfRangeException($"points.Count({points.Count}) has to be multiple of chunkSize({chunkSize})");
            int dataSetCount = points.Count / chunkSize;
            List<double[]> result = new(dataSetCount);
            for (int i = 0; i < dataSetCount; i++)
            {
                result.Add(new double[2*chunkSize]);
                for (int ii = 0; ii < chunkSize; ii++)
                    (result[i][2 * ii], result[i][2 * ii + 1]) = points[chunkSize * i + ii];
            }
            return result;
        }


        public static bool CheckIfSetIsDividedPropperly(List<Vector2> points, double angle)
        {
            int leftHandCount = 0;
            int rightHandCount = 0;
            // For diagonal.
            angle += 0.5;
            // Point is here undersood as Vector2.
            Vector2 diagonal = new(Math.Cos(angle * Math.PI), Math.Sin(angle * Math.PI));

            foreach (var point in points)
                (diagonal.Dot(point) > 0 ? ref leftHandCount : ref rightHandCount) += 1;

            return leftHandCount == rightHandCount;
        }

        public double Dot(Vector2 other)
            => (X * other.X) + (Y * other.Y);

        // Nie zwracajcie na to uwagi.
        private void Deconstruct(out double Item1, out double Item2)
            => (Item1, Item2) = (X, Y);

        
        /// <summary>
        /// To tutaj jest po to by można było łatwo deklarować nowe punkty za pomocą notacji (x, y).
        /// </summary>
        public static implicit operator Vector2((double, double) tuple)
            => new Vector2(tuple.Item1, tuple.Item2);
        
    }
}
