using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day082021 : IAdventOfCodeUngroupedData
    {
        public string Result { get; set; }
        public string[] InputValues { get; set; }

        public string GetSolution(int partId)
        {
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValues = File.ReadAllLines(file);
        }

    }
}
