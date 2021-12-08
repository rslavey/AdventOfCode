using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day192020 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }
        internal List<Rule> rules = new List<Rule>();
        internal List<string> possibleMessages = new List<string>();

        public string GetSolution(int part)
        {
            List<string> messages = new List<string>();
            var isMessage = false;
            foreach (var line in Input)
            {
                if (line == string.Empty)
                {
                    isMessage = true;
                    continue;
                }
                if (isMessage)
                {
                    messages.Add(line);
                }
                else
                {
                    var index = int.Parse(line.Split(':')[0]);
                    var r = line.Split(':')[1];
                    Rule rule = new Rule { Id = index, Definition = r.Replace("\"", ""), Values = r.Contains("\"") ? new List<List<object>> { new List<object> { r.Replace("\"", "") } } : new List<List<object>>() };
                    rules.Add(rule);
                }
            }
            //for(var i = 0; i < rules.Count(); i++)
            //{
            //    if (rules[i].Definition.Contains("|"))
            //    {
            //        var ors = rules[i].Definition.Split('|');
            //        for (var ii = 0; ii < ors.Length; ii++)
            //        {
            //            var d = rules[i].Definition.Split('|')[ii].Split(" ".ToArray(), options: System.StringSplitOptions.RemoveEmptyEntries);
            //            rules[i].Values.Add(new List<object>());
            //            rules[i].Values[ii].AddRange(d.ToList());
            //        }
            //        //rules[i].Definition = string.Empty;
            //    }
            //    else
            //    {
            //        var d = rules[i].Definition.Split(" ".ToArray(), options: System.StringSplitOptions.RemoveEmptyEntries);
            //        rules[i].Values.Add(new List<object>(d));
            //    }
            //}
            var regex = new Regex("[0-9]+");
            MatchEvaluator evaluator = new MatchEvaluator(DefReplacer);

            while (rules.Any(x => regex.IsMatch(x.Definition)))
            {

                for (var i = 0; i < rules.Count(); i++)
                {
                    var r = rules[i];
                    rules[i].Definition = regex.Replace(rules[i].Definition, evaluator);
                    //foreach(var m in regex.Matches(r.Definition))
                    //{
                    //    if (int.TryParse(m.ToString(), out int mInt))
                    //    {
                    //        var defReplace = rules.FirstOrDefault(x => x.Id == mInt).Definition;
                    //        rules[i].Definition = rules[i].Definition.Replace($" {m} ", $" {defReplace.Replace("\"","")} ");
                    //    }
                    //}
                }
            }
            possibleMessages.Add($"{rules.FirstOrDefault(x => x.Id == 0).Definition.Replace(" ","").Replace("[a]","a").Replace("[b]","b")}");
            var orReg = new Regex(@"\[[ab]+\|[ab]+\]");
            while (possibleMessages.Any(x => x.Contains("|")))
            {
                var firstMatch = possibleMessages.FirstOrDefault(x => orReg.IsMatch(x));
                var match = orReg.Match(firstMatch);
                var matchValue = (string)match.Value;
                var orVals = matchValue.Split('|');
                possibleMessages.Add(orReg.Replace(firstMatch, orVals[0].Replace("[", ""), 1));
                possibleMessages.Add(orReg.Replace(firstMatch, orVals[1].Replace("]", ""), 1));
                possibleMessages.Remove(firstMatch);// = orReg.Replace(firstMatch, orVals[1].Replace("]", ""), 1);
            }
            var sb = new StringBuilder();
            foreach(var message in possibleMessages)
            {
                sb.AppendLine(message);
            }
            return $"{sb}";
        }
        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

        private string DefReplacer(Match match)
        {
            if (int.TryParse(match.Value.ToString(), out int mInt))
            {
                return $"[{rules.FirstOrDefault(x => x.Id == mInt).Definition}]";
            }
            return match.Value.ToString().Replace("\"","");
        }

        private StringBuilder FindMessages(Rule rule, StringBuilder sb)
        {
            if (rule.Values.SelectMany(x => x).Count() == 1 && 
                (
                    (string)rule.Values[0][0] == "a" || 
                    (string)rule.Values[0][0] == "b"
                )
            )
            {
                return sb.Append((string)rule.Values[0][0]);
            }
            else
            {
                for (var i = 0; i < rule.Values.Count(); i++)
                {
                    var orSb = new StringBuilder();
                    foreach(var val in rule.Values[i])
                    {
                        if (int.TryParse((string)val, out int valInt))
                        {
                            orSb.Append(FindMessages(rules.FirstOrDefault(x => x.Id == valInt), sb));
                        }
                        else
                        {
                            orSb.Append(val);
                        }
                    }
                    rule.Values[i].Add(orSb.ToString());
                }
            }
            return null;
        }

        internal class Rule
        {
            internal int Id { get; set; }
            internal string Definition { get; set; }
            internal List<List<object>> Values { get; set; }
        }
    }
}
