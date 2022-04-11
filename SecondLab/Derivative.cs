using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{
    public class Derivative
    {
        public static Complex LeftRectangle(Func<double, Complex> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum = new Complex(0d, 0d);
            for (var i = 0; i < n; i++)
            {
                var x = a + i * h;
                sum += f(x);
            }

            var result = h * sum;
            return result;
        }

        static double RightRectangle(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum = 0d;
            for (var i = 1; i <= n; i++)
            {
                var x = a + i * h;
                sum += f(x);
            }

            var result = h * sum;
            return result;
        }

        static double CentralRectangle(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum = (f(a) + f(b)) / 2;
            for (var i = 1; i < n; i++)
            {
                var x = a + h * i;
                sum += f(x);
            }

            var result = h * sum;
            return result;
        }

        /*static void Main(string[] args)
        {
            //локальная функция
            double f(double x) => x / (x - 1);

            var result = LeftRectangle(f, 4, 10, 1000);
            Console.WriteLine("Формула левых прямоугольников: {0}", result);
            result = RightRectangle(f, 4, 10, 1000);
            Console.WriteLine("Формула правых прямоугольников: {0}", result);
            result = CentralRectangle(f, 4, 10, 1000);
            Console.WriteLine("Формула средних прямоугольников: {0}", result);

            Console.ReadKey();
        }*/
    }
}
