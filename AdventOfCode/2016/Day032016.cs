using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day032016 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        public List<List<int>> FI = new List<List<int>>();


        public string GetSolution(int partId)
        {
            var count = 0;
            if (partId == 1)
            {
                for (var i = 0; i < FI.Count(); i++)
                {
                    FI[i].Sort();
                    count += FI[i][0] + FI[i][1] > FI[i][2] ? 1 : 0;
                }
                Result = count;
            }
            else
            {
                for (var i = 0; i < FI.Count() - 2; i += 3)
                {
                    for (var ii = 0; ii < 3; ii++)
                    {
                        List<int> newSet = new List<int> { FI[i][ii], FI[i + 1][ii], FI[i + 2][ii] };
                        newSet.Sort();
                        count += newSet[0] + newSet[1] > newSet[2] ? 1 : 0;
                    }
                }
                Result = count;
            }
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^[ ]*([0-9]+)[ ]*([0-9]+)[ ]*([0-9]+)$");
            foreach(var line in File.ReadLines(file))
            {
                var m = r.Match(line).Groups;
                List<int> nums = new List<int>{ int.Parse(m[1].Value), int.Parse(m[2].Value), int.Parse(m[3].Value) };
                FI.Add(nums);
            }
            InputValues = File.ReadAllLines(file);
        }

    }
}
