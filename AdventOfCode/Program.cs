using System;
using System.IO;

namespace com.randyslavey.AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //MakeTemplate("2016", @"D:\Users\Randy\source\repos\AdventOfCode\AdventOfCode");
            RunCode("2016", "03", 2);
            Console.ReadLine();
        }

        private static void RunCode(string yearId, string dayId, int partId = 1)
        {
            var path = $".\\Inputs\\{yearId}\\Day{dayId}Input.txt";
            var nc = (IAdventOfCode)Activator.CreateInstance(Type.GetType($"com.randyslavey.AdventOfCode.Day{dayId}{yearId}"));
            nc.GetInputData(path);
            Console.WriteLine(nc.GetSolution(partId));
        }

        private static void MakeTemplate(string yearId, string projectPath)
        {
            var inputFileDir = $"{projectPath}\\Inputs\\{yearId}";
            if (!Directory.Exists(inputFileDir))
            {
                Directory.CreateDirectory(inputFileDir);
            }
            for (var i = 1; i <= 25; i++)
            {
                var inputFile = $"{inputFileDir}\\Day{i.ToString().PadLeft(2, '0')}Input.txt";
                if (!File.Exists(inputFile))
                {
                    File.CreateText(inputFile);
                }
            }

            var classFileDir = $"{projectPath}\\{yearId}";
            if (!Directory.Exists(classFileDir))
            {
                Directory.CreateDirectory(classFileDir);
            }
            for (var i = 1; i <= 25; i++)
            {
                var classFile = $"{classFileDir}\\Day{i.ToString().PadLeft(2, '0')}{yearId}.cs";
                if (!File.Exists(classFile))
                {
                    var templateContents = File.ReadAllText($"{projectPath}\\DayTemplate.cs").Replace("DAYTEMPLATE", $"Day{i.ToString().PadLeft(2, '0')}{yearId}");
                    File.WriteAllText(classFile, templateContents);
                }
            }

        }
    }
}
