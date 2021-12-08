using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day152016 : IAdventOfCodeData<List<(int posCount, int startPos)>>
    {
        public string Result { get; set; }
        public List<(int posCount, int startPos)> Input { get; set; }

        public string GetSolution(int partId)
        {
            if (partId == 2)
            {
                Input.Add((11, 0));
            }
            var t = 0;
            int ds;
            do
            {
                ds = 0;
                for (var i = 0; i < Input.Count(); i++)
                {
                    ds += (Input[i].startPos + t + i + 1) % Input[i].posCount;
                    if (ds > 0)
                    {
                        break;
                    }
                }
                t++;
            } while (ds != 0);
            return $"{t - 1}";
        }

        public void GetInputData(string file)
        {
            Input = new List<(int posCount, int startPos)>();
            var r = new Regex(@"Disc #[0-9]* has ([0-9]*) positions; at time=0, it is at position ([0-9]*)\.");
            foreach(var line in File.ReadAllLines(file))
            {
                var g = r.Match(line).Groups;
                Input.Add((int.Parse(g[1].Value), int.Parse(g[2].Value)));
            }
        }

    }
}
