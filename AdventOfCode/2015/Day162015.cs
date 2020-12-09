using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day162015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        List<(int index, Dictionary<string, int> compounds)> FormattedInputs = new List<(int index, Dictionary<string, int> compounds)>();
        Dictionary<string, int> AnalysisData = new Dictionary<string, int>();

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                FormattedInputs.FirstOrDefault(x => x.compounds.Intersect(AnalysisData).SequenceEqual(x.compounds)).index :
                FormattedInputs.FirstOrDefault(x =>
                    x.compounds.All(xx =>
                        (xx.Key == "cats" || xx.Key == "trees") ? (xx.Value > AnalysisData[xx.Key]) :
                        (xx.Key == "pomeranians" || xx.Key == "goldfish") ? (xx.Value < AnalysisData[xx.Key]) :
                        (xx.Value == AnalysisData[xx.Key])
                    )
                ).index;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^Sue ([^:]+): (.*)$");
            FormattedInputs = File.ReadAllLines(file).Select(x =>
                (
                    int.Parse(r.Match(x).Groups[1].Value),
                    r.Match(x).Groups[2].Value.Split(',').Select(xx =>
                        new KeyValuePair<string, int>(xx.Split(':')[0].Trim(), int.Parse(xx.Split(':')[1].Trim()))).ToDictionary(xx => xx.Key, xx => xx.Value)
                )
            ).ToList();

            AnalysisData = File.ReadAllLines($"{file.Replace(".txt", "")}-2.txt").Select(x => new KeyValuePair<string, int>(x.Split(',')[0], int.Parse(x.Split(',')[1]))).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
