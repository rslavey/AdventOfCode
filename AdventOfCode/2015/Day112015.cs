using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day112015 : IAdventOfCodeSingleData
    {
        public string Result { get; set; }
        public string InputValue { get; set; }
        private static IEnumerable<char[]> charSetsOfThree { get; set; }
        public string GetSolution(int partId)
        {
            charSetsOfThree = Enumerable.Range('a', 24).Select(x => Enumerable.Range(x, 3).Select(xx => (char)xx).ToArray());

            var curPass = InputValue.ToCharArray();
            NextValidPassword(ref curPass);
            
            return partId == 1 ?
                new string(curPass) :
                "";
        }

        private void NextValidPassword(ref char[] v)
        {
            v = LetterIncrement(v);
            while (!IsValid(v))
            {
                v = LetterIncrement(v);
            }
        }

        private char[] LetterIncrement(char[] v, int index = 0)
        {
            var nextChar = v[v.Length - 1 - index] == 'z' ? 'a' : (char)(v[v.Length - 1 - index] + 1);
            v[v.Length - 1 - index] = nextChar;
            return nextChar == 'a' ? LetterIncrement(v, ++index) : v;
        }

        private bool IsValid (char[] c)
        {
            var s = new string(c);
            var sets = string.Join("|", charSetsOfThree.Select(x => new string(x)));
            var isNotIOL = !Regex.IsMatch(s, @"[iol]");
            var repeats = Regex.Matches(s, @"(.)\1").Count > 1;
            var isSets = Regex.IsMatch(s, $"^{sets}$");
            var isMatch = isNotIOL && repeats && isSets;
            if (isMatch && isNotIOL)
            {
                var what = s;
            }

            return isMatch;
        }
        public void GetInputData(string file)
        {
            InputValue = File.ReadAllLines(file)[0];
        }
    }
}
