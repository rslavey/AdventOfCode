using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day052015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        public IEnumerable<(char value, int index)> IndexedInputValues { get; private set; }

        public string GetSolution(int partId)
        {
            var rVowel = new Regex("^([aeiou]+)$");
            var rRepeat = new Regex(@"(.)\1");
            var rBadStrings = new Regex("^(?!.*(ab|cd|pq|xy)).*$");
            var rGap = new Regex(@"(..).*\1");
            var rPairs = new Regex(@"(.).\1");
            Result = partId == 1 ?
                InputValues.Count(x => x.ToCharArray().Count(xx => "aeiou".Contains(xx)) >= 3 && rRepeat.IsMatch(x) && rBadStrings.IsMatch(x)) :
                InputValues.Count(x => rGap.IsMatch(x) && rPairs.IsMatch(x));

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValues = File.ReadAllLines(file);
        }
    }
}
