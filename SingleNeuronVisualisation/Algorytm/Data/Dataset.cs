using Algorithm.Marshalling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithm.Data
{
    // Represents single dataset.
    public class Dataset
    {
        public double[] RawData { get; set; }
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
        public int ChunkSize => RawData.Length/2;

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

        public override string ToString()
            => $"{Solution}: {{{string.Join(", ", RawData[..^1])}}}";




        //public List
    }
}
