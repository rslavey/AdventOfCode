using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.randyslavey.AdventOfCode
{
    class Day062015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public List<(string action, int[] start, int[] end)> FormattedInputs = new List<(string action, int[] start, int[] end)>();
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    var grid = new bool[1000, 1000];
                    foreach (var (action, start, end) in FormattedInputs)
                    {
                        for (var ix = start[0]; ix <= end[0]; ix++)
                        {
                            for (var iy = start[1]; iy <= end[1]; iy++)
                            {
                                grid[ix, iy] = action == "toggle" ? !grid[ix, iy] : action == "on";
                            }
                        }
                    }
                    Result = grid.Cast<bool>().Count(val => val);
                    break;
                default:
                    var grid2 = new int[1000, 1000];
                    foreach (var (action, start, end) in FormattedInputs)
                    {
                        for (var ix = start[0]; ix <= end[0]; ix++)
                        {
                            for (var iy = start[1]; iy <= end[1]; iy++)
                            {
                                grid2[ix, iy] += action == "toggle" ? 2 : action == "on" ? 1 : grid2[ix, iy] > 0 ? -1 : 0;
                            }
                        }
                    }
                    Result = grid2.Cast<int>().Sum(val => val);
                    break;
            }

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            foreach(var line in File.ReadAllLines(file))
            {
                var newLine = line.Replace("through ", "").Replace("turn on", "on").Replace("turn off", "off");
                var action = newLine.Split(' ')[0];
                var start = newLine.Split(' ')[1].Split(',').Select(x => int.Parse(x));
                var end = newLine.Split(' ')[2].Split(',').Select(x => int.Parse(x));
                FormattedInputs.Add((action, start.ToArray(), end.ToArray()));
            }
        }
    }
}
