using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day022016 : IAdventOfCodeUngroupedData
    {
        public string Result { get; set; }
        public string[] InputValues { get; set; }

        List<char[]> FI = new List<char[]>();

        public string GetSolution(int partId)
        {
            var passCode = new List<char>();
            if (partId == 1)
            {
                var code = new Dictionary<(int x, int y), char> {
                    { (0,0), '1' },
                    { (1,0), '2' },
                    { (2,0), '3' },
                    { (0,1), '4' },
                    { (1,1), '5' },
                    { (2,1), '6' },
                    { (0,2), '7' },
                    { (1,2), '8' },
                    { (2,2), '9' },
                };
                foreach (var line in FI)
                {
                    (int x, int y) curNum = (1, 1);
                    foreach (var c in line)
                    {
                        curNum.y += (c == 'U' && curNum.y > 0) ? -1 : (c == 'D' && curNum.y < 2) ? 1 : 0;
                        curNum.x += (c == 'L' && curNum.x > 0) ? -1 : (c == 'R' && curNum.x < 2) ? 1 : 0;
                    }
                    passCode.Add(code[(curNum.x, curNum.y)]);
                }
                Result = new string(passCode.ToArray());
            }
            else
            {
                var code = new Dictionary<(int x, int y), char> {
                    { (2,0), '1' },
                    { (1,1), '2' },
                    { (2,1), '3' },
                    { (3,1), '4' },
                    { (0,2), '5' },
                    { (1,2), '6' },
                    { (2,2), '7' },
                    { (3,2), '8' },
                    { (4,2), '9' },
                    { (1,3), 'A' },
                    { (2,3), 'B' },
                    { (3,3), 'C' },
                    { (2,4), 'D' },
                };
                foreach (var line in FI)
                {
                    (int x, int y) curNum = (2, 2);
                    foreach (var c in line)
                    {
                        var newY = c == 'U' ? curNum.y -1 : c == 'D' ? curNum.y + 1 : curNum.y;
                        var newX = c == 'L' ? curNum.x - 1 : c == 'R' ? curNum.x + 1 : curNum.x;
                        if (code.ContainsKey((newX, newY)))
                        {
                            curNum = (newX, newY);
                        }

                    }
                    passCode.Add(code[(curNum.x, curNum.y)]);
                }
                Result = new string(passCode.ToArray());
            }


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
            FI = File.ReadAllLines(file).Select(x => x.ToCharArray()).ToList();
        }

    }
}
