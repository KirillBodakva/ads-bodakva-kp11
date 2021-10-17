using System;
using static System.Console;

namespace АСД2
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = Int32.Parse(Console.ReadLine());
            c = (c * 100 - 100);

            int d = 13;
            int res = 5;
            double res1;
            int n = 0;
            for (int i = 1; i <= 100; i++)
            {
                c += 1;
                int m = 1;
                for (; m <= 12; m++)
                {
                    res1 = 2.6 * m - 0.2;
                    res = (Convert.ToInt16(res1) + d + c % 100 + (c % 100) / 4 + (c / 100) / 4 - 2 * c / 100) % 7;
                    if (res == 5)
                        n++;
                }
            }
            Console.WriteLine(n);
        }
    }
}
