// Вычисление интеграла по заданной квадратичной формуле с заданной точностью.
// Формула правых треугольников
using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 10, n = 0;
            double e = 0.0001, I1 = 0, I2 = 0, a = 0, b = 2, h = 0;

            do
            {
                n++;
                N *= 2;
                h = (b - a) / N;
                I1 = I2;
                I2 = 0;
                for(double i = 0; i <=2; i+=h)
                {
                    I2 += h * F(i);
                }
                Console.WriteLine("Step: " + n + "  I = " + I2 + "  N = " + N + " h = " + h);
            } while (Math.Abs(I1 - I2) > e);
            Console.ReadKey();
        }

        static double F(double x)
        {
            return x*Math.Exp(x);
        }
    }
}
