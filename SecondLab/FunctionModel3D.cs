using System;
using System.Collections.Generic;
using System.Numerics;

namespace SecondLab
{
    class FunctionModel3D : FunctionModel
    {
        public List<List<Complex>> FunctionValues2(bool isGauss)
        {
            var xList = XList();
            var result = new List<List<Complex>>();
            for (int i = 0; i < xList.Count; i++)
            {
                result.Add(new List<Complex>());
                for (int j = 0; j < xList.Count; j++)
                {
                    result[i].Add(isGauss ?
                         Complex.Exp(-Math.Pow(xList[i], 2) - Math.Pow(xList[j], 2)) :
                        (Complex.Exp(-Math.PI * Complex.ImaginaryOne * xList[i]) +
                         Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * xList[i])) *
                        (Complex.Exp(-Math.PI * Complex.ImaginaryOne * xList[j]) +
                         Complex.Exp(3 * Math.PI * Complex.ImaginaryOne * xList[j])));
                }
            }
            return result;
        }

        public List<List<Complex>> FftValues2(bool isGauss)
        {
            var firstFuction = FunctionValues2(isGauss);
            //columns
            for (int i = 0; i < N; i++)
            {
                var column = new List<Complex>();
                for (int j = 0; j < N; j++)
                {
                    column.Add(firstFuction[j][i]);
                }
                column = AddZerosToList(column, M);
                column = TransderListSides(column);
                var complexArray = FFT.Fft(column.ToArray());
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
                firstFuction[i] = result;
            }
            //lines
            for (int i = 0; i < N; i++)
            {
                var column = new List<Complex>();
                for (int j = 0; j < N; j++)
                {
                    column.Add(firstFuction[i][j]);
                }
                column = AddZerosToList(column, M);
                column = TransderListSides(column);
                var complexArray = FFT.Fft(column.ToArray());
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
                firstFuction[i] = result;
            }
            return firstFuction;
        }
    }
}
