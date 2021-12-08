using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day172016 : IAdventOfCodeData<string>
    {
        public string Result { get; set; }
        public string Input { get; set; }
        private const string openLock = "bcdef";
        private string[] dirs = new string[] { "U", "D", "L", "R" };
        private List<string> successfulPaths = new List<string>();
        public string GetSolution(int partId)
        {
            (int x, int y) pos = (0, 0);
            WalkMaze(pos, string.Empty, Input, partId == 1 ? true : false);
            Result = partId == 1 ? successfulPaths.OrderBy(x => x.Length).FirstOrDefault() : $"{successfulPaths.OrderByDescending(x => x.Length).FirstOrDefault().Length - Input.Length}";
            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0];
        }

        private void WalkMaze((int x, int y) pos, string path, string hash, bool findShortest = true)
        {
            if (pos == (3, 3))
            {
                successfulPaths.Add(hash);
            }
            else
            {
                var m = CreateMD5(hash);
                if (successfulPaths.Count() == 0 || !findShortest || successfulPaths.Min(x => x.Length) > path.Length)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        if (openLock.Contains(m[i]) &&
                            (
                                (dirs[i] == "U" && pos.y > 0) ||
                                (dirs[i] == "D" && pos.y < 3) ||
                                (dirs[i] == "L" && pos.x > 0) ||
                                (dirs[i] == "R" && pos.x < 3)
                                )
                            )
                        {
                            var dir = dirs[i];
                            switch (dirs[i])
                            {
                                case "U":
                                    WalkMaze((pos.x, pos.y - 1), $"{path}{dirs[i]}", $"{hash}{dirs[i]}", findShortest);
                                    break;
                                case "D":
                                    WalkMaze((pos.x, pos.y + 1), $"{path}{dirs[i]}", $"{hash}{dirs[i]}", findShortest);
                                    break;
                                case "L":
                                    WalkMaze((pos.x - 1, pos.y), $"{path}{dirs[i]}", $"{hash}{dirs[i]}", findShortest);
                                    break;
                                case "R":
                                    WalkMaze((pos.x + 1, pos.y), $"{path}{dirs[i]}", $"{hash}{dirs[i]}", findShortest);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).ToLower().Replace("-", string.Empty);
            }
        }

    }
}
