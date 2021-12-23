using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 3;
            double[,] A = { { 1,2,-1 },
                            { 1,6,0 },
                            { 0,8,-2 } };
            double[] B =  { 2,
                            9,
                            6 };
            double[] X = { 0, 0, 0 };
            double[] Xp = { 0, 0, 0 };
            //double[,] A = { { 1,1 },
            //                { 1,2 } };
            //double[] B =  { 1,
            //                1 };
            //double[] X = { 0, 0 };
            //double[] Xp = { 0, 0 };
            double[] V = new double[N];
            double e = 0.001;
            double a = 1, max;
            double[] AV = new double[N];
            double[] dF = new double[N];

            double[,] q = new double[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    q[i, j] = A[j, i];
            B = MMultV(q, B);

            double[,] p = new double[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    for (int l = 0; l < N; l++)
                        p[i, j] += q[i, l] * A[l, j];

            //V = Sub(V, B);
            //for (int i = 0; i < N; i++) dF[i] = V[i];
            ////dF = V;
            //dF = NMult(dF, 2);
            int k = 1;
            do
            {
                Xp = X;
                V = Sub(MMultV(A, X), B);
                AV = MMultV(A, V);
                a = (SMult(AV, V) / SMult(AV, AV));
                X = Sub(X, NMult(V, a));
                //AV = MMultV(A, V);
                //a = 0.5 * SMult(AV, V) / SMult(AV, AV);
                //dF = NMult(dF, a);
                //X = Sub(Xp, dF);
                //V = Sub(MMultV(A,X), B);
                //for (int i = 0; i < N; i++) Xp[i] = X[i];

                //Xp = X;
                //V = Sub(MMultV(A, X), B);
                //AV = MMultV(A, V);
                //a = (SMult(AV, V) / SMult(AV, AV)) / 2;
                //X = Sub(X, NMult(NMult(V, a), 2));

                //Xp = X;
                //if (k == 1) V = Sub(MMultV(A, X), B);
                //else V = Sub(V, NMult(AV, a));
                //AV = MMultV(A, V);
                //dF = NMult(V, 2);
                //a = (SMult(AV, V) / SMult(AV, AV)) / 2;
                //X = Sub(X, NMult(dF, a));
                double[] r = Sub(X, Xp);
                max = r[0];
                for (int i = 0; i < N; i++)
                    if (r[i] > max) max = r[i];
                k++;
            } while (k != 40);
            //for (int i = 0; i < N; i++) Console.WriteLine(V[i]);
            for (int i = 0; i < N; i++) Console.WriteLine(X[i]);
            Console.WriteLine(k);
            Console.ReadLine();
            MinNev(p, B, e);
            Console.ReadLine();
        }

        static double[] MMultV (double[,] a, double[] b)
        {
            double[] result = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
                for (int j = 0; j < b.Length; j++)
                    result[i] += a[i, j] * b[j];
            return result;
        }

        static double SMult(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < b.Length; i++)
                result += a[i] * b[i];
            return result;
        }

        static double[] NMult(double[] a, double b)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] * b;
            return result;
        }

        static double[] VMult(double[] b, double a)
        {
            double[] result = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
                result[i] = b[i] * a;
            return result;
        }

        static double[] Sub(double[] a, double[] b)
        {
            double[] result = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
                result[i] = a[i] - b[i];
            return result;
        }

        static void MinNev(double[,] a, double[] b, double e)
        {
            int n = b.Length;
            double[] x0 = new double[n];
            double[] x1 = new double[n];
            double[] r = new double[n];
            double[] deltaF = new double[n];
            double a0;
            double max;
            int k = 0;
            r = Sub(r, b);

            for (int i = 0; i < n; i++)
                deltaF[i] = r[i];

            deltaF = VMult(deltaF, 2);
            do
            {
                max = 0;
                double[] Ar = new double[n];
                Ar = MMultV(a, r);
                a0 = 0.5 * SMult(Ar, r) / SMult(Ar, Ar);
                deltaF = VMult(deltaF, a0);
                x1 = Sub(x0, deltaF);
                r = Sub(MMultV(a, x1), b);
                Ar = Sub(x1, x0);
                for (int i = 0; i < n; i++)
                    if (Math.Abs(Ar[i]) > max)
                        max = Math.Abs(Ar[i]);
                deltaF = VMult(r, 2);
                for (int i = 0; i < n; i++)
                    x0[i] = x1[i];
                k++;
            } while (max > e);
            Console.WriteLine("Корни: ");
            string text = "X= ";
            for (int i = 0; i < n; i++)
                text += Convert.ToString(x1[i] + " ");
            Console.WriteLine(text);
        }
    }
}
