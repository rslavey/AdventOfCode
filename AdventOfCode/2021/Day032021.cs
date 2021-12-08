using Combinatorics.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day032021 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        bool[] g = new bool[Input.Max(x => x.Length)];
                        bool[] e = new bool[Input.Max(x => x.Length)];
                        for (var i = 0; i < Input.Max(x => x.Length); i++)
                        {
                            var cc = Input.Select(x => x[i]).Count(x => x == '1') > Input.Select(x => x[i]).Count(x => x == '0') ? true : false;
                            g.SetValue(cc, i);
                        }
                        new BitArray(g).Not().CopyTo(e, 0);
                        Result = Convert.ToInt32(new string(g.Select(x => x ? '1' : '0').ToArray()), 2) * Convert.ToInt32(new string(e.Select(x => x ? '1' : '0').ToArray()), 2);
                        return $"{Result}";
                    }
                default:
                    {
                        List<string> inputList = new List<string>(Input);
                        for (var i = 0; i < inputList.Max(x => x.Length); i++)
                        {
                            var ocount = inputList.Select(x => x[i]).Count(x => x == '1');
                            var zcount = inputList.Select(x => x[i]).Count(x => x == '0');
                            var cc = ocount == zcount ? '1' : ocount > zcount ? '1' : '0';
                            inputList.RemoveAll(x => x[i] != cc);
                            if (inputList.Count() == 1)
                            {
                                break;
                            }
                        }
                        var ogr = Convert.ToInt32(new string(inputList[0].ToArray()), 2);
                        inputList = new List<string>(Input);
                        for (var i = 0; i < inputList.Max(x => x.Length); i++)
                        {
                            var ocount = inputList.Select(x => x[i]).Count(x => x == '1');
                            var zcount = inputList.Select(x => x[i]).Count(x => x == '0');
                            var cc = ocount == zcount ? '0' : ocount > zcount ? '0' : '1';
                            inputList.RemoveAll(x => x[i] != cc);
                            if (inputList.Count() == 1)
                            {
                                break;
                            }
                        }
                        var csr = Convert.ToInt32(new string(inputList[0].ToArray()), 2);
                        Result = ogr * csr;
                        return $"{Result}";
                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
