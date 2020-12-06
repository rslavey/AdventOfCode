using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day012015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                InputValues[0].ToCharArray().Count(x => x == '(') - InputValues[0].ToCharArray().Count(x => x == ')') :
                InputValues[0].ToCharArray().Select((value,index) => (value,index)).FirstOrDefault(x => InputValues[0].ToCharArray(0, x.index).Count(xx => xx == ')') > InputValues[0].ToCharArray(0, x.index).Count(xx => xx == '(')).index;

            return $"{Result}";
        }

        public void GetInputData(string filePath)
        {
            InputValues = File.ReadAllLines(filePath);
        }
    }
}
