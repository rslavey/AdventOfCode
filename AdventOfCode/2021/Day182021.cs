using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day182021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
