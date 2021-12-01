using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day032015 : IAdventOfCodeData<string>
    {
        public int Result { get; set; }
        public string Input { get; set; }
        public IEnumerable<(char value, int index)> IndexedInput { get; private set; }

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                IndexedInput
                    .Select(x => new
                    {
                        h = IndexedInput.Count(xx => xx.index < x.index && xx.value == '>') - IndexedInput.Count(xx => xx.index < x.index && xx.value == '<'),
                        v = IndexedInput.Count(xx => xx.index < x.index && xx.value == '^') - IndexedInput.Count(xx => xx.index < x.index && xx.value == 'v')
                    }).GroupBy(x => new { x.h, x.v }).Distinct().Count() :
                IndexedInput
                    .Select(x => new
                    {
                        h = IndexedInput.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '>') - IndexedInput.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '<'),
                        v = IndexedInput.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '^') - IndexedInput.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == 'v')
                    }).GroupBy(x => new { x.h, x.v }).Distinct().Count();

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0];
            IndexedInput = Input.ToCharArray().Select((value, index) => (value, index));
        }
    }
}
