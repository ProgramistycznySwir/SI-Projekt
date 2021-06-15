using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MathCatalogue.Random_Extensions;

namespace Algorithm.Marshalling
{
    public struct Vector2
    {
        public double X, Y;
        public Vector2(double x, double y)
            => (X, Y) = (x, y);

        /// <summary> Generates random point in range of -1 to 1. </summary>
        public Vector2 Random => new Vector2(CommonUseRNG.NextDouble()*2-1, CommonUseRNG.NextDouble()*2-1);

        public double Lenght => Math.Sqrt(X*X + Y*Y);
        public Vector2 Normalized => this / Lenght;

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



        public double Dot(Vector2 other)
            => (X * other.X) + (Y * other.Y);

        // Nie zwracajcie na to uwagi.
        public void Deconstruct(out double Item1, out double Item2)
            => (Item1, Item2) = (X, Y);

        
        /// <summary>
        /// To tutaj jest po to by można było łatwo deklarować nowe punkty za pomocą notacji (x, y).
        /// </summary>
        public static implicit operator Vector2((double, double) tuple)
            => new Vector2(tuple.Item1, tuple.Item2);
        //public static implicit operator (double, double)(Point point)
        //    => (point.X, point.Y);

        public static Vector2 operator *(Vector2 point, double scalar)
            => new Vector2(point.X * scalar, point.Y * scalar);
        public static Vector2 operator /(Vector2 point, double scalar)
            => new Vector2(point.X / scalar, point.Y / scalar);
    }
}
