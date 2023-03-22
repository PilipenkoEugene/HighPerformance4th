using System;
using System.Threading.Tasks;

namespace LW4
{
    delegate int NumberComparer(int[] arr1, int[] arr2);


    internal class Program
    {
        static int CompareNumbers(int[] arr1, int[] arr2)
        {
            int count = 0;

            foreach (int num in arr1)
            {
                if (Array.IndexOf(arr2, num) >= 0)
                {
                    count++;
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер массива 1: ");
            int size1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите размер массива 2: ");
            int size2 = Convert.ToInt32(Console.ReadLine());

            int[] arr1 = new int[size1];
            int[] arr2 = new int[size2];
            Random rnd = new Random();

            Console.Write("Массив 1: ");
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = rnd.Next(1, 10000);
                Console.Write("{0} ", arr1[i]);
            }

            Console.Write("\nМассив 2: ");
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = rnd.Next(1, 10000);
                Console.Write("{0} ", arr2[i]);
            }
            Console.WriteLine();

            NumberComparer comparer = new NumberComparer(CompareNumbers);

            Task<int> task = Task.Run(() => comparer(arr1, arr2));

            // Вывод результата

            int result = task.Result;
                
            Console.WriteLine("Количество общих элементов в массивах: {0}", result);

            Console.ReadLine();
        }
    }
}