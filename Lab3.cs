// Решение СЛАУ обратным ходом метода Гаусса.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            string line;
            StreamReader input;

            try {
                input = new StreamReader("input.txt");
            } catch {
                return;
            }

            line = input.ReadLine();
            n = Convert.ToInt32(line.Split(' ')[0]);
            m = Convert.ToInt32(line.Split(' ')[1]);
            double[,] matrix = new double[n, m];
            double[] values = new double[n];
            double[] x = new double[n];
            for(int i = 0; i < n; i++)
            {
                line = input.ReadLine();
                for(int j = 0; j < m; j++)
                {
                    matrix[i, j] = Convert.ToDouble(line.Split(' ')[j]);
                }
                Console.WriteLine(line.Split(' ')[m]);
                //values[i] = Convert.ToDouble(line.Split(' ')[m]);
                values[i] = Double.Parse(line.Split(' ')[m], System.Globalization.NumberStyles.Any, null);
            }

            Console.WriteLine("Исходная система:");
            OutputEquation(ref matrix, ref values, n, m);

            double r, d;

            for (int k = 0; k < n; k++)
            {
                if (matrix[n - k - 1,n - k - 1] != 0)
                {
                    for (int i = 1; i < n - k; i++)
                    {
                        d = (matrix[n - k - 1 - i, n - k - 1] / matrix[n - k - 1, n - k - 1]);
                        for (int j = 0; j < n - k; j++)
                        {
                            matrix[n - k - 1 - i,j] -= matrix[n - k - 1,j] * d;
                        }
                        values[n-k-i-1] -= values[n - k - 1] * d;
                    }
                }
                else
                {
                    for (int i = 1; i < n - k; i++)
                    {
                        if (matrix[n - k - 1 - i,n - k - 1] != 0)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                r = matrix[n - k - 1, j];
                                matrix[n - k - 1, j] = matrix[n - k - 1 - i, j];
                                matrix[n - k - 1 - i, j] = r;
                            }
                            k--;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("Обработанная система:");
            OutputEquation(ref matrix, ref values, n, m);

            for(int i = 0; i < n; i++)
            {
                r = 0;
                for(int j = 0; j < i; j++)
                {
                    r += x[j] * matrix[i, j];
                }
                x[i] = (values[i] - r)/matrix[i,i];
            }

            Console.WriteLine("Корни:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("x"+i+" = "+x[i]);
            }

            Console.ReadKey();
        }

        public static void OutputMatrix(ref double[,] matrix, ref double[] values, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write("{0,-5}", matrix[i, j]);
                }
                Console.WriteLine(values[i]);
            }
        }

        public static void OutputEquation(ref double[,] matrix, ref double[] values, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] == 0) continue;
                    string value="", sign = "";
                    if(j != 0)
                    {
                        if (matrix[i, j - 1] != 0)
                        {
                            if (matrix[i, j] >= 0) sign = "+";
                            else sign = "-";
                        }
                        else if (sign != "")
                        {
                            if (matrix[i, j] >= 0) sign = "+";
                            else sign = "-";
                        }
                    }
                    if(matrix[i, j] != 1) value = Convert.ToString(Math.Abs(matrix[i, j]))+"*";
                    Console.Write("{2} {0}x{1} ", value, j, sign);
                }
                Console.WriteLine("= "+values[i]);
            }
        }
    }
}
