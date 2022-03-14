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

        private Complex InputFunction(double x)
        {
            return Complex.Exp(-Math.PI * Complex.ImaginaryOne * x) +
                Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * x);
        }

        private double GaussBundle(double x)
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
            int zerosCount = size = list.Count;
            for (int i = 0; i < zerosCount; i+=2)
            {
                list.Add(new Complex(0d, 0d));
                list.Insert(0, new Complex(0d, 0d));
            }
            return list;
        }

        private List<Complex> TransderListSides(List<Complex> list)
        {
            int center = list.Count / 2;
            List<Complex> result = new List<Complex>();
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
    }
}
