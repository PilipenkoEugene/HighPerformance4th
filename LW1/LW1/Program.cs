using System;
using System.Globalization;

namespace LW1
{
    class Program
    {
        delegate void MyFirstDelegate(int pParam);
        delegate int Summ(int a, int v);
        delegate string ConverterFromFloat(float D);
        delegate int[] ToVector(int a, int b, int c);
        delegate string GetAString();


        static void Main(string[] args)
        {

            string spaceNoise = "1o12i12u1y213fguaboba090kllkfo87k";
            string alienSignal = "aboba";


            //Лямбда-выражение
            bool extraterrestrialIntelligence() =>
                spaceNoise.Contains(alienSignal);

            //Делегаты Action<Func<a> b, c> и Лямбда-выражение
            Action<Func<bool>, int, int> resPrint = (resFunc, basis, power) =>
            {
                string res = "";
                if (resFunc())
                {
                    res = "OMG! Alien Signal! Light Years To Target: " + (Math.Pow(basis, power)).ToString();    
                }
                else
                {
                    res = "White Noise. No Signals Of Extraterrestrial Intelligence Found.";
                }

                Console.WriteLine(res);
            };

            resPrint(extraterrestrialIntelligence, 887, 13);
        }
    }
}