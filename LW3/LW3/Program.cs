using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LW3
{
    internal class Program
    {

        public delegate int CountOddNumbersDelegate(int A, int B);

        static void Main(string[] args)
        {
            //асинхронный вызов метода с применением делегата
            CountOddNumbersDelegate dlCountOdd = CountOddNumbers;
            IAsyncResult arCountOdd = dlCountOdd.BeginInvoke(1, 3000, null, null);
            while (true)
            {
                Console.Write(" . ");
                if (arCountOdd.AsyncWaitHandle.WaitOne(50, false))
                {
                    Console.WriteLine("Можно извлечь результат сейчас");
                    break;
                }
            }
            int result = dlCountOdd.EndInvoke(arCountOdd);           
            //вывод результата
            Console.WriteLine("\nКоличество нечетных элементов в матрице случайных чисел: {0}", result);
        }

        static int CountOddNumbers(int A, int B)
        {
            Console.WriteLine("Генерация матрицы и подсчёт нечётных элементов запущены.");
            int k = 20;
            int pos = 0;
            int neg = 0;
            Random Rand = new Random();
            Console.Write("Введите длину массива: ");
            A = int.Parse(Console.ReadLine());
            Console.Write("Введите ширину массива: ");
            B = int.Parse(Console.ReadLine());
            int[] Mass = new int[k];
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = Rand.Next(1, 9);
                Console.Write(Mass[i] + "   ");
                if (Mass[i] % 2 == 0) pos++;
                else neg++;
            }
            pos = neg = 0;
            int[,] Matr = new int[B, A];
            for (int i = 0; i < B; i++)
            {
                for (int j = 0; j < A; j++)
                {
                    Matr[i, j] = Rand.Next(0, 9);
                    Console.Write(Matr[i, j] + "   ");
                    if (Matr[i, j] % 2 == 0) pos++;
                    else neg++;
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nПодсчёт нечётных элементов матрицы завершён.");
            return neg;
        }      
    }
}