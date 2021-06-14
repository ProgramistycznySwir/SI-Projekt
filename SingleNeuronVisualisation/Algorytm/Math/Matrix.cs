using System;
using System.Linq;
using static System.Linq.Enumerable;

namespace MathCatalogue
{
    public class Matrix
    {
        private double[,] Raw { get; }
        public int Rows { get; }
        public int Cols { get; }
        public (int, int) Shape => (Rows, Cols);
        public double this[int row, int col]
        {
            get => Raw[row, col];
            set => Raw[row, col] = value;
        }

        public Matrix(double[] raw)
        {
            Rows = raw.Length;
            Cols = 1;
            Raw = new double[Rows, Cols];
            for (var i = 0; i < Rows; ++i)
                Raw[i, 0] = raw[i];
        }
        public Matrix(int rows, int cols, double[,]? raw = null)
        {
            Rows = rows;
            Cols = cols;
            Raw = new double[Rows, Cols];
            if (raw is not null)
                Raw = (raw.Clone() as double[,])!;
        }

        public Matrix Add(double scalar)
        {
            var result = new Matrix(Rows, Cols, Raw);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] += scalar;
            return result;
        }
        public Matrix Add(Matrix other)
        {
            if (Shape != other.Shape)
                throw new ArithmeticException();
            var result = new Matrix(Rows, Cols, Raw);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] += other[r, c];
            return result;
        }
        public Matrix Subtract(double scalar)
        {
            var result = new Matrix(Rows, Cols, Raw);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] -= scalar;
            return result;
        }
        public Matrix Subtract(Matrix other)
        {
            if (Shape != other.Shape)
                throw new ArithmeticException();
            var result = new Matrix(Rows, Cols, Raw);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] -= other[r, c];
            return result;
        }
        public Matrix Product(double scalar)
        {
            var result = new Matrix(Rows, Cols, Raw);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] *= scalar;
            return result;
        }
        public Matrix Product(Matrix other)
        {
            if (Cols != other.Rows) 
                throw new ArithmeticException();
            var result = new Matrix(Rows, other.Cols);
            for (var r = 0; r < result.Rows; ++r)
                for (var c = 0; c < result.Cols; ++c)
                    for (var r2 = 0; r2 < Cols; ++r2)
                        result[r, c] += this[r, r2] * other[r2, c];

            return result;
        }
        public Matrix Hadamard(Matrix other)
        {
            if (Shape != other.Shape) 
                throw new ArithmeticException();
            var result = new Matrix(Rows, Cols);
            for (var r = 0; r < Rows; ++r)
                for (var c = 0; c < Cols; ++c)
                    result[r, c] = this[r, c] * other[r, c];
            return result;
        }

        public Matrix Transpose()
        {
            var result = new Matrix(Cols, Rows);
            for (var r = 0; r < Rows; r++)
                for (var c = 0; c < Cols; c++)
                    result[c, r] = this[r, c];
            return result;
        }
        public Matrix Map(Func<double, double> function)
        {
            var result = new Matrix(Rows, Cols);
            for (var r = 0; r < Rows; r++)
                for (var c = 0; c < Cols; c++)
                    result[r, c] = function(this[r, c]);
            return result;
        }
        public override string ToString()
            => string.Join("\n", Range(0, Rows)
                .Select(r => string.Join("\t", Range(0, Cols).Select(c => this[r, c]))));

        public void Info(string name)
            => Console.WriteLine($"{name} {Shape}\n{this}");

        public double First => Raw[0, 0];
    }
}
