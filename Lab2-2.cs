// Вычисление интеграла по заданной квадратичной формуле с заданной точностью.
// Формула Гаусса
using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 4, n = 0;
            double e = 0.001, I1 = 0, I2 = 0, a = 0, b = 2, h = 0, s;
            double[] q = new double[4] { 0.3478548451, 0.6521451549, 0.6521451549, 0.3478548451 };
            double[] t = new double[4] { -0.8611363116, -0.3399810436, 0.3399810436, 0.8611363116 };

            do
            {
                n++;
                I1 = I2;
                I2 = 0;
                h = (b - a) / N;
                for(int i = 0; i < N; i++)
                {
                    s = 0;
                    for(int j = 0; j < N; j++)
                    {
                        s += q[j] * F( (a+i*h+a+(i+1)*h)/2+t[j]*h/2 );
                    }
                    I2 += s * (h / 2);
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
