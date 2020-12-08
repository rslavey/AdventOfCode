using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day152015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        List<(string ingredient, int capacity, int durability, int flavor, int texture, int calories)> FormattedInputs = new List<(string ingredient, int capacity, int durability, int flavor, int texture, int calories)>();
        public string GetSolution(int partId)
        {
            var max = 0;
            for (int i = 0; i <= 100; i++)
            {
                for (int ii = 0; ii < 100 - i; ii++)
                {
                    for (int iii = 0; iii < 100 - i - ii; iii++)
                    {
                        int iiii = 100 - i - ii - iii;
                        var c = (FormattedInputs[0].capacity * i) + (FormattedInputs[1].capacity * ii) + (FormattedInputs[2].capacity * iii) + (FormattedInputs[3].capacity * iiii);
                        var d = (FormattedInputs[0].durability * i) + (FormattedInputs[1].durability * ii) + (FormattedInputs[2].durability * iii) + (FormattedInputs[3].durability * iiii); ;
                        var f = (FormattedInputs[0].flavor * i) + (FormattedInputs[1].flavor * ii) + (FormattedInputs[2].flavor * iii) + (FormattedInputs[3].flavor * iiii); ;
                        var t = (FormattedInputs[0].texture * i) + (FormattedInputs[1].texture * ii) + (FormattedInputs[2].texture * iii) + (FormattedInputs[3].texture * iiii); ;
                        var calories = i * FormattedInputs[0].calories + ii * FormattedInputs[1].calories + iii * FormattedInputs[2].calories + iiii * FormattedInputs[3].calories;
                        var sum = (c < 0 ? 0 : c) * (d < 0 ? 0 : d) * (f < 0 ? 0 : f) * (t < 0 ? 0 : t);
                        if (sum > max)
                        {
                            if (partId == 1 || (partId == 2 && calories == 500))
                            {
                                max = sum;
                            }
                        }
                    }
                }
            }

            Result = max;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^(.*): capacity (.*), durability (.*), flavor (.*), texture (.*), calories (.*)$");
            FormattedInputs = File.ReadAllLines(file).Select(x =>
                (
                    r.Match(x).Groups[1].Value,
                    int.Parse(r.Match(x).Groups[2].Value),
                    int.Parse(r.Match(x).Groups[3].Value),
                    int.Parse(r.Match(x).Groups[4].Value),
                    int.Parse(r.Match(x).Groups[5].Value),
                    int.Parse(r.Match(x).Groups[6].Value)
                )
            ).ToList();
        }
    }
}
