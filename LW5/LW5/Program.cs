using System;
using System.Threading;

namespace LW5
{
    internal class Program
    {

        static int[,] matrix;
        static int minValue;

        static void Main()
        {
            // Создание матрицы случайных чисел
            matrix = new int[5, 5];
            Random rand = new Random();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    matrix[row, col] = rand.Next(1, 100);
                }
            }

            // Создание массива потоков
            Thread[] threads = new Thread[5];

            // Запуск всех потоков
            for (int i = 0; i < threads.Length; i++)
            {
                int index = i; // Необходимо создать локальную переменную index, чтобы ее можно было использовать в замыкании
                threads[i] = new Thread(() =>
                {
                    for (int col = 0; col < 5; col++)
                    {
                        int value = matrix[index, col];
                        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} обрабатывает значение {value}");
                        if (value < minValue || minValue == 0)
                        {
                            minValue = value;
                        }
                    }
                });
                threads[i].Start();
            }

            // Ожидание завершения всех потоков
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            // Вывод минимального значения в консоль
            Console.WriteLine($"Минимальное значение: {minValue}");
            Console.ReadKey();
        }
    }
}