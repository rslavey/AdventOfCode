using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day142015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            var raceTime = 2503;
            var r = new Regex(@"^(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds\.$");
            IEnumerable<(string name, int speed, int active, int rest)> reindeer = Input.Select(x =>
                        ( r.Match(x).Groups[1].Value, int.Parse(r.Match(x).Groups[2].Value), int.Parse(r.Match(x).Groups[3].Value), int.Parse(r.Match(x).Groups[4].Value) )
                    );

            Dictionary<string, int> reindeerPoints = reindeer.Select(x => new KeyValuePair<string, int>(x.name, 0)).ToDictionary(x => x.Key, x => x.Value);

            IEnumerable<(string name, IEnumerable<int> speeds, int total)> race = reindeer.Select(x => (
                x.name, 
                Enumerable.Range(0, raceTime).Select(t => speedAtTime(x, t)),
                Enumerable.Range(0, raceTime).Sum(t => speedAtTime(x, t))
                )
            );

            //var points = Enumerable.Range(0, raceTime).Select(x => race.Select(xx => xx.speeds.Take(x).Sum()))

            Result = partId == 1 ?
                reindeer.Max(x => Enumerable.Range(0, raceTime)
                    .Sum(t => speedAtTime(x, t))
                ) :
                reindeer.Max(x => Enumerable.Range(0, raceTime)
                    .Sum(t => speedAtTime(x, t))
                );

            return $"{Result}";
        }

        private int speedAtTime((string name, int speed, int active, int rest) x, int time)
        {
            return time % (x.active + x.rest) < x.active ? x.speed : 0;
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }
    }
}
