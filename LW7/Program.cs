using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace LW7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Сгенерировать два случайных массива
            int size = new Random().Next(10, 1000);
            int[] array1 = new int[size];
            int[] array2 = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                array1[i] = rnd.Next();
                array2[i] = rnd.Next();
            }

            // Создать пул потоков
            int threadsCount = Environment.ProcessorCount;
            var pool = new SemaphoreSlim(threadsCount, threadsCount);

            // Найти количество элементов, присутствующих в обоих массивах
            int commonCount = 0;
            for (int i = 0; i < size; i++)
            {
                pool.Wait();
                int iCopy = i;
                Task.Run(() => {
                    Thread.Sleep(20); // Задержка алгоритма
                    for (int j = 0; j < size; j++)
                    {
                        if (array1[iCopy] == array2[j])
                        {
                            Interlocked.Increment(ref commonCount);
                            break;
                        }
                    }
                    pool.Release();
                });
            }

            // Ожидать завершения всех задач
            for (int i = 0; i < size; i++)
            {
                pool.Wait();
                pool.Release();
            }

            // Вывести результат
            Console.WriteLine($"Количество элементов, присутствующих в обоих массивах случайных чисел: {commonCount}");
            Console.ReadLine();
        }
    }

}