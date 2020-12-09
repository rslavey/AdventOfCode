using System;

namespace com.randyslavey.AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            RunCode("2015", "17", 2);
            Console.ReadLine();
        }

        private static void RunCode(string yearId, string dayId, int partId = 1)
        {
            var path = $".\\Inputs\\{yearId}\\Day{dayId}Input.txt";
            var nc = (IAdventOfCode)Activator.CreateInstance(Type.GetType($"com.randyslavey.AdventOfCode.Day{dayId}{yearId}"));
            nc.GetInputData(path);
            Console.WriteLine(nc.GetSolution(partId));
        }
    }
}
