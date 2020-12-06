using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day082015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public List<(int flen, int uel, int el)> FormattedInputs = new List<(int flen, int uel, int el)>();
        public string[] InputValues { get; set; }

        private static readonly Dictionary<string, ushort> cache = new Dictionary<string, ushort>();

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                FormattedInputs.Sum(x => x.flen - x.uel) :
                FormattedInputs.Sum(x => x.el - x.flen);

            return $"{Result}";
        }

        public void GetInputData(string filePath)
        {
            foreach(var line in File.ReadAllLines(filePath))
            {

                var flen = line.Length;
                var uel = Regex.Unescape(line.Remove(line.Length - 1, 1).Remove(0, 1)).Length;
                var el = Helpers.ToLiteral(line).Length;
                FormattedInputs.Add((flen, uel, el));
            }
        }
    }
}
