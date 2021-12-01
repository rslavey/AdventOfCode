using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day072016 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            if (partId == 1)
            {
                var bracketMatches = new Regex(@"\[(.*?)\]");
                var abbaMatch = new Regex(@"([a-z])([a-z])\2(?!\2)\1");
                var matches = 0;
                foreach(var line in Input)
                {
                    var bm = bracketMatches.Matches(line).Cast<Match>().Select(x => x.Value);
                    if (!bm.Any(x => abbaMatch.IsMatch(x)) && abbaMatch.Matches(line).Count > 0)
                    {
                        matches++;
                    }
                }
                Result = matches;
            }
            else
            {
                var matches = 0;
                foreach (var line in Input)
                {
                    List<(string aba, bool inBrackets)> abas = new List<(string aba, bool inBrackets)>();

                    bool inBrackets = false;
                    for (var i = 0; i < line.Length - 2; i++)
                    {
                        if (line[i] == '[' || line[i] == ']')
                        {
                            inBrackets = line[i] == '[';
                        }
                        else if (line[i + 2] == '[' || line[i + 2] == ']'){
                            inBrackets = line[i + 2] == '[';
                            i = i + 2;
                        }
                        else if (line[i] == line[i + 2] && line[i] != line[i + 1])
                        {
                            abas.Add(((inBrackets ? line.Substring(i, 3) : $"{line.Substring(i + 1,2)}{line.Substring(i + 1,1)}"), inBrackets));
                        }
                    }
                    if (abas.GroupBy(x => x.aba).Where(x => x.Count() > 1).SelectMany(x => x).GroupBy(x => x.inBrackets).SelectMany(x => x).Select(x => x.inBrackets).Distinct().Count() > 1)
                    {
                        matches++;
                    }
                }

                Result = matches;
            }
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
