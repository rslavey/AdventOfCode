using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day182016 : IAdventOfCodeData<string>
    {
        public string Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            List<string> map = new List<string>();
            map.Add(Input);
            var mapSize = partId == 1 ? 40 : 400000;
            for (var i = 0; i < mapSize - 1; i++)
            {
                var test = map.LastOrDefault();
                var sb = new StringBuilder();
                for (var ii = 0; ii < test.Length; ii++)
                {
                    bool isTrap = false;
                    var t1 = ii - 1 >= 0 && test[ii - 1] == '^' ? 1 : 0;
                    var t2 = test[ii] == '^' ? 2 : 0;
                    var t3 = ii + 1 < test.Length && test[ii + 1] == '^' ? 4 : 0;
                    switch (t1 + t2 + t3)
                    {
                        case 3:
                        case 6:
                        case 1:
                        case 4:
                            isTrap = true;
                            break;
                    }
                    sb.Append(isTrap ? "^" : ".");
                }
                map.Add(sb.ToString());
            }
            return $"{map.SelectMany(x => x).Count(x => x == '.')}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0];
        }

    }
}
