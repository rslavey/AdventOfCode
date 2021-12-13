using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static com.randyslavey.AdventOfCode.Day092021;

namespace com.randyslavey.AdventOfCode
{
    class Day112021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        internal Grid G = new Grid();
        public int Flash { get; set; }
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            FlashGrid();
                        }
                        return $"{Flash}";
                    }
                default:
                    {
                        var i = 0;
                        while (G.GridPoints.GroupBy(x => x.Value).Max(x => x.Count()) != G.GridPoints.Count())
                        {
                            FlashGrid();
                            i++;
                        }
                        return $"{i}";
                    }
            }
        }
        internal void FlashGrid()
        {
            foreach (var p in G.GridPoints)
            {
                p.Value++;
            }
            while (G.GridPoints.Any(x => x.Value == 10))
            {
                foreach (var p in G.GridPoints.Where(x => x.Value == 10))
                {
                    p.Value = 0;
                    Flash++;
                    foreach (var np in p.GetNearPoints(G.GridPoints, true).Where(x => x.Value != 10 && x.Value != 0))
                    {
                        np.Value++;
                    }
                }
            }
        }
        public class Grid
        {
            public List<Point> GridPoints = new List<Point>();

            public void Print()
            {
                for (var i = 0; i <= GridPoints.Max(x => x.Y); i++)
                {
                    for (var ii = 0; ii <= GridPoints.Max(x => x.X); ii++)
                    {
                        Console.Write(GridPoints.FirstOrDefault(x => x.X == ii && x.Y == i).Value);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public string PrintDotGrid()
            {
                var sb = new StringBuilder();
                for (var i = 0; i <= GridPoints.Max(x => x.Y); i++)
                {
                    for (var ii = 0; ii <= GridPoints.Max(x => x.X); ii++)
                    {
                        sb.Append(GridPoints.FirstOrDefault(x => x.X == ii && x.Y == i) == default ? "." : "#");
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
        }
        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            for (var l = 0; l < Input.Length; l++)
            {
                for (var c = 0; c < Input.Min(x => x.Length); c++)
                {
                    G.GridPoints.Add(new Point { X = c, Y = l, Value = int.Parse($"{Input[l][c]}") });
                }
            }
        }

    }
}
