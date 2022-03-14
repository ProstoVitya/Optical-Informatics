using System;
using System.Collections.Generic;
using System.Numerics;


namespace FirstLab
{
    public class FunctionModel
    {
        private const int Alpha = 1;
        private const int m = 1000;
        private const int n = 1000;
        private const int a = -10, b = 10;
        private const int p = -10, q = 10;
        private const double h = (b - a) / ((double)n);
        private const double l = (q - p) / ((double)m);

        public readonly List<double> X = new List<double>();
        private readonly List<Complex> _f = new List<Complex>();
        public readonly List<double> Ksi = new List<double>();

        public Complex OutputFunction(double ksi)
        {
            var f = new Complex(0, 0);

            for (int k = 0; k < n; k++)
            {
                f += K(ksi, X[k]) * _f[k] * h;
            }

            return f;
        }

        public Complex InputFunctiuon(double x)
        {
            return Complex.Exp(Complex.ImaginaryOne * x / 10);
        }

        public Complex K(double ksi, double x)
        {
            return Complex.ImaginaryOne * Complex.Exp(-Alpha *
                Complex.Abs(x + ksi * Complex.ImaginaryOne));
        }

        public List<Complex> Result()
        {
            for (double c = a; c < b; c += h)
            {
                X.Add(c);
            }

            foreach (var item in X)
            {
                _f.Add(InputFunctiuon(item));
            }

            for (double c = p; c < q; c += l)
            {
                Ksi.Add(c);
            }

            if (X.Count != _f.Count && _f.Count != Ksi.Count)
                throw new Exception("Incorrect size");

            var result = new List<Complex>();
            foreach (var item in Ksi)
            {
                result.Add(OutputFunction(item));
            }
            return result;
        }

        public List<Complex> KsiResult(double ksi)
        {
            for (double c = a; c < b; c += h)
            {
                X.Add(c);
            }

            foreach (var item in X)
            {
                _f.Add(InputFunctiuon(item));
            }

            for (double c = p; c < q; c += l)
            {
                Ksi.Add(c);
            }

            if (X.Count != _f.Count && _f.Count != Ksi.Count)
                throw new Exception("Incorrect size");

            var result = new List<Complex>();
            for (int k = 0; k < n; k++)
            {
                result.Add(K(ksi, X[k]));
            }
            return result;
        }

        
        public Dictionary<double, Complex> CountInputSignal()
        {
            var result = new Dictionary<double, Complex>();
            for (double c = a; c < b; c += h)
            {
                result.Add(c, InputFunctiuon(c));
                X.Add(c);
            }

            return result;
        }
    }
}
