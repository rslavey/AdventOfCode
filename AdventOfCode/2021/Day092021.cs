using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day092021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        internal List<Point> Grid = new List<Point>();
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        var lowPoints = 0;
                        foreach (var p in Grid)
                        {
                            if (p.GetNearPoints(Grid).Min(x => x.Value) >= p.Value)
                            {
                                lowPoints += p.Value + 1;
                            }
                        }
                        return $"{lowPoints}";
                    }
                default:
                    {
                        var lowPoints = new List<Point>();
                        foreach (var p in Grid)
                        {
                            if (p.GetNearPoints(Grid).Min(x => x.Value) >= p.Value)
                            {
                                lowPoints.Add(p);
                            }
                        }
                        var basinId = 1;
                        foreach(var lp in lowPoints)
                        {
                            ConnectedPoints(lp, basinId);
                            basinId++;
                        }
                        var basins = Grid.Where(x => x.Basin != 0).GroupBy(x => x.Basin).OrderByDescending(x => x.Count()).Take(3).Select(x => x.Count()).Aggregate((total, next) => total * next);
                        return $"{basins}";
                    }
            }
        }

        private void ConnectedPoints(Point lp, int basinId)
        {
            var np = lp.GetNearPoints(Grid).Where(x => x.Value != 9 && x.Basin == 0).ToList();
            if (np.Count() > 0)
            {
                foreach(var p in np)
                {
                    p.Basin = basinId;
                    ConnectedPoints(p, basinId);
                }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            for (var l = 0; l < Input.Length; l++)
            {
                for (var c = 0; c < Input.Min(x => x.Length); c++)
                {
                    Grid.Add(new Point { X = c, Y = l, Value = int.Parse($"{Input[l][c]}") });
                }
            }

        }
        internal class Point
        {
            internal int X { get; set; }
            internal int Y { get; set; }
            internal int Value { get; set; }
            internal int Basin { get; set; }
            internal List<Point> GetNearPoints(List<Point> grid, bool includeCorners = false)
            {
                var p = grid.Where(x => 
                    (x.X == X && Math.Abs(x.Y - Y) == 1) || 
                    (x.Y == Y && Math.Abs(x.X - X) == 1) ||
                    (includeCorners && 
                        (Math.Abs(x.X - X) == 1) && (Math.Abs(x.Y - Y) == 1)
                    )
                );
                return p.ToList();
            }
    
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
