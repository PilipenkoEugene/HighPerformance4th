using System;
using System.Threading;

namespace LW6
{
    internal class Program
    {
        static int[,] matrix;
        static int minValue;
        static int maxValue;

        static void Main()
        {
            Console.Write("Введите размер матрицы: ");
            int size = int.Parse(Console.ReadLine());

            // Создание матрицы случайных чисел
            matrix = new int[size, size];
            Random rand = new Random();
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = rand.Next(1, 100);
                }
            }

            // Создание массива потоков
            Thread[] threads = new Thread[size];

            // Запуск всех потоков
            for (int i = 0; i < threads.Length; i++)
            {
                int index = i; // Необходимо создать локальную переменную index, чтобы ее можно было использовать в замыкании
                object threadArgs = new ThreadArgs();
                threads[i] = new Thread(ObjectThreadMethod);

                // Передача минимального и максимального значения потоку через объект-аргумент
                ((ThreadArgs)threadArgs).Min = int.MaxValue;
                ((ThreadArgs)threadArgs).Max = int.MinValue;

                threads[i].Start(threadArgs);
            }

            // Ожидание завершения всех потоков
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            // Вычисление разницы максимального и минимального значения
            int diff = maxValue - minValue;
            Console.WriteLine($"Максимальное значение: {maxValue}");
            Console.WriteLine($"Минимальное значение: {minValue}");
            Console.WriteLine($"Разница: {diff}");
            Console.ReadKey();
        }

        // Метод, который будет выполняться в потоке
        static void ObjectThreadMethod(object threadArgsObj)
        {
            ThreadArgs threadArgs = (ThreadArgs)threadArgsObj;

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                int value = matrix[threadArgs.Index, col];
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} обрабатывает значение {value}");

                if (value < threadArgs.Min)
                {
                    threadArgs.Min = value;
                }
                if (value > threadArgs.Max)
                {
                    threadArgs.Max = value;
                }
            }

            // Обновление минимального и максимального значения в глобальных переменных
            lock (matrix)
            {
                if (threadArgs.Min < minValue)
                {
                    minValue = threadArgs.Min;
                }
                if (threadArgs.Max > maxValue)
                {
                    maxValue = threadArgs.Max;
                }
            }
        }

        // Класс-аргумент для передачи минимального и максимального значения потоку
        class ThreadArgs
        {
            public int Index { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}
