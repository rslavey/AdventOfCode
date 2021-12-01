using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day012015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                Input[0].ToCharArray().Count(x => x == '(') - Input[0].ToCharArray().Count(x => x == ')') :
                Input[0].ToCharArray().Select((value,index) => (value,index)).FirstOrDefault(x => Input[0].ToCharArray(0, x.index).Count(xx => xx == ')') > Input[0].ToCharArray(0, x.index).Count(xx => xx == '(')).index;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
