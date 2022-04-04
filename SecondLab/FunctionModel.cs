using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{

    //exp(-pi*i*x)+exp(3*pi*i*x)
    //финитное преобразование
    class FunctionModel
    {
        private const int LeftBound = -5, RightBound = 5;
        private const int M = 2048;
        private const int N = 200;
        private static readonly double Hx = (RightBound - LeftBound) / (double)N;
        private static readonly double B2 = (N * N) / (4 * RightBound * M);
        private static readonly double B1 = -B2;

        public Complex InputFunction(double x)
        {
            return Complex.Exp(-Math.PI * Complex.ImaginaryOne * x) +
                Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * x);
        }

        public Complex GaussBundle(double x)
        {
            return Math.Exp(-Math.Pow(x, 2));
        }

        private List<double> SegmentSpliterator(double pointFrom, double pointTo,
            int segmentsCount)
        {
            var pointsList = new List<double>();
            var step = (pointTo - pointFrom) / (segmentsCount - 1);
            double value = pointFrom;
            for (int i = 0; i < segmentsCount; i++)
            {
                pointsList.Add(value);
                value += step;
            }
            return pointsList;
        }

        private List<Complex> AddZerosToList(List<Complex> list, int size)
        {
            int zerosCount = size - list.Count;
            for (int i = 0; i < zerosCount; i+=2)
            {
                list.Add(0d);
                list.Insert(0, 0d);
            }
            return list;
        }

        private List<Complex> TransderListSides(List<Complex> list)
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

        public Complex[] GetFunction(bool isGauss)
        {
            var numbers = SegmentSpliterator(LeftBound, RightBound, N);
            var firstFuction = new List<Complex>();
            foreach (var number in numbers)
            {
                firstFuction.Add(isGauss? GaussBundle(number) : InputFunction(number));
            }
            firstFuction = AddZerosToList(firstFuction, M);
            firstFuction = TransderListSides(firstFuction);
            var complexArray = FFT.Fft(firstFuction.ToArray());

            for (int i = 0; i < complexArray.Length; i++)
            {
                complexArray[i] *= Hx;
            }
            var preResult = new List<Complex>(complexArray);
            preResult = TransderListSides(preResult);

            var result = new List<Complex>();
            for (int i = (M - N) / 2; i < (M + N) / 2; i++)
            {
                result.Add(preResult[i]);
            }

            return result.ToArray();
        }

        public List<Complex> GetGaussValues()
        {
            var result = new List<Complex>();
            var step = (RightBound - LeftBound) / ((double) N);
            for (double i = LeftBound; i < RightBound; i += step)
            {
                result.Add(GaussBundle(i));
            }
            return result;
        }
    }
}
