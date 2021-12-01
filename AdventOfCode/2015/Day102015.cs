using System;
using System.IO;
using System.Linq;
using System.Text;

namespace com.randyslavey.AdventOfCode
{
    class Day102015 : IAdventOfCodeData<string>
    {
        public string Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            return partId == 1 ?
                $"{LookAndSay(Input, 50).Length}" :
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
            Input = File.ReadAllLines(file)[0];
        }
    }
}
