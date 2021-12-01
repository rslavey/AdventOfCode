using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day022015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }
        private List<(int l, int w, int h)> SplitInput = new List<(int l, int w, int h)>();

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                SplitInput.Sum(x => (2 * x.l * x.w) + (2 * x.w * x.h) +( 2 * x.h * x.l ) + (new List<int>{ (x.l * x.w), (x.w * x.h), (x.h * x.l) }).Min()) :
                SplitInput.Sum(x => (new List<int> { x.l, x.w, x.h }).Aggregate(1, (a, v) => a * v) + ((new List<int> { x.l, x.w, x.h }).OrderBy(xx => xx).Take(2).Aggregate(0, (a, v) => a + (v * 2))));

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach (var line in Input)
            {
                SplitInput.Add(
                    (int.Parse(line.Split('x')[0]),
                    int.Parse(line.Split('x')[1]),
                    int.Parse(line.Split('x')[2])
                    ));
            }

        }
    }
}
