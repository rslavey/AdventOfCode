using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day172015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }
        public List<int> FormattedInput { get; set; }

        public string GetSolution(int partId)
        {
            var target = 150;
            var c = new Combinations<int>(FormattedInput, 2).ToList();
            for (var i = 3; i <= FormattedInput.Count(); i++)
            {
                c = c.Concat(new Combinations<int>(FormattedInput, i)).ToList();
            }

            var answers = c.Where(x => x.Sum() == target).ToList();
            Result = partId == 1 ?
                c.Count(x => x.Sum() == target) :
                answers.Count(x => x.Count() == answers.Min(xx => xx.Count()));

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            FormattedInput = File.ReadAllLines(file).Select(x => int.Parse(x)).ToList();
        }
    }
}
