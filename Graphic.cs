using System;

namespace Lab1_gr
{
    class Program
    {
        const int Width = 100, Height = 50;
        static double XPosition = 0, YPosition = 0, scale = 5;
        static string buffer;
        static double[] F_Values = new double[Width];
        static bool draw1 = true, draw2 = true, switchDraw = false;
        
        static void Draw()
        {
            //Console.Clear();
            for(int i = 0; i < Width; i++)
            {
                F_Values[i] = Func(XPosition+i*(scale/5));
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
                    bool cond1 = IsNear(Func (XPosition + (j - 1) * (scale / 5)), YPosition + (Height - i) * (scale / 5));
                    bool cond2 = IsNear(Func2(XPosition + (j - 1) * (scale / 5)), YPosition + (Height - i) * (scale / 5));
                    bool cond3 = InRange(ToPos(Func (XPosition + (j - 1) * (scale / 5))), ToPos(Func (XPosition + (j + 0) * (scale / 5))), ToPos(YPosition + (Height - i) * (scale / 5)));
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
                if ((YPosition/(scale/5) - i ) % 5 == 0)
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
                        i += (int)Math.Ceiling(Math.Log10(Math.Abs((int)(XPosition + i * (scale / 5))) + ((int)(XPosition + i * (scale / 5)) % 10 == 0 ? 1 : 0))) - 1 + ((int)(XPosition + i * (scale / 5)) < 0 ? 1 : 0) + (XPosition + i*(scale/5) == Math.Round(XPosition +i* (scale / 5)) ?0:1);
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
            return Math.Pow(x, 2);
        }

        static double Func2(double x)
        {
            return Math.Pow(x, 3);
        }

        static bool IsNear(double check, double to)
        {
            return ToPos(to) == ToPos(check);
        }

        static int ToPos(double x)
        {
            return (int)Math.Round(x / (scale / 5) );
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
        static void Main(string[] args)
        {
            XPosition = -(Width / 2) * (scale / 5);
            YPosition = -(Height / 2) * (scale / 5);
            Console.SetWindowSize(Width + 15, Height+5);
            //GraphDrawer drawer = new GraphDrawer();
            //string[] bruh = { "sdfddddddddddddddddsggggrrrrrrrrrefggdbgfgddfbergiehrrbhgye\nsdfddddddddddddddddsggggrrrrrrrrrefggdbgfgddfbergiehrrbhgye\n", "sdfgbvmbmcnmbncmvcmbnmvbncggjrhfgyrgfbfgyweiehrrbhgye\nsdfddddbvmnmvnbmbnvmnvmbnmbmvmvnbmnbmrgiehrrbhgye\n" };
            //drawer.DrawCorner();
            //drawer.DrawMetrics_X();
            //drawer.DrawMetrics_Y();
            //bool i = false;
            Draw();
            do
            {
                //Console.SetCursorPosition(0, 0);
                //Console.Write(bruh[Convert.ToInt32(i)]);
                //i = !i;
                Draw();
                //Console.WriteLine((-10) % 3);
                /*switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow: drawer.X -= 1; drawer.DrawMetrics_X(); break;
                    case ConsoleKey.RightArrow: drawer.X += 1; drawer.DrawMetrics_X(); break;
                    case ConsoleKey.UpArrow: drawer.Y += 1; drawer.DrawMetrics_Y(); break;
                    case ConsoleKey.DownArrow: drawer.Y -= 1; drawer.DrawMetrics_Y(); break;
                }*/
                //} while (true);
                //Console.WriteLine(scale);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow: XPosition -= (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5:1); break;
                    case ConsoleKey.RightArrow: XPosition += (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.UpArrow: YPosition += (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.DownArrow: YPosition -= (scale / 5) * (key.Modifiers == ConsoleModifiers.Control ? 5 : 1); break;
                    case ConsoleKey.Z: {
                            scale /= 2 * (key.Modifiers == ConsoleModifiers.Control ? 2 : 1);
                            XPosition = Math.Round(XPosition / (scale / 5)) * (scale / 5) + (Width/2) * (scale/5);
                            YPosition = Math.Round(YPosition / (scale / 5)) * (scale / 5) + (Height / 2) * (scale / 5);
                            break; }
                    case ConsoleKey.X: {
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
    }
}
