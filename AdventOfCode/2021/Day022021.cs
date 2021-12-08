using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day022021 : IAdventOfCodeData<(string dir, int dis)[]>
    {
        public int Result { get; set; }
        public (string dir, int dis)[] Input { get; set; }

        public string GetSolution(int partId)
        {
            var hor = 0;
            var ver = 0;
            var aim = 0;
            switch (partId)
            {
                case 1:
                    {
                        Result = (Input.Where(x => x.dir == "forward").Sum(x => x.dis)) * (Input.Where(x => x.dir == "down").Sum(x => x.dis) - Input.Where(x => x.dir == "up").Sum(x => x.dis));
                        return $"{Result}";
                    }
                default:
                    {
                        foreach (var line in Input)
                        {
                            switch (line.dir)
                            {
                                case "forward":
                                    hor += line.dis;
                                    ver += aim * line.dis;
                                    break;
                                case "down":
                                    aim += line.dis;
                                    break;
                                case "up":
                                    aim -= line.dis;
                                    break;
                            }
                        }
                        Result = hor * ver;
                        return $"{Result}";
                    }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file).Select(x => (x.Split(' ')[0], int.Parse(x.Split(' ')[1]))).ToArray();
        }

    }
}
