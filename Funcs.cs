using System;

namespace Functions
{
    class Program
    {
        class Value
        {
            public double abs;
            public string name;
            public bool sign;

            static public Value operator +(Value x, Value y)
            {
                if (x.name == y.name)
                {
                    Value z = new Value();
                    z.abs = x.abs * Math.Pow(-1, Convert.ToInt32(x.sign)) + y.abs * Math.Pow(-1, Convert.ToInt32(y.sign));
                    z.name = x.name;
                    z.sign = z.abs < 0 ? true : false;
                    z.abs = Math.Abs(z.abs);

                    return z;
                }
                else return x;
            }
        }

        

        static void Main(string[] args)
        {
            Value x = new Value();
            x.name = "";
            x.abs = 12;
            x.sign = false;

            Value y = new Value();
            y.name = "";
            y.abs = 61;
            y.sign = true;


            Value z = x + y;

            Console.WriteLine(z.sign == true?"-":"" + z.abs + z.name);
        }
    }
}
