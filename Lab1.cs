using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0, xr, xl, xm, eps;
            int n = 0;
            bool found = false;
            xr = Convert.ToDouble(Console.ReadLine());
            xl = Convert.ToDouble(Console.ReadLine());
            eps = Convert.ToDouble(Console.ReadLine());
            do
            {
                n++;
                xm = (xr + xl) / 2;
                if (Math.Abs(f(xm)) <= eps)
                {
                    x = xm;
                    found = true;
                }
                else
                {
                    if (f(xl) * f(xm) < 0)
                    {
                        xr = xm;
                    }
                    else xl = xm;
                }
            } while (!found);

            Console.WriteLine("{0}\n{1}", x, n);
            Console.ReadKey();
        }

        static double f(double x)
        {
            return x*x*x - x*x - 1000 - Math.Cos(x);
        }
    }
}
