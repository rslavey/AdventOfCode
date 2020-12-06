using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day072015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public List<(string op, string wire)> FormattedInputs = new List<(string op, string wire)>();
        public string[] InputValues { get; set; }

        private static readonly Dictionary<string, ushort> cache = new Dictionary<string, ushort>();

        public string GetSolution(int partId)
        {
            ProcessWires();
            if (partId == 2)
            {
                ushort tempa = cache["a"];
                cache.Clear();
                cache["b"] = tempa;
                ProcessWires();
            }
            return $"{cache["a"]}";
        }

        private void ProcessWires()
        {
            var rushort = new Regex("^(.*)$");
            var rAnd = new Regex("^(.*) AND (.*)$");
            var rOr = new Regex("^(.*) OR (.*)$");
            var rLeft = new Regex("^(.*) LSHIFT (.*)$");
            var rRight = new Regex("^(.*) RSHIFT (.*)$");
            var rNot = new Regex("^NOT (.*)$");
            var rIsNum = new Regex(@"\d");
            var wires = FormattedInputs.Select(x => x.wire).Distinct().OrderBy(x => x);
            var wireCount = FormattedInputs.Select(x => x.wire).Distinct().Count();

            while (cache.Count() < wireCount)
            {
                var cacheWires = wires.Except(cache.Keys.Distinct().OrderBy(x => x));
                foreach (var (op, wire) in FormattedInputs)
                {
                    if (rushort.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rushort.Match(op);
                        ushort u;
                        if (ushort.TryParse(m.Value, out u))
                        {
                            cache[wire] = u;
                        }
                        else if (cache.ContainsKey(m.Value))
                        {
                            cache[wire] = cache[m.Value];
                        }
                    }

                    if (rAnd.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rAnd.Match(op);
                        var l = m.Groups[1].Value;
                        var r = m.Groups[2].Value;
                        if (
                            (Helpers.IsNumeric(l) || cache.ContainsKey(l)) &&
                            (Helpers.IsNumeric(r) || cache.ContainsKey(r))
                        )
                        {
                            var lu = Helpers.IsNumeric(l) ? ushort.Parse(l) : cache[l];
                            var ru = Helpers.IsNumeric(r) ? ushort.Parse(r) : cache[r];
                            cache[wire] = (ushort)(lu & ru);
                        }
                    }

                    if (rOr.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rOr.Match(op);
                        var l = m.Groups[1].Value;
                        var r = m.Groups[2].Value;
                        if (
                            (Helpers.IsNumeric(l) || cache.ContainsKey(l)) &&
                            (Helpers.IsNumeric(r) || cache.ContainsKey(r))
                        )
                        {
                            var lu = Helpers.IsNumeric(l) ? ushort.Parse(l) : cache[l];
                            var ru = Helpers.IsNumeric(r) ? ushort.Parse(r) : cache[r];
                            cache[wire] = (ushort)(lu | ru);
                        }
                    }

                    if (rLeft.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rLeft.Match(op);
                        var l = m.Groups[1].Value;
                        var r = m.Groups[2].Value;
                        if (
                            (Helpers.IsNumeric(l) || cache.ContainsKey(l)) &&
                            (Helpers.IsNumeric(r) || cache.ContainsKey(r))
                        )
                        {
                            var lu = Helpers.IsNumeric(l) ? ushort.Parse(l) : cache[l];
                            var ru = Helpers.IsNumeric(r) ? ushort.Parse(r) : cache[r];
                            cache[wire] = (ushort)(lu << ru);
                        }
                    }

                    if (rRight.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rRight.Match(op);
                        var l = m.Groups[1].Value;
                        var r = m.Groups[2].Value;
                        if (
                            (rIsNum.IsMatch(l) || cache.ContainsKey(l)) &&
                            (rIsNum.IsMatch(r) || cache.ContainsKey(r))
                        )
                        {
                            var lu = Helpers.IsNumeric(l) ? ushort.Parse(l) : cache[l];
                            var ru = Helpers.IsNumeric(r) ? ushort.Parse(r) : cache[r];
                            cache[wire] = (ushort)(lu >> ru);
                        }
                    }

                    if (rNot.IsMatch(op) &&
                            !cache.ContainsKey(wire))
                    {
                        var m = rNot.Match(op);
                        var l = m.Groups[1].Value;
                        if (
                            (Helpers.IsNumeric(l) || cache.ContainsKey(l))
                        )
                        {
                            var lu = Helpers.IsNumeric(l) ? ushort.Parse(l) : cache[l];
                            cache[wire] = (ushort)(~lu);
                        }
                    }
                }
            }
        }

        public void GetInputData(string filePath)
        {
            foreach(var line in File.ReadAllLines(filePath))
            {
                FormattedInputs.Add((line.Replace(" -> ","=").Split('=')[0], line.Replace(" -> ", "=").Split('=')[1]));
            }
        }
    }
}
