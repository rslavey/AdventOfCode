using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day202015 : IAdventOfCodeData<string>
    {
        public int Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            var target = int.Parse(Input);

            var house = 0;
            var presents = 0;
            if (partId == 1)
            {
                while (presents < target)
                {
                    house++;
                    presents = 0;
                    for (int elf = 1; elf <= Math.Sqrt(house); elf++)
                    {
                        if (house % elf == 0)
                        {
                            if (house / elf == elf)
                            {
                                presents += elf * 10;
                            }
                            else
                            {
                                presents += (elf * 10) + (house / elf * 10);
                            }
                        }
                    }
                }
                Result = house;
            }
            else
            {
                while (presents < target)
                {
                    house++;
                    presents = 0;
                    for (int elf = 1; elf <= Math.Sqrt(house); elf++)
                    {
                        if (house % elf == 0)
                        {
                            if (house / elf == elf)
                            {
                                presents += (house / elf) > 50 ? 0 : elf * 11;
                            }
                            else
                            {
                                presents += ((house / elf) > 50 ? 0 : (elf * 11)) + ((house / (house / elf)) > 50 ? 0 : (house / elf * 11));
                            }
                        }
                    }
                }
                Result = house;
            }

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllText(file);
        }

    }
}
