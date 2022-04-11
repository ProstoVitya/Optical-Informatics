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
        public const double A1 = -5, A2 = 5;
        public const int N = 200;
        public const int M = 2048;
        public static readonly double B2 = (N * N) / (4 * A2 * M);
        public static readonly double B1 = -B2;
        public static readonly double Hx = (A2 - A1) / N;
        public static readonly double Hu = (B2 - B1) / N;

        public const double K = (M - N) / 2;

        public Complex GaussFunction(double x)
        {
            return Math.Exp(-Math.Pow(x, 2));
        }

        public List<double> XList()
        {
            var xList = new List<double>();
            double point = A1;
            for (double i = 0; i < N; i++)
            {
                xList.Add(point);
                point += Hx;
            }
            return xList;
        }

        public List<double> UList()
        {
            var uList = new List<double>();
            double point = B1;
            for (double i = 0; i < N; i++)
            {
                uList.Add(point);
                point += Hu;
            }
            return uList;
        }

        public List<Complex> FuctionValues(bool isGauss)
        {
            var xList = XList();
            var result = new List<Complex>();
            foreach (var item in xList)
            {
                result.Add(isGauss ? Math.Exp(-Math.Pow(item, 2)) :
                    Complex.Exp(-Math.PI * Complex.ImaginaryOne * item) +
                  Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * item));
            }
            return result;
        }

        public List<Complex> FftValues(bool isGauss)
        {
            var firstFuction = FuctionValues(isGauss);
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

            return result;
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

        public List<Complex> AnaliticFurier(bool isGauss)
        {
            var matrix = new Complex[N,N];
            var fList = FuctionValues(isGauss);
            var xList = XList();
            for (int i_u = 0; i_u < N; i_u++)
            {
                for (int i_x = 0; i_x < N; i_x++)
                {
                    matrix[i_x, i_u] = fList[i_x] * Complex.Exp(-2 * Math.PI *
                        Complex.ImaginaryOne * xList[i_x]  * (B1 + i_u * Hu));
                }
            }
            var result = new List<Complex>();
            for (int i = 0; i < N; i++)
            {
                var temp = new Complex(0, 0);
                for (int j = 0; j < N; j++)
                {
                    temp += matrix[i, j] * fList[j];
                }
                result.Add(temp * Hx);
            }
            return result;
        }

        #region old code
        /*  public Complex InputFunction(double x)
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
              var numbers = SegmentSpliterator(A1, A2, N);
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
              var step = (A2 - A1) / ((double) N);
              for (double i = A1; i < A2; i += step)
              {
                  result.Add(GaussBundle(i));
              }
              return result;
          }

          public List<Complex> GetDerivative()
          {
              var result = new List<Complex>();
              var step = (A2 - A1) / ((double)N);
              for (double i = A1; i < A2; i += step)
                  result.Add(Derivative.LeftRectangle(GaussBundle, i, A2, N));
              return result;
          }*/
        #endregion old code
    }
}
