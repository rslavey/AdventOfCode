using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day012016 : IAdventOfCodeSingleData
    {
        public long Result { get; set; }
        public string InputValue { get; set; }
        List<(string dir, int distance)> FI = new List<(string dir, int distance)>();

        public string GetSolution(int partId)
        {
            var curDir = 'n';
            var xdir = 0;
            var ydir = 0;
            List<(int x, int y)> visits = new List<(int x, int y)>();
            for (var i = 0; i < FI.Count(); i++)
            {
                curDir = NewDir(curDir, FI[i].dir);
                for (var ii = 1; ii <= Math.Abs(FI[i].distance); ii++)
                {
                    xdir += curDir == 'e' ? 1 : curDir == 'w' ? -1 : 0;
                    ydir += curDir == 'n' ? -1 : curDir == 's' ? 1 : 0;
                    if (!visits.Any(x => x.x == xdir && x.y==ydir))
                    {
                        visits.Add((xdir, ydir));
                    }
                    else if (partId == 2)
                    {
                        i = FI.Count();
                        break;
                    }
                }
            }
            Result = partId == 1 ?
                Math.Abs(xdir) + Math.Abs(ydir) :
                Math.Abs(xdir) + Math.Abs(ydir);

            return $"{Result}";
        }

        private char NewDir(char curDir, string dir)
        {
            switch (curDir) {
                case 'e':
                    return dir == "L" ? 'n' : 's';
                case 'w':
                    return dir == "L" ? 's' : 'n';
                case 'n':
                    return dir == "L" ? 'w' : 'e';
                default:
                    return dir == "L" ? 'e' : 'w';
            }
        }

        public void GetInputData(string file)
        {
            var inputs = File.ReadAllText(file).Split(',').Select(x => Regex.Match(x.Trim(), @"^([a-zA-Z]+)([0-9]+)$"));//.Groups[1]);
            FI = inputs.Select(x => (x.Groups[1].Value, int.Parse(x.Groups[2].Value))).ToList();
        }

    }
}
