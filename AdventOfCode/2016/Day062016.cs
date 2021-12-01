using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day062016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            if (partId == 1)
            {
                var pass = new char[Input.Max(x => x.Length)];
                for (var i = 0; i < Input.Max(x => x.Length); i++)
                {
                    pass[i] = Input.Select(x => x.ToCharArray()[i]).GroupBy(x => x).OrderByDescending(x => x.Count()).FirstOrDefault().Key;
                }
                Result = new string(pass);
            }
            else
            {
                var pass = new char[Input.Max(x => x.Length)];
                for (var i = 0; i < Input.Max(x => x.Length); i++)
                {
                    pass[i] = Input.Select(x => x.ToCharArray()[i]).GroupBy(x => x).OrderBy(x => x.Count()).FirstOrDefault().Key;
                }
                Result = new string(pass);
            }
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
