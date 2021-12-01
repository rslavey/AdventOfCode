using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day012020 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                Helpers.FindSum(Input.Select(x => int.Parse(x)).ToArray(), new int[2], 0, Input.ToArray().Length - 1, 0, 2, 2020).Aggregate(1, (acc, val) => acc * val) :
                Helpers.FindSum(Input.Select(x => int.Parse(x)).ToArray(), new int[3], 0, Input.ToArray().Length - 1, 0, 3, 2020).Aggregate(1, (acc, val) => acc * val);

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
