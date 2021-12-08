using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day132016 : IAdventOfCodeData<int>
    {
        public string Result { get; set; }
        public int Input { get; set; }
        private List<List<(int x, int y)>> answers = new List<List<(int x, int y)>>();
        private List<List<(int x, int y)>> answers2 = new List<List<(int x, int y)>>();
        (int x, int y) targetPosition = (31, 39);
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        (int x, int y) pos = (1, 1);
                        List<(int x, int y)> moves = new List<(int x, int y)>();
                        TracePath(pos, moves);
                        var shortestRoute = answers.OrderBy(x => x.Count()).FirstOrDefault();
                        var steps = shortestRoute?.Count() - 1;
                        return $"{steps}";
                    }
                default:
                    {
                        (int x, int y) pos = (1, 1);
                        List<(int x, int y)> moves = new List<(int x, int y)>();
                        TracePath2(pos, moves);
                        HashSet<(int x, int y)> a = new HashSet<(int x, int y)>();
                        foreach(var an in answers2)
                        {
                            foreach(var an2 in an.Take(51))
                            {
                                a.Add(an2);
                            }
                        }
                        var steps = a.Distinct().Count();
                        return $"{steps}";
                    }
            }
        }

        private void TracePath((int x, int y) pos, IEnumerable<(int x, int y)> moves)
        {
            var m = new List<(int x, int y)>(moves)
            {
                pos
            };
            if (pos == targetPosition)
            {
                answers.Add(m.ToList());
            }
            else
            {
                foreach(var curPos in GetPoints(pos).Where(x => IsOpen(x) && !moves.Contains(x)))
                {
                    TracePath(curPos, m);
                }
            }
        }
        private void TracePath2((int x, int y) pos, IEnumerable<(int x, int y)> moves)
        {
            var m = new List<(int x, int y)>(moves)
            {
                pos
            };
            foreach (var curPos in GetPoints(pos).Where(x => IsOpen(x) && !moves.Contains(x) && moves.Count() <= 50))
            {
                TracePath2(curPos, m);
            }
            answers2.Add(m.ToList());
        }

        private bool IsOpen((int x, int y) curPos)
        {
            var s1 = (curPos.x * curPos.x) + (3 * curPos.x) + (2 * curPos.x * curPos.y) + curPos.y + (curPos.y * curPos.y);
            s1 += Input;
            var b = Convert.ToString(s1, 2);
            return b.Count(x => x == '1') % 2 == 0;
        }

        private IEnumerable<(int x, int y)> GetPoints((int x, int y) pos)
        {
            List<(int x, int y)> ps = new List<(int x, int y)>();
            ps.Add((pos.x, pos.y - 1));
            ps.Add((pos.x, pos.y + 1));
            ps.Add((pos.x + 1, pos.y));
            ps.Add((pos.x - 1, pos.y));
            return ps.Where(x => x.x >=0 && x.y >= 0);
        }

        public void GetInputData(string file)
        {
            Input = int.Parse(File.ReadAllLines(file)[0]);
        }

    }
}
