using System;
using System.Drawing;
using static System.Console;

namespace Lab_asd_5
{
    internal class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            while (true)
            {
                int z;
                WriteLine("1 - start the program | 2 - control example | 3 - exit");
                switch (z)
                {
                    //case 1: (); break;
                    //case 2: (); break;
                    case 3: Environment.Exit(1); break;
                    default: WriteLine("\nYou chose incorrect option\n"); break;
                }
            }
        }
        static void generate()
        {
            Write("\nEnter M: "); int m = int.Parse(ReadLine());
            Write("Enter N: "); int n = int.Parse(ReadLine());
            if (m <= 1 || n <= 1)
            {
                WriteLine("\nYou enter incorrect data\n");
                return;
            }
            int [,] matrix = new int[m, n];
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    matrix[i, j] = rnd.Next(0, 19);
                }
            }
            //operations(m, n, matrix);
        }
        static void operations(int m, int n, int[,] matrix)
        {
            int[] line = new int[((m + 1) / 2) * (n / 2)];
            int count = 0, max = -1; // вопросик
            WriteLine("\nOriginal matrix:");
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if((double)i % 2 == 0)
                    {
                        BackgroundColor = ConsoleColor.Green;
                        line[count] = matrix[i, j];
                        max = Math.Max(max, line[count]);
                        count++;
                    }
                    Write($"{matrix[i,j],4}");
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine();
            }
            WriteLine("\nCount:");

            int[] up = new int[max + 1];
            for(int i = 0; i < line.Length; i++)
            {
                up[line[i]]++;
            }

            for (int i = 0; i < up.Length; i++)
            {
                if (i != 0)
                {
                    up[i] += up[i - 1];
                }
                Write($"{up[i]}; ");
            }
            WriteLine("\n\nSorted:");

            int [] sorted = new int[line.Length];
            for()
        }
    }
}
