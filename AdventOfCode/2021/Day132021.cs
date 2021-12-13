using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static com.randyslavey.AdventOfCode.Day112021;

namespace com.randyslavey.AdventOfCode
{
    class Day132021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        internal Grid G = new Grid();
        internal List<(int line, char dir)> Folds = new List<(int line, char dir)>();
        public string GetSolution(int partId)
        {
            foreach(var f in Folds.Take(partId == 1 ? 1 : Folds.Count()))
            {
                var max = G.GridPoints.Max(x => f.dir == 'y' ? x.Y : x.X);
                var newG = new Grid();
                for (var i = 0; i <= f.line; i++)
                {
                    foreach (var gp in G.GridPoints.Where(x => f.dir == 'y' ? (x.Y == f.line + i || x.Y == f.line - i) : (x.X == f.line + i || x.X == f.line - i)))
                    {
                        newG.GridPoints.Add(new Day092021.Point { X = (f.dir == 'y' ? gp.X : f.line - i), Y = (f.dir == 'y' ? f.line - i : gp.Y) });
                    }
                }
                G = newG;
            }
            return $"{G.GridPoints.Distinct().Count()}{(partId == 1 ? string.Empty : $"\n{G.PrintDotGrid()}")}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            bool firstHalf = true;
            foreach(var line in Input)
            {
                if (line == string.Empty)
                {
                    firstHalf = false;
                    continue;
                }
                if (firstHalf)
                {
                    var p = line.Split(',');
                    G.GridPoints.Add(new Day092021.Point { X = int.Parse(p[0]), Y = int.Parse(p[1]) });
                }
                else
                {
                    var l = line.Replace("fold along ","");
                    var fold = l.Split('=');
                    Folds.Add((int.Parse(fold[1]), fold[0][0]));
                }
            }
        }

    }
}
