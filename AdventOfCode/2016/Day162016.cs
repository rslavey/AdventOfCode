using Combinatorics.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day162016 : IAdventOfCodeData<BitArray>
    {
        public string Result { get; set; }
        public BitArray Input { get; set; }

        public string GetSolution(int partId)
        {
            var diskSize = partId == 1 ? 272 : 35651584;
            while (Input.Length < diskSize)
            {
                Input = Dragon(Input);
            }
            
            var diskData = new bool[Input.Length];
            Input.CopyTo(diskData, 0);
            diskData = diskData.Take(diskSize).ToArray();
            do
            {
                List<bool> cs = new List<bool>();
                for (var i = 0; i < diskData.Length; i += 2)
                {
                    cs.Add(diskData[i] == diskData[i + 1]);
                }
                diskData = cs.ToArray();
            } while (diskData.Length % 2 == 0);
            Result = $"{new string(diskData.Select(x => x ? '1' : '0').ToArray())}";
            return $"{Result}";
        }

        private BitArray Dragon(BitArray input)
        {
            bool[] a = new bool[Input.Length];
            bool[] b = new bool[Input.Length];
            input.CopyTo(a, 0);
            var r = $"{new string(a.Select(x => x ? '1' : '0').ToArray())}".Reverse();
            var ba = new BitArray(r.Select(x => x == '1').ToArray());
            ba.Not().CopyTo(b, 0);
            var ar = a.Append(false).Concat(b).ToArray();
            return new BitArray(ar);
        }

        public void GetInputData(string file)
        {
            var input = File.ReadAllLines(file)[0];
            Input = new BitArray(input.Select(x => x == '1').ToArray());
        }

    }
}
