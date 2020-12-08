using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day022015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        private List<(int l, int w, int h)> SplitInputValues = new List<(int l, int w, int h)>();

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                SplitInputValues.Sum(x => (2 * x.l * x.w) + (2 * x.w * x.h) +( 2 * x.h * x.l ) + (new List<int>{ (x.l * x.w), (x.w * x.h), (x.h * x.l) }).Min()) :
                SplitInputValues.Sum(x => (new List<int> { x.l, x.w, x.h }).Aggregate(1, (a, v) => a * v) + ((new List<int> { x.l, x.w, x.h }).OrderBy(xx => xx).Take(2).Aggregate(0, (a, v) => a + (v * 2))));

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValues = File.ReadAllLines(file);
            foreach (var line in InputValues)
            {
                SplitInputValues.Add(
                    (int.Parse(line.Split('x')[0]),
                    int.Parse(line.Split('x')[1]),
                    int.Parse(line.Split('x')[2])
                    ));
            }

        }
    }
}
