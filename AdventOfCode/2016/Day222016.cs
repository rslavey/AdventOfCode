using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day222016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public List<Node> Nodes = new List<Node>();
        public List<(Node a, Node b)> ViablePairs = new List<(Node a, Node b)>();
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        for (var i = 0; i < Nodes.Count(); i++)
                        {
                            ViablePairs.AddRange(Nodes.Where(x => Nodes[i] != x && Nodes[i].Used != 0 && Nodes[i].Used <= x.Avail).Select(x => (Nodes[i], x)));
                        }
                        return $"{ViablePairs.Count()}";
                    }
                default:
                    {
                        for (var i = 0; i < Nodes.Count(); i++)
                        {
                            ViablePairs.AddRange(Nodes.Where(x => Nodes[i] != x && Nodes[i].Used != 0 && Nodes[i].Used <= x.Avail).Select(x => (Nodes[i], x)));
                        }
                        var MovablePairs = ViablePairs.Where(x =>
                        (
                            Math.Abs(x.a.Location.X - x.b.Location.X) == 1 && x.a.Location.Y == x.b.Location.Y
                        ) ||
                        (
                            Math.Abs(x.a.Location.Y - x.b.Location.Y) == 1 && x.a.Location.X == x.b.Location.X
                        )
                        );
                        //recurse movable pairs, moving all each time until target reach. Take smallest path.
                        return $"{Result}";
                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            for (var i = 2; i < Input.Length; i++)
            {
                var line = Input[i].Split(" ".ToCharArray(), options: StringSplitOptions.RemoveEmptyEntries);
                var loc = new Regex(@"^/dev/grid/node-x(\d*)-y(\d*)$").Match(line[0]);
                Nodes.Add(new Node
                {
                    Location = new Point { X = int.Parse(loc.Groups[1].Value), Y = int.Parse(loc.Groups[2].Value) },
                    Size = int.Parse(line[1].Replace("T", "")),
                    Used = int.Parse(line[2].Replace("T", "")),
                    Avail = int.Parse(line[3].Replace("T", "")),
                    UsePercent = int.Parse(line[4].Replace("%", "")),
                });
            }
        }

        internal class Node
        {
            internal Point Location { get; set; }
            internal int Size { get; set; }
            internal int Used { get; set; }
            internal int Avail { get; set; }
            internal int UsePercent { get; set; }
        }

        internal class Point
        {
            internal int X { get; set; }
            internal int Y { get; set; }
        }
    }
}
