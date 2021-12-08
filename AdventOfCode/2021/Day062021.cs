using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day062021 : IAdventOfCodeData<string>
    {
        public string Result { get; set; }
        public string Input { get; set; }
        List<int> Fish = new List<int>();
        public string GetSolution(int partId)
        {
            long[] fishDays = new long[9];
            foreach(int fish in Fish)
            {
                fishDays[fish]++;
            }

            for (var i = 0; i < (partId == 1 ? 80 : 256); i++)
            {
                var newFish = MoveFish(fishDays);
                fishDays = newFish;
            }
            return $"{fishDays.Sum()}";
        }

        private long[] MoveFish(long[] fishDays)
        {
            var temp = fishDays[0];
            var newFishDays = new long[fishDays.Length];
            Array.Copy(fishDays, 1, newFishDays, 0, fishDays.Length - 1);
            newFishDays[6] += temp;
            newFishDays[8] += temp;
            return newFishDays;
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0];
            Fish.AddRange(Input.Split(',').Select(x => int.Parse(x)));
        }

    }
}
