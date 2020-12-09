using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day172015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        public List<int> FormattedInputValues { get; set; }

        public string GetSolution(int partId)
        {
            var target = 150;
            var c = new Combinations<int>(FormattedInputValues, 2).ToList();
            for (var i = 3; i <= FormattedInputValues.Count(); i++)
            {
                c = c.Concat(new Combinations<int>(FormattedInputValues, i)).ToList();
            }

            var answers = c.Where(x => x.Sum() == target).ToList();
            Result = partId == 1 ?
                c.Count(x => x.Sum() == target) :
                answers.Count(x => x.Count() == answers.Min(xx => xx.Count()));

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            FormattedInputValues = File.ReadAllLines(file).Select(x => int.Parse(x)).ToList();
        }
    }
}
