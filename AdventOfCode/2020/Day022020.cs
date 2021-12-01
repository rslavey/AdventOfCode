using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day022020 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        private List<(int lowReq, int highReq, char letter, string pass)> SplitInput = new List<(int lowReq, int highReq, char letter, string pass)>();

        public string GetSolution(int part)
        {
            Result = part == 1 ?
                SplitInput.Count(x => Enumerable.Range(x.lowReq, x.highReq - x.lowReq + 1).Contains(x.pass.ToCharArray().Count(xx => xx == x.letter))) :
                SplitInput.Count(x => x.pass[x.lowReq - 1] == x.letter ^ x.pass[x.highReq - 1] == x.letter);

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach (var line in Input)
            {
                SplitInput.Add(
                    (
                        int.Parse(line.Split(' ')[0].Split('-')[0]),
                        int.Parse(line.Split(' ')[0].Split('-')[1]),
                        line.Split(' ')[1].ToCharArray()[0],
                        line.Split(' ')[2]
                    )
                );
            }
        }
    }
}
