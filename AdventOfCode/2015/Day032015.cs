using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day032015 : IAdventOfCodeSingleData
    {
        public int Result { get; set; }
        public string InputValue { get; set; }
        public IEnumerable<(char value, int index)> IndexedInputValues { get; private set; }

        public string GetSolution(int partId)
        {
            Result = partId == 1 ?
                IndexedInputValues
                    .Select(x => new
                    {
                        h = IndexedInputValues.Count(xx => xx.index < x.index && xx.value == '>') - IndexedInputValues.Count(xx => xx.index < x.index && xx.value == '<'),
                        v = IndexedInputValues.Count(xx => xx.index < x.index && xx.value == '^') - IndexedInputValues.Count(xx => xx.index < x.index && xx.value == 'v')
                    }).GroupBy(x => new { x.h, x.v }).Distinct().Count() :
                IndexedInputValues
                    .Select(x => new
                    {
                        h = IndexedInputValues.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '>') - IndexedInputValues.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '<'),
                        v = IndexedInputValues.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == '^') - IndexedInputValues.Count(xx => xx.index < x.index && xx.index % 2 == x.index % 2 && xx.value == 'v')
                    }).GroupBy(x => new { x.h, x.v }).Distinct().Count();

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValue = File.ReadAllLines(file)[0];
            IndexedInputValues = InputValue.ToCharArray().Select((value, index) => (value, index));
        }
    }
}
