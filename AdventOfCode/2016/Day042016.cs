using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day042016 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }
        List<(string name, int sectorId, string checksum)> FI = new List<(string name, int sectorId, string checksum)>();
        public string GetSolution(int partId)
        {
            var count = 0;
            if (partId == 1)
            {
                for (var i = 0; i < FI.Count(); i++)
                {
                    count += new string(FI[i].name.Replace("-", "").GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Take(FI[i].checksum.Length).Select(x => x.Key).ToArray()) == FI[i].checksum ? FI[i].sectorId : 0;
                }
                Result = count;
            }
            else
            {
                List<(string name, int sectorId, string checksum)> realRooms = new List<(string name, int sectorId, string checksum)>();
                for (var i = 0; i < FI.Count(); i++)
                {
                    if (new string(FI[i].name.Replace("-", "").GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Take(FI[i].checksum.Length).Select(x => x.Key).ToArray()) == FI[i].checksum)
                    {
                        realRooms.Add(FI[i]);
                    }
                }
                foreach(var room in realRooms)
                {
                    if ($"{Decode(room.name, room.sectorId)} {room.sectorId}".Contains("north"))
                    {
                        Console.WriteLine($"{Decode(room.name, room.sectorId)} {room.sectorId}");
                    }
                }
            }
            return $"{Result}";
        }

        private string Decode(string name, int sectorId)
        {
            var offset = sectorId % 26;
            var newString = new char[name.Length];
            for (var i = 0; i < name.Length; i++)
            {
                if (name[i] == '-')
                {
                    newString[i] = '-';
                }
                else
                {
                    var charId = (int)name[i];
                    if (charId + offset <= 122)
                    {
                        newString[i] = (char)(charId + offset);
                    }
                    else
                    {
                        newString[i] = (char)(offset - (122 - charId) + 96);
                    }
                }
            }
            return new string(newString);
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^(([a-z]+-)+)([0-9]+)\[([a-z]+)\]$");
            foreach(var line in File.ReadLines(file))
            {
                var m = r.Match(line);
                FI.Add((m.Groups[1].Value.Trim('-'), int.Parse(m.Groups[3].Value), m.Groups[4].Value));
            }
            Input = File.ReadAllLines(file);
        }

    }
}
