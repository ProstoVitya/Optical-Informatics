using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThirdLab
{
    public class Fourier
    {
        public const int N = 398;
        public const int M = 4096;
        private readonly double Hx = R / (A - 1);
        public const double R = 5;
        public const int A = 400;
        private FunctionModel _model;

        public Fourier(FunctionModel model)
        {
            _model = model;
        }

        public Complex[,] FftValues()
        {
            var functionArray = _model.RestoreImage(_model.GetGaussLaggerList);
            var firstFunction = new List<List<Complex>>();
            for (int i = 0; i < functionArray.GetLength(0); i++)
            {
                firstFunction.Add(new List<Complex>());
                for (int j = 0; j < functionArray.GetLength(0); j++)
                    firstFunction[i].Add(functionArray[i, j]);
            }

            for (int i = 0; i < N; i++)
            {
                var column = new List<Complex>();
                for (int j = 0; j < N; j++)
                {
                    column.Add(firstFunction[j][i]);
                }
                column = AddZerosToList(column, M);
                column = TransderListSides(column);
                var complexArray = Fft(column.ToArray());
                for (int j = 0; j < complexArray.Length; j++)
                {
                    complexArray[j] *= Hx;
                }
                var preResult = new List<Complex>(complexArray);
                preResult = TransderListSides(preResult);

                var result = new List<Complex>();
                for (int j = (M - N) / 2; j < (M + N) / 2; j++)
                {
                    result.Add(preResult[j]);
                }
                for (int j = 0; j < result.Count; j++)
                {
                    firstFunction[j][i] = result[j];
                }
            }
            //lines
            for (int i = 0; i < N; i++)
            {
                var column = new List<Complex>();
                for (int j = 0; j < N; j++)
                {
                    column.Add(firstFunction[i][j]);
                }
                column = AddZerosToList(column, M);
                column = TransderListSides(column);
                var complexArray = Fft(column.ToArray());
                for (int j = 0; j < complexArray.Length; j++)
                {
                    complexArray[j] *= Hx;
                }
                var preResult = new List<Complex>(complexArray);
                preResult = TransderListSides(preResult);

                var result = new List<Complex>();
                for (int j = (M - N) / 2; j < (M + N) / 2; j++)
                {
                    result.Add(preResult[j]);
                }
                firstFunction[i] = result;
            }

            for (int i = 0; i < firstFunction[0].Count; i++)
                for (int j = 0; j < firstFunction[0].Count; j++)
                    functionArray[i,j] = firstFunction[i][j];
            return functionArray;
        }

        public List<Complex> AddZerosToList(List<Complex> list, int size)
        {
            int zerosCount = size - list.Count;
            for (int i = 0; i < zerosCount; i += 2)
            {
                list.Add(0d);
                list.Insert(0, 0d);
            }
            return list;
        }

        public List<Complex> TransderListSides(List<Complex> list)
        {
            int center = list.Count / 2;
            var result = new List<Complex>();
            for (int i = center; i < list.Count; i++)
            {
                result.Add(list[i]);
            }
            for (int i = 0; i < center; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }

        public static Complex[] Fft(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = Fft(x_even);
                Complex[] X_odd = Fft(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + W(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - W(i, N) * X_odd[i];
                }
            }
            return X;
        }

        private static Complex W(int k, int N)
        {
            if (k % N == 0) return 1;
            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }
    }
}
