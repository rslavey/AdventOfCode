using System;
using System.IO;
using System.Linq;
using System.Text;

namespace com.randyslavey.AdventOfCode
{
    class Day102015 : IAdventOfCodeSingleData
    {
        public string Result { get; set; }
        public string InputValue { get; set; }

        public string GetSolution(int partId)
        {
            return partId == 1 ?
                $"{LookAndSay(InputValue, 50).Length}" :
                "";
        }

        private string LookAndSay(string num, int count)
        {
            if (count == 0)
            {
                return num;
            }

            var sbNum = new StringBuilder();
            for (var i = 0; i < num.Length; i++)
            {
                var sb = new StringBuilder();
                sb.Append(num[i]);
                while (i + 1 < num.Length && num[i] == num[i + 1])
                {
                    sb.Append(num[i++ + 1]);
                }
                sbNum.Append($"{sb.Length}{num[i]}");
            }
            return LookAndSay(sbNum.ToString(), --count);
        }

        public void GetInputData(string file)
        {
            InputValue = File.ReadAllLines(file)[0];
        }
    }
}
