using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day082016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            var gridWidth = 50;
            var gridHeight = 6;
            bool[,] grid = new bool[gridHeight, gridWidth];

            foreach (var line in Input)
            {
                if (line.Substring(0, 4) == "rect")
                {
                    (int x, int y) rectSize = (int.Parse(line.Split(' ')[1].Split('x')[0]), int.Parse(line.Split(' ')[1].Split('x')[1]));
                    for (var x = 0; x < rectSize.x; x++)
                    {
                        for (var y = 0; y < rectSize.y; y++)
                        {
                            grid[y, x] = true;
                        }
                    }
                }
                else
                {
                    var r = new Regex(@"^rotate (.*) (.*)=(.*) by (.*)$");
                    var m = r.Match(line);
                    var dir = m.Groups[2].Value;
                    var grIndex = int.Parse(m.Groups[3].Value);
                    var distance = int.Parse(m.Groups[4].Value);
                    var size = dir == "x" ? gridHeight : gridWidth;

                    var tempRow = new bool[size + distance];
                    for (var x = 0; x < size; x++)
                    {
                        tempRow[x + distance % size > (size - 1) ? (x + distance % size) - size : x + distance % size] = dir == "x" ? grid[x, grIndex] : grid[grIndex, x];
                    }
                    for (var x = 0; x < size; x++)
                    {
                        grid[dir == "x" ? x : grIndex, dir == "x" ? grIndex : x] = tempRow[x];
                    }
                }
            }
            if (partId == 1)
            {
                var totalLit = 0;
                foreach (var row in grid)
                {
                    totalLit += row ? 1 : 0;
                }

                Result = $"{totalLit}";
            }
            else
            {
                var result = new StringBuilder();
                for (var y = 0; y < gridHeight; y++)
                {
                    var sb = new StringBuilder();
                    for (var x = 0; x < gridWidth; x++)
                    {
                        sb.Append($"{(grid[y,x] ? "#" : ".")}");
                    }
                    result.AppendLine(sb.ToString());
                }
                Result = result.ToString();
            }
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
