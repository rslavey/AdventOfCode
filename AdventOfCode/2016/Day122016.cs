using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day122016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public string GetSolution(int partId)
        {
            Dictionary<string, long> CurValues = new Dictionary<string, long> { { "a", 0 }, { "b", 0 }, { "c", partId == 1 ? 0 : 1 }, { "d", 0 } };
            var step = 0;
            while (step < Input.Length)
            {
                var lineSplit = Input[step].Split(' ');
                switch (lineSplit[0])
                {
                    case "cpy":
                        long copyVal = 0;
                        copyVal = long.TryParse(lineSplit[1], out copyVal) ? copyVal : CurValues[lineSplit[1]];
                        CurValues[lineSplit[2]] = copyVal;
                        step++;
                        break;
                    case "inc":
                        CurValues[lineSplit[1]]++;
                        step++;
                        break;
                    case "dec":
                        CurValues[lineSplit[1]]--;
                        step++;
                        break;
                    case "jnz":
                        long jnzVal = 0;
                        jnzVal = long.TryParse(lineSplit[1], out jnzVal) ? jnzVal : CurValues[lineSplit[1]];
                        step += jnzVal != 0 ? int.Parse(lineSplit[2]) : 1;
                        break;
                }
            }
            return $"{CurValues["a"]}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
