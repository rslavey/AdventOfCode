using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day072021 : IAdventOfCodeData<int[]>
    {
        public string Result { get; set; }
        public int[] Input { get; set; }

        public string GetSolution(int partId)
        {

            (int pos, int dist) minDistance = (-1, int.MaxValue);
            for (var i = Input.Min(); i <= Input.Max(); i++)
            {
                var dist = Input.Select(x => GetGas(Math.Abs(x - i), partId)).Sum();
                minDistance = dist < minDistance.dist ? (i, dist) : minDistance;
            }
            return $"{minDistance.dist}";
        }

        private int GetGas(int v, int partId)
        {
            switch (partId)
            {
                case 1:
                    return v;
                default:
                    return v * (v + 1) / 2;
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0].Split(',').Select(x => int.Parse(x)).ToArray();
        }

    }
}
