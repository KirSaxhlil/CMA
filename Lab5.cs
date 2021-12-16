using System;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 3;
            int I, J;
            int k = 1;
            double[,] A = { { 5,1,2 },
                            { 1,4,1 },
                            { 2,1,3 } };
            //double[,] A = new double[N, N];
            //for (int i = 0; i < N; i++)
            //    for (int j = 0; j < N; j++) A[i, j] = 1;
            double[,] H = new double[N, N];
            double max;
            double e = 0.0001;
            double fi;

            double[] L = new double[N];

            do
            {
                max = A[0, 1];
                I = 0; J = 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (Math.Abs(A[i, j]) > Math.Abs(max)) max = A[i, j]; I = i; J = j;
                    }
                }
                Console.WriteLine(max);
                if (max <= e)
                {
                    for (int i = 0; i < N; i++) L[i] = A[i, i];
                    break;
                }
                else
                {
                    fi = Math.Atan( (2 * A[I, J]) / (A[I, I] - A[J, J]) )/2;
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (i == I && j == J) H[i, j] = -Math.Sin(fi);
                            else if (i == J && j == I) H[i, j] = Math.Sin(fi);
                            else if (i == j && (i == I || i == J)) H[i, j] = Math.Cos(fi);
                            else if (i == j) H[i, j] = 1;
                            else H[i, j] = 0;
                        }
                    }
                    //Output(Multiply(Transparent(H), A));
                    A = Multiply(Multiply(Transparent(H), A), H);
                    k++;
                }
            } while (k!=2);

            
            for (int i = 0; i < N; i++) Console.Write(L[i] + "\t");
        }
        static double[,] Multiply(double[,] A, double[,] B)
        {
            int N = A.Rank+1;
            double[,] C = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    C[i, j] = 0;
                }
            }

            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    for(int k = 0; k < N; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }

        static double[,] Transparent(double[,] A)
        {
            int N = A.Rank+1;
            double t;
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    t = A[i, j];
                    A[i, j] = A[j, i];
                    A[j, i] = t;
                }
            }

            return A;
        }

        static void Output(double[,] A)
        {
            int N = A.Rank + 1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(A[i, j] + " ");
                    if (j == N - 1) Console.Write("\n");
                }
            }
        }
    }
}
