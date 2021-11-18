using System;

namespace Lab1_gr
{
    class Program
    {
        const int Width = 100, Height = 50;
        static double XPosition = 0, YPosition = 0, scale = 10;
        static void Draw()
        {
            Console.Clear();
            for(int i = 0; i < Width+2; i++)
                Console.Write('-');
            Console.WriteLine();
            for (int i = 0; i < Height; i++)
            {
                Console.Write('|');
                for (int j = 0; j < Width; j++)
                    Console.Write(' ');
                Console.Write("|");
                if ((YPosition - i * (scale / 5)) % scale == 0)
                    Console.Write("- " + (YPosition + Height*(scale/5) - (double)i * (scale / 5)) + "\n");
                else Console.WriteLine();
            }
            for (int i = 0; i < Width + 2; i++)
                Console.Write('-');
            Console.WriteLine();
            Console.Write(' ');
            for (int i = 0; i < Width; i++)
            {
                if ((XPosition+i * (scale / 5)) % scale == 0)
                    Console.Write('|');
                else
                    Console.Write(' ');
            }
            Console.WriteLine();
            Console.Write(' ');
            for(int i = 0; i < Width; i++)
            {
                if ((XPosition+i*(scale/5)) % scale == 0)
                {
                    Console.Write(XPosition+(double)i * (scale / 5));
                    if(XPosition+i * (scale / 5) != 0)
                        i += (int)Math.Ceiling(Math.Log10(Math.Abs((int)(XPosition + i * (scale / 5))) + ((int)(XPosition+i * (scale / 5)) % 10 == 0 ? 1 : 0))) - 1 + ((int)(XPosition + i * (scale / 5)) < 0?1:0);
                    //Console.Write((int)Math.Ceiling(Math.Log10(XPosition + i)) - 1);
                }
                else
                    Console.Write(' ');
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(Width+15, Height);
            do
            {
                Draw();
                //Console.WriteLine((-10) % 10);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow: XPosition -= scale/5; break;
                    case ConsoleKey.RightArrow: XPosition += scale/5; break;
                }
            } while (true);
        }
    }
}
