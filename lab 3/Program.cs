using System;
using static System.Console;
using static System.Math;

namespace fffff
{
    class Program
    {
        static void Main(string[] args)
        {
            Write(" M = "); int m = int.Parse(ReadLine());
            Write(" N = "); int n = int.Parse(ReadLine());
            if (n % 2 != 0)
            {
                WriteLine("Ошибка");
                ReadKey();
            }
            else
            {
                Random rnd = new Random();
                int[,] matrix = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        matrix[i, j] = rnd.Next(0, 100);
                    }
                }
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Write($"{matrix[i, j],3}  ");
                    }
                    Write("\n");
                }
                int X = -1;
                int Y = -1;
                int x = n - 1;
                int y = 1;
                int max1 = matrix[x, y - 1];
                Write(matrix[x, y - 1]);
                for (int i = 0; i < m + 2; i++)
                {
                    for (int j = 1; j < n / 2; j++)
                    {
                        if (y >= 0 && y < m) 
                        {
                            if (max1 < matrix[x, y])
                            {
                                max1 = matrix[x, y];
                            }
                            Write(matrix[x, y]);
                        }
                        x += X;
                        y += Y;
                    }
                    if (y >= 0 && y < m)
                    {
                        if (matrix[x, y] > max1)
                        {
                            max1 = matrix[x, y];
                        }
                        Write(matrix[x, y]);
                    }
                    y += 1;
                    X = -X;
                    Y = -Y;
                }
                int max2 = matrix[x / 2, y - 1];
                int N = n / 2 - 1;
                for (int i = 0; i < n / 2; i++)
                {
                    if (i % 2 == 1)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if (matrix[N, j] > max2)
                            {
                                max2 = matrix[N, j];
                            }
                            Write(matrix[N, j]);
                        }
                    }
                    else
                    {
                        for (int j = m - 1; j >= 0; j--)
                        {
                            if (matrix[N, j] > max2)
                            {
                                max2 = matrix[N, j];
                            }
                            Write(matrix[N, j]);
                        }
                    }
                    N--;
                }
                WriteLine();
                WriteLine("max= " + Max(max1, max2));
                ReadKey();
            }
        }
    }
}