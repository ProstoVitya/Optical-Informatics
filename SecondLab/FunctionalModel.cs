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
    class FunctionalModel
    {
        private const int LeftBound = -5, RightBound = 5;
        private const int M = 2048;
        private const int N = 200;
        private static readonly double Hx = (RightBound - LeftBound) / (double)N;

        private Complex InputFunction(double x)
        {
            return Complex.Exp(-Math.PI * Complex.ImaginaryOne * x) +
                Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * x);
        }

        private static double GaussBundle(double x)
        {
            return Math.Exp(-Math.Pow(x, 2));
        }

        private static List<double> SegmentSpliterator(double pointFrom, double pointTo,
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

        private static List<double> AddZerosToList(List<double> list, int size)
        {
            int zerosCount = size - list.Count;
            for (int i = 0; i < zerosCount; i+=2)
            {
                list.Add(0d);
                list.Insert(0, 0d);
            }
            return list;
        }

        private static List<double> TransderListSides(List<double> list)
        {
            int center = list.Count / 2;
            var result = new List<double>();
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

        private static List<Complex> TransderListSides(List<Complex> list)
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

        /*public static void Main(string[] args)
        {
            var numbers = SegmentSpliterator(LeftBound, RightBound, N);
            var firstFuction = new List<double>();
            foreach (var number in numbers)
            {
                firstFuction.Add(GaussBundle(number));
            }
            firstFuction = AddZerosToList(firstFuction, M);
            firstFuction = TransderListSides(firstFuction);
        }*/

        private static Complex[] DoubleListToComplexArray(List<double> list)
        {
            var result = new Complex[list.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Complex(list[i], 0d);
            }
            return result;
        }

        public static Complex[] GetFunction()
        {
            var numbers = SegmentSpliterator(LeftBound, RightBound, N);
            var firstFuction = new List<double>();
            foreach (var number in numbers)
            {
                firstFuction.Add(GaussBundle(number));
            }
            firstFuction = AddZerosToList(firstFuction, M);
            firstFuction = TransderListSides(firstFuction);
            var complexArray = DoubleListToComplexArray(firstFuction);
            complexArray = FFT.Fft(complexArray);
            for (int i = 0; i < complexArray.Length; i++)
            {
                complexArray[i] *= Hx;
            }
            var preResult = new List<Complex>(complexArray);
            preResult = TransderListSides(preResult);

            var result = new List<Complex>();
            for (int i = M - N / 2; i < M + N / 2; i++)
            {
                result.Add(preResult[i]);
            }

            return complexArray;
        }
    }
}
