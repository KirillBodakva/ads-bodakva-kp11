using System;
using static System.Console;

namespace АСД
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("X=");
            double x = double.Parse(Console.ReadLine());
            Write("Y=");
            double y = double.Parse(Console.ReadLine());
            double a = Math.Pow(Math.Abs(Math.Sin(x) - Math.Cos(y)), 1 / 3.0);
            WriteLine("A=" + a);
            double b = Math.Cos(Math.Sin(Math.Pow(a, 2.0)));
            WriteLine("B=" + b);
        }
    }
}
