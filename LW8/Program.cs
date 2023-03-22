using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace LW8
{
    class Program
    {
        static void Main(string[] args)
        {
            // создание матрицы
            Console.WriteLine("Сгенерирована матрица:");
            int size = 10;
            double[,] matrix = new double[size, size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rnd.NextDouble();
                    Console.Write("{0} ", Math.Round(matrix[i, j], 4));
                }
            }
            Console.WriteLine();
            // Ожидать завершения всех задач
            Task.WaitAll();
            Console.WriteLine("\nВсе значения в матрице заменены на их косинусы:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Создать задачу для преобразования элемента матрицы
                    int iCopy = i;
                    int jCopy = j;
                    Task.Run(() => {
                        Console.Write("{0} ", Math.Round(Math.Cos(matrix[iCopy, jCopy]), 4));
                    });
                }
            }                             
            
            Console.ReadLine();
        }
    }
}