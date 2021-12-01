using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day012016 : IAdventOfCodeData<string>
    {
        public long Result { get; set; }
        public string Input { get; set; }
        List<(string dir, int distance)> FI = new List<(string dir, int distance)>();

        public string GetSolution(int partId)
        {
            if (partId == 1)
            {

            }
            else
            {

            }

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var inputs = File.ReadAllText(file).Split(',').Select(x => Regex.Match(x.Trim(), @"^([a-zA-Z]+)([0-9]+)$"));//.Groups[1]);
            FI = inputs.Select(x => (x.Groups[1].Value, int.Parse(x.Groups[2].Value))).ToList();
        }

    }
}
