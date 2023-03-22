using System;
using System.Linq;
using System.Threading.Tasks;

namespace LW9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов: ");
            int columns = int.Parse(Console.ReadLine());

            Console.WriteLine();

            var matrixTask = Task.Run(() =>
            {
                int[,] matrix = new int[rows, columns];
                Random rnd = new Random();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] = rnd.Next();
                    }
                }
                return matrix;
                Console.WriteLine();
            });
                       
            matrixTask.ContinueWith(task => {
                int[,] matrix = task.Result;
                int sum = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix[i, j] % 2 == 0)
                        {
                            sum += (matrix[i, j] * matrix[i, j] * matrix[i, j]); 
                        }
                    }
                }
                Console.WriteLine($"Сумма кубов чётных элементов: {sum}");
                Console.WriteLine();
            });
            
            matrixTask.ContinueWith(task => {
                int[,] matrix = task.Result;
                int average = (int)matrix.Cast<int>().Average();
                Console.WriteLine($"Среднее арифметическое элементов: {average}");
                Console.WriteLine();
            });
            
            matrixTask.ContinueWith(task => {
                int[,] matrix = task.Result;
                int min = matrix.Cast<int>().Min();
                Console.WriteLine($"Минимальный элемент: {min}");
                Console.WriteLine();
            });

            matrixTask.ContinueWith(task => {
                Console.WriteLine("Матрица:");
                int[,] matrix = task.Result;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            });
            Task.WaitAll(matrixTask);
            Console.ReadLine();
            
        }
    }
}
