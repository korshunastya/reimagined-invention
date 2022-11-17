using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class GMatrix<T>
        where T : struct
    {
        protected T[,] MatrixT;
        public int Row { get; private set; }
        public int Column { get; private set; }
        public T this[int row, int column]
        {
            get => MatrixT[row, column];
            set => MatrixT[row, column] = value;
        }
        public GMatrix(int row, int column)
        {
            if (typeof(T) != typeof(int) && typeof(T) != typeof(double) && typeof(T) != typeof(float))
            {
                throw new ArgumentException($"<T> can be only int, double, float for GMatrix<T>");
            }

            Row = row;
            Column = column;
            MatrixT = new T[Row, Column];
        }
        public static GMatrix<T> operator +(GMatrix<T> x, GMatrix<T> y)
        {
            GMatrix<T> result = new GMatrix<T>(x.Row, y.Column);
            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < y.Column; j++)
                {
                    result[i, j] = (dynamic)x[i, j] + y[i, j];
                }
            }
            return result;
        }
        public static GMatrix<T> operator *(GMatrix<T> x, GMatrix<T> y)
        {
            GMatrix<T> result = new GMatrix<T>(x.Row, y.Column);
            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < y.Column; j++)
                {
                    for (int k = 0; k < y.Row; k++)
                    {
                        result[i, j] += (dynamic)x[i, k] * y[k, j];
                    }
                }
            }
            return result;
        }
    }
}
