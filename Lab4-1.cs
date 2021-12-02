//Метод релаксации.
using System;


namespace Lab4
{
    class Program
    {
        static int n = 3;
        static void Main(string[] args)
        {
            double[,] A = new double[n, n];
            double[] B = new double[n];
            double[] X = new double[n];
            double[] R = new double[n];
            double q, sum, max;
            double e = 0.00000001;
            int index;
            int N = 0;

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    A[i, j] = double.Parse(Console.ReadLine());
                }
                B[i] = double.Parse(Console.ReadLine());
            }

            double[,] P = new double[n, n];
            double[] C = new double[n];

            for(int i = 0; i < n; i++)
            {
                q = -A[i, i];
                C[i] = -B[i] / q;
                for (int j = 0; j < n; j++) P[i, j] = A[i, j] / q;
            }

            for (int i = 0; i < n; i++) X[i] = 1;
            for (int i = 0; i < n; i++) R[i] = 0;

            do
            {
                N++;
                sum = 0;
                for (int l = 0; l < n; l++)
                {
                    sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (l != j) sum += (P[l, j] * X[j]);
                    }
                    R[l] = C[l] - X[l] + sum;
                }
                max = R[0];
                index = 0;
                for(int i = 0; i < n; i++)
                {
                    if(Math.Abs(R[i]) > max)
                    {
                        max = R[i];
                        index = i;
                    }
                }
                X[index] += max;
            } while (Math.Abs(max) > e);

            Console.WriteLine(N);
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("x" + i + " = " + X[i] + ", ");
            }
        }
    }
}
