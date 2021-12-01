using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day102016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        Dictionary<int, HashSet<int>> Outputs = new Dictionary<int, HashSet<int>>();
        Dictionary<int, HashSet<int>> Bots = new Dictionary<int, HashSet<int>>();
        public string GetSolution(int partId)
        {
            var valReg = new Regex(@"^value (.*) goes to bot (.*)$");
            var giveReg = new Regex(@"^bot (.*) gives low to (.*) (.*) and high to (.*) (.*)$");
            var successBot = -1;
            while (successBot < 0)
            {
                foreach(var line in Input)
                {
                    if (line.Substring(0,5) == "value")
                    {
                        var m = valReg.Match(line);
                        var val = int.Parse(m.Groups[1].Value);
                        var bot = int.Parse(m.Groups[2].Value);
                        if (Bots.ContainsKey(bot)) {
                            Bots[bot].Add(val);
                        }
                        else
                        {
                            Bots.Add(bot,new HashSet<int>{val});
                        }
                    }
                    else
                    {
                        var m = giveReg.Match(line);
                        var sourceBot = int.Parse(m.Groups[1].Value);
                        if (!Bots.ContainsKey(sourceBot))
                        {
                            continue;
                        }
                        else
                        {
                            if (Bots[sourceBot].Count() == 2)
                            {
                                var min = Bots[sourceBot].Min(x => x);
                                var max = Bots[sourceBot].Max(x => x);
                                
                                if (partId == 1)
                                {
                                    if (min == 17 && max == 61)
                                    {
                                        successBot = sourceBot;
                                    }
                                }
                                else
                                {
                                    if (Outputs.ContainsKey(0) && Outputs.ContainsKey(1) && Outputs.ContainsKey(2))
                                    {
                                        successBot = Outputs[0].FirstOrDefault() * Outputs[1].FirstOrDefault() * Outputs[2].FirstOrDefault();
                                    }
                                }
                                var giveLowType = m.Groups[2].Value;
                                var giveLowId = int.Parse(m.Groups[3].Value);
                                var giveHighType = m.Groups[4].Value;
                                var giveHighId = int.Parse(m.Groups[5].Value);
                                if (giveLowType == "bot")
                                {
                                    AddToBot(giveLowId, min);
                                }
                                if (giveHighType == "bot")
                                {
                                    AddToBot(giveHighId, max);
                                }
                                if (giveHighType == "output")
                                {
                                    AddToOutput(giveHighId, max);
                                }
                                if (giveLowType == "output")
                                {
                                    AddToOutput(giveLowId, min);
                                }
                                Bots[sourceBot].Clear();
                            }
                        }
                    }
                }
            }
            return $"{successBot}";
        }

        private void AddToOutput(int outputId, int v)
        {
            if (Outputs.ContainsKey(outputId))
            {
                Outputs[outputId].Add(v);
            }
            else
            {
                Outputs.Add(outputId, new HashSet<int> { v });
            }
        }

        private void AddToBot(int botId, int v)
        {
            if (Bots.ContainsKey(botId))
            {
                Bots[botId].Add(v);
            }
            else
            {
                Bots.Add(botId, new HashSet<int> { v });
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
