using System;
using System.Collections.Generic;

namespace Lab16
{
    class Program
    {
        static double F(double x, double y)
        {
            return x + y;
        }
        static void Main(string[] args)
        {
            int n = 100;
            double[] y = new double[n];
            double r1, r2, x0;
            double a = 0, b = 1, h = (b - a) / n;
            x0 = a;
            y[0] = F(x0, x0);
            Console.WriteLine(y[0]);
            for (int i = 0; i < n-1; i++) {
                x0 = a + i*h;
                r1 = h * F(x0, y[i]);
                r2 = h * F(x0 + h, y[i] + r1);
                y[i + 1] = y[i] + (r1 + r2) / 2;
                Console.WriteLine(y[i+1]);
            }
            Console.ReadLine();
        }
    }
}
