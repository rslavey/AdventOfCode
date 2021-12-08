using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day202016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public List<(long start, long end)> BlockedIPs = new List<(long start, long end)>();
        public long lowestIp = long.MaxValue;
        public string GetSolution(int partId)
        {
            long lowNum = 0;
            long openCount = 0;
            while (lowNum <= 4294967295)
            {
                var block = BlockedIPs.Where(x => x.start <= lowNum && x.end >= lowNum);
                if (block.Count() > 0)
                {
                    lowNum = block.Max(x => x.end) + 1;
                }
                else
                {
                    if (partId == 1) { 
                        return $"{lowNum}"; 
                    }
                    else
                    {
                        openCount++;
                        lowNum++;
                    }
                }
            }
            return $"{openCount}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach (var line in Input)
            {
                var start = Int64.Parse(line.Split('-')[0]);
                var end = Int64.Parse(line.Split('-')[1]);
                BlockedIPs.Add((start, end));
            }
        }

    }
}
