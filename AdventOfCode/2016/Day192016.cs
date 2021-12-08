using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day192016 : IAdventOfCodeData<int>
    {
        public string Result { get; set; }
        public int Input { get; set; }

        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        var elfCount = Input;
                        var elfInts = new List<int>();
                        for (var i = 1; i <= elfCount; i++)
                        {
                            elfInts.Add(i);
                        }
                        int index = 0;
                        var tempElves = new List<int>();
                        while (elfInts.Count() > 1)
                        {
                            tempElves.Add(elfInts[index]);
                            if (index + 1 == elfInts.Count())
                            {
                                index = 1;
                                elfInts = tempElves;
                                tempElves = new List<int>();
                            }
                            else if (index + 2 == elfInts.Count())
                            {
                                index = 0;
                                elfInts = tempElves;
                                tempElves = new List<int>();
                            }
                            else
                            {
                                index+=2;
                            }
                        }

                        return $"{elfInts.FirstOrDefault()}";
                    }
                default:
                    {
                        var elfCount = Input;
                        //elfCount = 5;

                        int exp = 0;
                        while (Math.Pow(3,exp) < elfCount)
                        {
                            exp++;
                        }
                        var target = Math.Pow(3, exp - 1);

                        if (target * 2 < elfCount || target < elfCount)
                        {
                            return $"{elfCount - target}";
                        }
                        else
                        {
                            return $"{(elfCount - (target * 2)) / 2}";
                        }

                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = int.Parse(File.ReadAllLines(file)[0]);
            //Input = 5;
        }

    }
}
