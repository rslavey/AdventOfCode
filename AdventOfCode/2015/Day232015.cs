﻿using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day232015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }

        public string GetSolution(int partId)
        {
            var limit = InputValues.Length;
            var curLine = 0;
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 1 }, { "b", 0 } };
            var test = int.Parse("-5");
            while (curLine < limit)
            {
                var reg = InputValues[curLine].Split(' ')[1];
                var action = InputValues[curLine].Substring(0, 3);
                switch (action)
                {
                    case "jio":
                        if (registers[reg.Substring(0, 1)] == 1)
                        {
                            curLine += int.Parse(InputValues[curLine].Split(' ')[2]);
                        }
                        else
                        {
                            curLine++;
                        }
                        break;
                    case "jie":
                        if (registers[reg.Substring(0,1)] % 2 == 0)
                        {
                            curLine += int.Parse(InputValues[curLine].Split(' ')[2]);
                        }
                        else
                        {
                            curLine++;
                        }
                        break;
                    case "tpl":
                        registers[reg.Substring(0, 1)] *= 3;
                        curLine++;
                        break;
                    case "inc":
                        registers[reg.Substring(0, 1)]++;
                        curLine++;
                        break;
                    case "hlf":
                        registers[reg.Substring(0, 1)] = (int)Math.Floor(registers[reg.Substring(0, 1)] * .5);
                        curLine++;
                        break;
                    case "jmp":
                        curLine += int.Parse(reg);
                        break;

                }
            }


            Result = partId == 1 ?
                registers["b"] :
                1;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValues = File.ReadAllLines(file);
        }
    }
}

