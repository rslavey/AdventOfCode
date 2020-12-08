using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day092015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public List<(string s, string e, int d)> FormattedInputs = new List<(string s, string e, int d)>();
        public string[] InputValues { get; set; }

        public string GetSolution(int partId)
        {
            var cities = FormattedInputs.Select(x => x.s).Union(FormattedInputs.Select(x => x.e)).Distinct();
            Permutations<string> ps = new Permutations<string>(cities.ToArray(), GenerateOption.WithRepetition);

            var minDistance = Int32.MaxValue;
            var maxDistance = 0;
            foreach (var p in ps)
            {
                var distance = 0;
                for (var i = 0; i < p.Count() - 1; i++)
                {
                    distance += FormattedInputs.FirstOrDefault(x => (x.s == p[i] || x.e == p[i]) && (x.s == p[i + 1] || x.e == p[i + 1])).d;
                }
                minDistance = distance < minDistance ? distance : minDistance;
                maxDistance = distance > maxDistance ? distance : maxDistance;
            }
            Result = partId == 1 ?
                minDistance :
                maxDistance;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^(.*) to (.*) = (.*)$");
            foreach(var line in File.ReadAllLines(file))
            {
                var m = r.Match(line);
                FormattedInputs.Add((m.Groups[1].Value, m.Groups[2].Value, int.Parse(m.Groups[3].Value)));
            }
        }
    }
}
