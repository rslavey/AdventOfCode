using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day052021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public List<Line> Lines = new List<Line>();
        public string GetSolution(int partId)
        {
            return $"{Lines.Where(x => (partId == 1 && (x.P1.X == x.P2.X || x.P1.Y == x.P2.Y)) || partId == 2).SelectMany(x => x.GetPoints()).GroupBy(x => x).Count(x => x.Count() >= 2)}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach (var line in File.ReadLines(file))
            {
                var p = line.Replace(" -> ", ",").Split(',').Select(x => int.Parse(x)).ToArray();
                Lines.Add(new Line { P1 = new Point { X = p[0], Y = p[1] }, P2 = new Point { X = p[2], Y = p[3] } });
            }
        }

        internal class Line
        {
            internal Point P1 { get; set; }
            internal Point P2 { get; set; }

            internal List<Point> GetPoints()
            {
                List<Point> points = new List<Point>();

                var xdir = P1.X < P2.X ? 1 : -1;
                var ydir = P1.Y < P2.Y ? 1 : -1;

                for (var i = 0; i <= TotalPoints() - 1; i++)
                {
                    var px = P1.X - P2.X == 0 ? P1.X : xdir * i + P1.X;
                    var py = P1.Y - P2.Y == 0 ? P1.Y : ydir * i + P1.Y;

                    points.Add(new Point { X = px, Y = py });
                }

                return points;
            }

            internal int TotalPoints()
            {
                return (P1.X != P2.X ? Math.Abs(P1.X - P2.X) + 1 : Math.Abs(P1.Y - P2.Y) + 1);
            }
        }

        internal class Point
        {
            internal int X { get; set; }
            internal int Y { get; set; }

            public override bool Equals(object obj)
            {
                var otherPoint = obj as Point;
                if (otherPoint == null) return false;
                return (X == otherPoint.X && Y == otherPoint.Y);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
        }
    }
}
