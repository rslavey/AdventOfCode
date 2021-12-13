using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day102021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        internal List<char> CloseChars = new List<char> { '}', ']', ')', '>' };
        internal Dictionary<char, int> BadCharScore = new Dictionary<char, int>() {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };
        internal Dictionary<char, int> ClosingCharScore = new Dictionary<char, int>() {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };
        internal Dictionary<char, char> ClosingChars = new Dictionary<char, char>()
        {
            {'(',')'},
            {'[',']'},
            {'{','}'},
            {'<','>'}
        };
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        List<char> badChars = new List<char>();
                        for (var i = 0; i < Input.Length; i++)
                        {
                            while (Input[i].Contains("<>") || Input[i].Contains("[]") || Input[i].Contains("()") || Input[i].Contains("{}"))
                            {
                                Input[i] = Input[i].Replace("<>", "").Replace("[]", "").Replace("()", "").Replace("{}", "");
                            }
                            var fo = Array.Find<char>(Input[i].ToCharArray(), IsClosingChar);
                            if (fo != 0)
                            {
                                badChars.Add(fo);
                            }
                        }
                        int badScore = 0;
                        foreach(var b in badChars)
                        {
                            badScore += BadCharScore[b];
                        }
                        return $"{badScore}";
                    }
                default:
                    {
                        for (var i = 0; i < Input.Length; i++)
                        {
                            while (Input[i].Contains("<>") || Input[i].Contains("[]") || Input[i].Contains("()") || Input[i].Contains("{}"))
                            {
                                Input[i] = Input[i].Replace("<>", "").Replace("[]", "").Replace("()", "").Replace("{}", "");
                            }
                            var fo = Array.Find<char>(Input[i].ToCharArray(), IsClosingChar);
                            if (fo != 0)
                            {
                                Input[i] = string.Empty;
                            }
                        }
                        var scores = new List<long>();
                        foreach(var line in Input.Where(x => x != string.Empty))
                        {
                            long score = 0;
                            var close = line.Reverse();
                            foreach(var c in close)
                            {
                                var closingChar = ClosingChars[c];
                                score = score * 5 + ClosingCharScore[closingChar];
                            }
                            scores.Add(score);
                        }
                        
                        var middleScore = (int)Math.Floor(scores.Count() / 2.0);
                        return $"{scores.OrderBy(x => x).Skip(middleScore).FirstOrDefault()}";
                    }
            }

        }

        private bool IsClosingChar(char c)
        {
            return CloseChars.Contains(c);
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
