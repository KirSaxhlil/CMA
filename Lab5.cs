using System;

namespace Lab5
{
    class Program
    {

        static void TEST()
        {
            double[,] A = { { 2,4,2 },
                            { 1,4,1 },
                            { 2,7,3 } };
            Output(Multiply(A, Transparent(A)));
            Output(A);
        }
        static void Main(string[] args)
        {
            int N = 3;
            int I, J;
            int k = 0;
            double[,] A = { { 5,1,2 },
                            { 1,4,1 },
                            { 2,1,3 } };
            //double[,] A = new double[N, N];
            //for (int i = 0; i < N; i++)
            //    for (int j = 0; j < N; j++) A[i, j] = 1;
            double[,] H = new double[N, N];
            double max;
            double e = 0.001;
            double fi;

            double[] L = new double[N];

            do
            {
                max = A[0, 1];
                I = 0; J = 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = i + 1; j < N; j++)
                    {
                        if (Math.Abs(A[i, j]) > Math.Abs(max)) { max = A[i, j]; I = i; J = j; }
                    }
                }
                //Console.WriteLine(I);
                if (max <= e)
                {
                    for (int i = 0; i < N; i++) L[i] = A[i, i];
                    break;
                }
                else
                {
                    fi = Math.Atan((2 * A[I, J]) / (A[I, I] - A[J, J])) / 2;
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
                    //Output(H);
                    //Output(Multiply(Transparent(H), A));
                    A = Multiply(Multiply(Transparent(H), A), H);
                    k++;
                }
            } while (true);

            Console.WriteLine("K = " + k);
            for (int i = 0; i < N; i++) Console.Write("L["+i+"] = "+L[i] + "\t");
            Console.ReadLine();
        }
        static double[,] Multiply(double[,] A, double[,] B)
        {
            int N = A.Rank + 1;
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
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }

        static double[,] Transparent(double[,] A)
        {
            int N = A.Rank + 1;
            double t;
            double[,] B = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    B[i, j] = A[i, j];
                }
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    t = B[i, j];
                    B[i, j] = B[j, i];
                    B[j, i] = t;
                }
            }

            return B;
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
