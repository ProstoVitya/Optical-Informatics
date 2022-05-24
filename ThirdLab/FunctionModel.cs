using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Accord.Math;

namespace ThirdLab
{
    public class FunctionModel
    {
        public const double M = -3;
        public const double N = 3;
        public const double P = -2;
        public const double R = 5;
        public const int A = 200;
        public const int B = 2048;
        private readonly List<double> _xList;
        private readonly double Hr = R/(A - 1);

        public FunctionModel()
        {
            _xList = new List<double>();
            for (double i = 0; i < R; i += R / A)
                _xList.Add(i);
        }

        public List<double> XList { get => _xList; }

        public List<Complex> GetGaussLaggerList()
        {
            var ans = new List<Complex>();
            foreach (var x in _xList)
                ans.Add(GaussLagger(x));

            return ans;
        }

        public Complex GaussLagger(double r)
        {
            var lagger = LaggerPolynom(r);
            return Math.Exp(-Math.Pow(r, 2)) * Math.Pow(r, Math.Abs(P)) * lagger;
        }

        private double LaggerPolynom(double r)
        {
            double sum = 0;
            for (int j = 0; j < N; j++)
                sum += Math.Pow(-1, j) * C(N + Math.Abs(P), N - j) * Math.Pow(r, 2 * j) / Factorial(j);
 
            return sum;
        }

        private double C(double n, double k)
        {
            return Factorial(n) / Factorial(k) / Factorial(n - k);
        }

        private double Factorial(double n)
        {
            double r = 1;
            for (int i = 2; i <= n; i++)
                r *= i;
            return r;
        }

        public Complex[,] RestoreImage(Func<List<Complex>> func)
        {
            var matrix = new Complex[2 * A - 1, 2 * A - 1];
            var funcValues = func();
            var n = funcValues.Count - 1;
            for (int i = 0; i < funcValues.Count; i++)
            {
                var element = funcValues[n - i];
                matrix[i, n] = element;
                matrix[2 * n - i , n] = element;
                matrix[n, i] = element;
                matrix[n, 2 * n - i] = element;
            }

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    var alpha = Math.Round(Math.Sqrt(Math.Pow(j - n, 2) + Math.Pow(k - n, 2)));
                    if (alpha > n)
                        matrix[j, k] = 0;
                    else
                    {
                        var element = Complex.Exp(Complex.ImaginaryOne * M * Math.Atan2(k - n, j - n));
                        matrix[j, k] = GaussLagger(alpha * Hr) * element;
                    }
                }
            }
            return matrix;
        }

        public List<Complex> Hankel()
        {
            var resultList = new List<Complex>();
            var gaussList = GetGaussLaggerList();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Complex a;
            for (int i = 0; i < A; i++)
            {
                a = 0;
                for (int j = 0; j < A; j++)
                    a += Bessel.I(Math.Abs((int)M), 2 * Math.PI * _xList[j] * _xList[i]) * gaussList[j] * _xList[j] * Hr
                        * (2 * Math.PI / Complex.Pow(Complex.ImaginaryOne, Math.Abs(M)));
                resultList.Add(a);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            return resultList;
        }
    }
}
