using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day242015 : IAdventOfCodeUngroupedData
    {
        public long Result { get; set; }
        public string[] InputValues { get; set; }
        public List<long> FormattedInputValues = new List<long>();
        List<List<long>> c = new List<List<long>>();

        public string GetSolution(int partId)
        {
            var totalWeight = InputValues.Sum(x => int.Parse(x));
            var groupWeight = totalWeight / (partId == 1 ? 3 : 4);
            var packageCount = InputValues.Length;
            
            for (var i = 0; i < InputValues.Length; i++)
            {
                c.AddRange(new Combinations<long>(FormattedInputValues, i).Select(x => x.ToList()).Where(x => x.Sum() == groupWeight).ToList());
                if (c.Count() >= (partId == 1 ? 3 : 4))
                {
                    break;
                }
            }

            Result = c.Select(x => x.Aggregate((a, v) => a * v)).OrderBy(x => x).Take(1).FirstOrDefault();

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValues = File.ReadAllLines(file);
            FormattedInputValues = InputValues.Select(x => long.Parse(x)).OrderByDescending(x => x).ToList();
        }
    }
}

