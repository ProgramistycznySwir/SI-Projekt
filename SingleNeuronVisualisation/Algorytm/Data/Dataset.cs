using Algorithm.Marshalling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using static MathCatalogue.Random_Extensions;

namespace Algorithm.Data
{
    // Represents single dataset.
    public struct Dataset
    {
        public double[] RawData { get; set; }
        public double[] PointsData => RawData[..^1];
        public Point this[int index]
        {
            get => new Point(RawData[2*index], RawData[2*index + 1]);
            set => (RawData[2*index], RawData[2*index + 1]) = value;
        }
        public double Solution 
        {
            get => RawData[^1];
            set => RawData[^1] = value;
        } 
        public int Size => RawData.Length/2;

        public Dataset(double[] rawPoints, double solution)
        {
            RawData = new double[rawPoints.Length + 1];
            rawPoints.CopyTo(RawData, 0);
            Solution = solution;
        }
        public Dataset(IList<Point> points, double solution)
        {
            RawData = new double[2 * points.Count + 1];
            for (int i = 0; i < points.Count; i++)
            {
                RawData[2*i]     = points[i].X;
                RawData[2*i + 1] = points[i].Y;
            }
                // To rozwiązanie jest znacznie bardziej eleganckie, ale mniej optymalne.
                //(RawPoints[2 * i], RawPoints[2 * i + 1]) = points[i];
            Solution = solution;
        }
        public Dataset(double solution, params Point[] points)
            : this(points, solution) { }
        public Dataset(double[] array)
            : this(array[..^1], array[^1]) { }

        public double CalculateError(double prediction)
            => Math.Abs(prediction - Solution);

        public override string ToString()
            => $"{Solution}: {{{string.Join(", ", PointsData)}}}";



        /// <summary>
        /// Checks if given angle divides this set propperly.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public bool CheckIfSetIsDividedPropperlyBy(double angle)
        {
            int leftHandCount = 0;
            int rightHandCount = 0;
            // For diagonal.
            angle += 0.5;
            // Point is here undersood as Vector2.
            Point diagonal = new(Math.Cos(angle * Math.PI), Math.Sin(angle * Math.PI));

            for (int i = 0; i < Size; i++)
                (diagonal.Dot(this[i]) > 0 ? ref leftHandCount : ref rightHandCount) += 1;

            return leftHandCount == rightHandCount;
        }

        // I know it's dirty code, but it has to be enough.
        /// <summary>
        /// 
        /// </summary>
        /// <returns> True if solution was found. </returns>
        public bool CalculateSolution()
        {
            for(int i = 64; i > 0; i--)
            {
                double angle = CommonUseRNG.NextDouble();
                if(CheckIfSetIsDividedPropperlyBy(angle))
                {
                    Solution = angle;
                    return true;
                }
            }
            return false;
        }

        public static Dataset CreateRandom(int dataSetSize)
        {
            Dataset result = new();
            result.RawData = new double[2*dataSetSize + 1];
            for(int i = 0;i < dataSetSize*2; i++)
                result.RawData[i] = CommonUseRNG.NextDouble()*2-1;
            if(result.CalculateSolution())
                return result;
            return CreateRandom(dataSetSize);
        }

        //public List
    }
}
