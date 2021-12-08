using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day142016 : IAdventOfCodeData<string>
    {
        public int Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            var keys = new List<Key>();
            var goodKeys = new HashSet<Key>();
            var index = 1000;
            for (var i = 0; i < 1000; i++)
            {
                var hash = CreateMD5($"{Input}{i}");
                if (partId == 2)
                {
                    for (var ii = 0; ii < 2016; ii++)
                    {
                        hash = CreateMD5(hash);
                    }
                }
                keys.Add(new Key { Hash = hash, Index = i });
            }
            while (goodKeys.Count() < 64)
            {
                var repeatKey = HasRepeat(keys[index - 1000].Hash, 3);
                if (repeatKey != null)
                {
                    var matchingIndex = keys.Skip(index - 1000 + 1).Take(1000).Any(x => HasRepeat(x.Hash, 5, (char)repeatKey) != null);
                    if (matchingIndex)
                    {
                        goodKeys.Add(keys[index - 1000]);
                        Console.WriteLine(goodKeys.Count());
                    }
                }

                //add another record
                var hash = CreateMD5($"{Input}{index}");
                if (partId == 2)
                {
                    for (var ii = 0; ii < 2016; ii++)
                    {
                        hash = CreateMD5(hash);
                    }
                }
                keys.Add(new Key { Hash = hash, Index = index++ });
            }
            Result = goodKeys.Last().Index;
            return $"{Result}";
        }

        private char? HasRepeat(string hash, int v, char? charCheck = null)
        {
            for (var i = 0; i < hash.Length - (v - 1); i++)
            {
                if (new string(hash[i], v) == new string(hash.Skip(i).Take(v).ToArray()))
                {
                    if (charCheck == null || hash[i] == charCheck)
                    {
                        return hash[i];
                    }
                }
            }
            return null;
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file)[0];
        }
        public class Key
        {
            public string Hash { get; set; }
            public int Index { get; set; }
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).ToLower().Replace("-", string.Empty);
            }
        }
    }

}
