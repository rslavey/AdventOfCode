using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day012021 : IAdventOfCodeData<int[]>
    {
        public string Result { get; set; }
        public int[] Input { get; set; }

        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        Result = $"{Input.Select(selector: (v, i) => { return i < Input.Length - 1 && v < Input[i + 1]; }).Count(x => x)}";
                        return $"{Result}";
                    }
                default:
                    {
                        Result = $"{Input.Select(selector: (v, i) => { return i < Input.Length - 1 && Input.Skip(i).Take(3).Sum() < Input.Skip(i + 1).Take(3).Sum(); }).Count(x => x)}";
                        return $"{Result}";
                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file).Select(x => int.Parse(x)).ToArray();
        }

    }
}
