using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    class Program
    {
        static  double F(double x)
        {
            return Math.Sin(x);
            //return x*x;
        }
        static double Lgr(int n, List<double> l, double x)
        {
            double sum = 0;
            for (int i = 0; i < n; i++)
                sum += (l[i] * Math.Pow(x, n - i - 1));
            return sum;
        }
        static void Clone<T>(ref List<T> clone, ref List<T> to)
        {
            to = new List<T>();
            for (int i = 0; i < clone.Count; i++) to.Add(clone[i]);
        }
        static void Create(ref List<double> list, int n)
        {
            for (int i = 0; i < n; i++) list.Add(0);
        }

        const int Width = 100, Height = 50;
        static double XPosition = 0, YPosition = 0, scale = 5;
        static string buffer;
        static double[] F_Values = new double[Width];
        static bool draw1 = true, draw2 = true, switchDraw = false;

        static void Draw()
        {
            //Console.Clear();
            for (int i = 0; i < Width; i++)
            {
                F_Values[i] = Func(XPosition + i * (scale / 5));
            }
            buffer = "";
            buffer += '╔';
            for (int i = 1; i < Width + 2 - 1; i++)
                buffer += '═';
            buffer += '╗';
            buffer += "\n";
            for (int i = 0; i < Height; i++)
            {
                buffer += '║';
                for (int j = 0; j < Width; j++)
                {
                    bool cond1 = IsNear(Func(XPosition + (j - 1) * (scale / 5)), YPosition + (Height - i) * (scale / 5));
                    bool cond2 = IsNear(Func2(XPosition + (j - 1) * (scale / 5)), YPosition + (Height - i) * (scale / 5));
                    bool cond3 = InRange(ToPos(Func(XPosition + (j - 1) * (scale / 5))), ToPos(Func(XPosition + (j + 0) * (scale / 5))), ToPos(YPosition + (Height - i) * (scale / 5)));
                    bool cond4 = InRange(ToPos(Func2(XPosition + (j - 1) * (scale / 5))), ToPos(Func2(XPosition + (j + 0) * (scale / 5))), ToPos(YPosition + (Height - i) * (scale / 5)));
                    //if(cond1) if(draw1 && switchDraw == false) buffer += '█';

                    if (cond1 && draw1 && (switchDraw == false || !(cond2 == true && draw2 == true))) buffer += '█';
                    else if (cond2 && draw2) buffer += '▒';
                    else if (cond3 && draw1 && (switchDraw == false || !(cond4 == true && draw2 == true))) buffer += '█';
                    else if (cond4 && draw2) buffer += '▒';
                    else buffer += ' ';
                }
                buffer += "║";
                //Console.WriteLine((YPosition - i * (scale / 5)) % scale);
                if ((YPosition / (scale / 5) - i) % 5 == 0)
                {
                    buffer += ("- " + (YPosition + Height * (scale / 5) - (double)i * (scale / 5)) + "\n");
                }
                else buffer += "          \n";
            }
            buffer += '╚';
            for (int i = 1; i < Width + 2 - 1; i++)
                buffer += '═';
            buffer += '╝';
            buffer += "\n";
            buffer += ' ';
            for (int i = 0; i < Width; i++)
            {
                if ((XPosition + i * (scale / 5)) % scale == 0)
                    buffer += '|';
                else
                    buffer += ' ';
            }
            buffer += "\n";
            buffer += ' ';
            for (int i = 0; i < Width; i++)
            {
                if ((XPosition + i * (scale / 5)) % scale == 0)
                //if((XPosition/(scale/5) + i) % 5 == 0)
                {
                    buffer += (XPosition + (double)i * (scale / 5));
                    if (XPosition + i * (scale / 5) != 0)
                        i += (int)Math.Ceiling(Math.Log10(Math.Abs((int)(XPosition + i * (scale / 5))) + ((int)(XPosition + i * (scale / 5)) % 10 == 0 ? 1 : 0))) - 1 + ((int)(XPosition + i * (scale / 5)) < 0 ? 1 : 0) + (XPosition + i * (scale / 5) == Math.Round(XPosition + i * (scale / 5)) ? 0 : 1);
                    //Console.Write((int)Math.Ceiling(Math.Log10(XPosition + i)) - 1);
                }
                else
                    buffer += ' ';
            }
            buffer += "    ";
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer);
        }

        static double Func(double x)
        {
            return F(x);
        }

        static double Func2(double x)
        {
            return Lgr(n, l, x);
        }

        static bool IsNear(double check, double to)
        {
            return ToPos(to) == ToPos(check);
        }

        static int ToPos(double x)
        {
            return (int)Math.Round(x / (scale / 5));
        }

        static bool InRange(int a, int b, int who)
        {
            if (a > b)
            {
                int c = a;
                a = b;
                b = c;
            }
            if (who > a && who < b) return true;
            else return false;
        }

        static void Control()
        {
            do
            {
                Draw();
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow: XPosition -= (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.RightArrow: XPosition += (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.UpArrow: YPosition += (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.DownArrow: YPosition -= (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.Z:
                        {
                            scale /= 2 * (key.Modifiers == ConsoleModifiers.Control ? 2 : 1);
                            XPosition = Math.Round(XPosition / (scale / 5)) * (scale / 5) + (Width / 2) * (scale / 5);
                            YPosition = Math.Round(YPosition / (scale / 5)) * (scale / 5) + (Height / 2) * (scale / 5);
                            break;
                        }
                    case ConsoleKey.X:
                        {
                            scale *= 2 * (key.Modifiers == ConsoleModifiers.Control ? 2 : 1);
                            XPosition = Math.Round(XPosition / (scale / 5)) * (scale / 5) - (Width / 4) * (scale / 5);
                            YPosition = Math.Round(YPosition / (scale / 5)) * (scale / 5) - (Height / 4) * (scale / 5);
                            break;
                        }
                    case ConsoleKey.R: { Console.Clear(); Draw(); break; }
                    case ConsoleKey.D1: draw1 = !draw1; break;
                    case ConsoleKey.D2: draw2 = !draw2; break;
                    case ConsoleKey.D3: switchDraw = !switchDraw; break;
                }
            } while (true);
        }
        static int n;
        static List<double> l;
        static void Main(string[] args)
        {
            
            double p2;
            double a, z, h;
            Console.WriteLine("Enter q of points interp: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Borders: ");
            a = double.Parse(Console.ReadLine(), System.Globalization.NumberStyles.Any, null);
            z = double.Parse(Console.ReadLine(), System.Globalization.NumberStyles.Any, null);
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> copy = new List<double>();
            List<double> main = new List<double>();
            l = new List<double>(); Create(ref l, n);
            List<bool> b = new List<bool>();

            h = (z - a) / (n - 1);

            while(a<=z)
            {
                x.Add(a);
                y.Add(F(-a));
                a += h;
            }

            main.Add(1);
            main.Add(x[n - 1]);
            for (int i = 0; i < n; i++) b.Add(false);
            b[n - 1] = true;
            for(int k = 0; k < n; k++)
            {
                b[k] = true;
                p2 = 1;
                for(int i = 0; i < n; i++)
                {
                    if (k != i) p2 *= ((-1) * x[k] + x[i]);
                    if(b[i] != true)
                    {
                        b[i] = true;
                        Clone<double>(ref main, ref copy);
                        main.Add(0);
                        for(int j = 0; j < copy.Count; j++)
                        {
                            copy[j] *= x[i];
                            main[j + 1] += copy[j];
                        }
                    }
                }
                for(int m = 0; m < main.Count; m++)
                {
                    main[m] *= y[k] / p2;
                    l[m] += main[m];
                }
                for (int i = 0; i < b.Count; i++) b[i] = false;
                b[k] = true;
                main = new List<double>();
                main.Add(1);
                main.Add(x[k]);
            }
            Console.WriteLine("Output: ");
            for(int i = 0; i < n; i++)
            {
                if(l[i] != 0)
                {
                    if (i == 0) Console.Write(l[i] + "x^" + (n - 1));
                    else
                    {
                        if (l[i] > 0) Console.Write("+" + l[i] + "x^"+(n-i-1));
                        else Console.Write(l[i]+"x^"+(n-i-1));
                    }
                }
            }
            Console.WriteLine("\nValues output: ");
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("x[{0,2}] = {1,10:f10} y[{0,2}] = " + Lgr(n, l, x[i]), i, x[i]);
            }
            Console.ReadLine();
            Draw();
            Control();

        }
    }
}
