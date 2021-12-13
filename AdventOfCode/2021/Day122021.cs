using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day122021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        internal List<Cave> Caves = new List<Cave>();
        internal List<List<string>> Paths = new List<List<string>>();
        internal Regex rUpper = new Regex("^[A-Z]+[a-zA-Z]*$");
        public string GetSolution(int partId)
        {
            foreach (var startCave in Caves.Where(x => x.CaveName == "start"))
            {
                foreach (var connectedCave in startCave.ConnectedCaves)
                {
                    TrackPath(connectedCave, new List<string> { "start" }, partId == 1);
                }
            }
            return $"{Paths.Count()}";
        }

        private void TrackPath(Cave cave, List<string> paths, bool smallVisited = false)
        {
            paths.Add(cave.CaveName);
            smallVisited = smallVisited || (!rUpper.IsMatch(cave.CaveName)) && paths.Count(x => x == cave.CaveName) > 1;
            if (cave.CaveName != "end")
            {
                foreach (var connectedCave in Caves.Where(x => x.ConnectedCaves.Contains(cave) && (
                        rUpper.IsMatch(x.CaveName) ||
                        !paths.Contains(x.CaveName) ||
                        (x.CaveName != "start" &&
                        !smallVisited)
                    )))
                {
                    var newP = new List<string>(paths);
                    TrackPath(connectedCave, newP, smallVisited);
                }
            }
            else
            {
                Paths.Add(paths);
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            foreach (var line in Input)
            {
                var cs = line.Split('-');
                Cave cave0, cave1;
                if (Caves.Any(x => x.CaveName == cs[0]))
                {
                    cave0 = Caves.FirstOrDefault(x => x.CaveName == cs[0]);
                }
                else
                {
                    cave0 = new Cave { CaveName = cs[0] };
                    Caves.Add(cave0);
                }
                if (Caves.Any(x => x.CaveName == cs[1]))
                {
                    cave1 = Caves.FirstOrDefault(x => x.CaveName == cs[1]);
                }
                else
                {
                    cave1 = new Cave { CaveName = cs[1] };
                    Caves.Add(cave1);
                }
                cave1.ConnectedCaves.Add(cave0);
                cave0.ConnectedCaves.Add(cave1);
            }
        }

        internal class Cave
        {
            internal string CaveName { get; set; }
            internal List<Cave> ConnectedCaves = new List<Cave>();
            //internal bool IsVisited;
        }
    }
}
