using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day082021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public List<(List<string> patterns, List<string> output)> Entries = new List<(List<string> patterns, List<string> output)>();
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        var uniqueCount = Entries.Sum(x => x.output.Count(xx => xx.Length == 2 || xx.Length == 4 || xx.Length == 3 || xx.Length == 7));
                        return $"{uniqueCount}";
                    }
                default:
                    {
                        string[] d = new string[10];
                        var total = 0;
                        foreach(var entry in Entries)
                        {
                            d[1] = entry.patterns.FirstOrDefault(x => x.Length == 2);
                            d[4] = entry.patterns.FirstOrDefault(x => x.Length == 4);
                            d[7] = entry.patterns.FirstOrDefault(x => x.Length == 3);
                            d[8] = entry.patterns.FirstOrDefault(x => x.Length == 7);
                            d[3] = entry.patterns.FirstOrDefault(x => x.Length == 5 && x.Except(d[1]).Count() == 3);
                            d[9] = entry.patterns.FirstOrDefault(x => x.Length == 6 && x.OrderBy(xx => xx).SequenceEqual(d[3].Concat(d[4]).Distinct().OrderBy(xx => xx)));
                            d[5] = entry.patterns.FirstOrDefault(x => x.Length == 5 && x != d[3] && !x.Contains(d[8].Except(d[9]).First()));
                            d[2] = entry.patterns.FirstOrDefault(x => x.Length == 5 && x != d[3] && x != d[5]);
                            d[6] = entry.patterns.FirstOrDefault(x => x.Length == 6 && x.OrderBy(xx => xx).SequenceEqual((d[8].Except(d[1]).Concat(d[5])).Distinct().OrderBy(xx => xx)));
                            d[0] = entry.patterns.FirstOrDefault(x => x != d[1] && x != d[2] && x != d[3] && x != d[4] && x != d[5] && x != d[6] && x != d[7] && x != d[8] && x != d[9]);

                            var sb = new StringBuilder();
                            foreach(var output in entry.output)
                            {
                                for (var i = 0; i < d.Length; i++)
                                {
                                    if (output.OrderBy(x => x).SequenceEqual(d[i].OrderBy(x => x)))
                                    {
                                        sb.Append(i);
                                    }
                                }
                            }
                            total += int.Parse(sb.ToString());
                        }
                        return $"{total}";
                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach(var line in Input)
            {
                var l = line.Split('|');
                var entry = (l[0].Split(" ".ToCharArray(), options: StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList(), l[1].Split(" ".ToCharArray(), options: StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList());
                Entries.Add(entry);
            }
        }

    }
}
