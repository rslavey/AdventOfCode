using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day132015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public List<(string s, int d, string e)> FormattedInputs = new List<(string s, int d, string e)>();
        public string[] InputValues { get; set; }

        public string GetSolution(int partId)
        {
            if (partId == 2)
            {
                foreach(var person in FormattedInputs.Select(x => x.s).Union(FormattedInputs.Select(x => x.e)).Distinct().ToList())
                {
                    FormattedInputs.Add(("Me", 0, person));
                    FormattedInputs.Add((person, 0, "Me"));
                }
            }
            var people = FormattedInputs.Select(x => x.s).Union(FormattedInputs.Select(x => x.e)).Distinct();
            Permutations<string> ps = new Permutations<string>(people.ToArray(), GenerateOption.WithRepetition);

            var minDistance = Int32.MaxValue;
            var maxDistance = 0;
            foreach (var p in ps)
            {
                var distance = 0;
                for (var i = 0; i < p.Count() - 1; i++)
                {
                    distance += FormattedInputs.FirstOrDefault(x => (x.s == p[i]) && (x.e == p[i + 1])).d;
                    distance += FormattedInputs.FirstOrDefault(x => (x.e == p[i]) && (x.s == p[i + 1])).d;
                }
                distance += FormattedInputs.FirstOrDefault(x => (x.s == p.First()) && (x.e == p.Last())).d;
                distance += FormattedInputs.FirstOrDefault(x => (x.e == p.First()) && (x.s == p.Last())).d;
                minDistance = distance < minDistance ? distance : minDistance;
                maxDistance = distance > maxDistance ? distance : maxDistance;
            }
            Result = maxDistance;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^(.*) would (.*) (.*) happiness units by sitting next to (.*)\.$");
            foreach(var line in File.ReadAllLines(file))
            {
                var m = r.Match(line);
                FormattedInputs.Add((m.Groups[1].Value, (int.Parse(m.Groups[3].Value)) * (m.Groups[2].Value == "gain" ? 1 : -1), m.Groups[4].Value));
            }
        }
    }
}
