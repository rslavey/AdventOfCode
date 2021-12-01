﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day032020 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int part)
        {
            Result = part == 1 ?
                GetModulo(3,1) :
                (new List<(int xmove, int ymove)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) }).Select(x => GetModulo(x.xmove, x.ymove)).Aggregate(1, (a, v) => a * v);

            return $"{Result}";
        }

        private int GetModulo(int xmove, int ymove)
        {
            return Input.Select((item, index) => new { item, index }).Count(x => '#' == x.item.ToCharArray()[(x.index / ymove) * xmove % x.item.Length] && x.index % ymove == 0 && x.index > 0);
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
