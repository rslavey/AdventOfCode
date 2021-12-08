using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day212016 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public static List<char[]> Perms = new List<char[]>();
        public string GetSolution(int partId)
        {
            switch (partId)
            {
                case 1:
                    {
                        var password = "abcdefgh".ToCharArray();
                        foreach (var line in Input)
                        {
                            var r1 = new Regex(@"^swap position (\d*) with position (\d*)$").Match(line);
                            var r2 = new Regex(@"^move position (\d*) to position (\d*)$").Match(line);
                            var r3 = new Regex(@"^swap letter ([a-z]*) with letter ([a-z]*)$").Match(line);
                            var r4 = new Regex(@"^reverse positions (\d*) through (\d*)$").Match(line);
                            var r5 = new Regex(@"^rotate ([^ ]*) (\d*) step[a-z]*$").Match(line);
                            var r6 = new Regex(@"^rotate based on position of letter ([a-z]*)$").Match(line);
                            if (r1.Success)
                            {
                                Swap(int.Parse(r1.Groups[1].Value), int.Parse(r1.Groups[2].Value), ref password);
                            }
                            if (r2.Success)
                            {
                                Move(int.Parse(r2.Groups[1].Value), int.Parse(r2.Groups[2].Value), ref password);
                            }
                            if (r3.Success)
                            {
                                SwapLetter(r3.Groups[1].Value[0], r3.Groups[2].Value[0], ref password);
                            }
                            if (r4.Success)
                            {
                                Reverse(int.Parse(r4.Groups[1].Value), int.Parse(r4.Groups[2].Value), ref password);
                            }
                            if (r5.Success)
                            {
                                RotateDir(r5.Groups[1].Value, int.Parse(r5.Groups[2].Value), ref password);
                            }
                            if (r6.Success)
                            {
                                RotatePosition(r6.Groups[1].Value[0], ref password);
                            }
                        }
                        return $"{new string(password)}";
                    }
                default:
                    {
                        string targetPasswordString = "fbgdceah";
                        var targetPassword = targetPasswordString.ToCharArray();
                        GetPer("abcdefgh".ToCharArray());
                        foreach(var p in Perms)
                        {
                            var password = (char[])p.Clone();
                            foreach (var line in Input)
                            {
                                var r1 = new Regex(@"^swap position (\d*) with position (\d*)$").Match(line);
                                var r2 = new Regex(@"^move position (\d*) to position (\d*)$").Match(line);
                                var r3 = new Regex(@"^swap letter ([a-z]*) with letter ([a-z]*)$").Match(line);
                                var r4 = new Regex(@"^reverse positions (\d*) through (\d*)$").Match(line);
                                var r5 = new Regex(@"^rotate ([^ ]*) (\d*) step[a-z]*$").Match(line);
                                var r6 = new Regex(@"^rotate based on position of letter ([a-z]*)$").Match(line);
                                if (r1.Success)
                                {
                                    Swap(int.Parse(r1.Groups[1].Value), int.Parse(r1.Groups[2].Value), ref password);
                                }
                                if (r2.Success)
                                {
                                    Move(int.Parse(r2.Groups[1].Value), int.Parse(r2.Groups[2].Value), ref password);
                                }
                                if (r3.Success)
                                {
                                    SwapLetter(r3.Groups[1].Value[0], r3.Groups[2].Value[0], ref password);
                                }
                                if (r4.Success)
                                {
                                    Reverse(int.Parse(r4.Groups[1].Value), int.Parse(r4.Groups[2].Value), ref password);
                                }
                                if (r5.Success)
                                {
                                    RotateDir(r5.Groups[1].Value, int.Parse(r5.Groups[2].Value), ref password);
                                }
                                if (r6.Success)
                                {
                                    RotatePosition(r6.Groups[1].Value[0], ref password);
                                }
                            }
                            if (password.SequenceEqual(targetPassword))
                            {
                                return $"{new string(p)}";
                            }
                        }
                        return $"";
                    }
            }
        }

        private void RotatePosition(char v, ref char[] password, bool reverse = false)
        {
            var pos = Array.FindIndex(password, x => x == v);
            var rotations = 1 + pos + (pos >= 4 ? 1 : 0);
            RotateDir("right", rotations, ref password);
        }

        private void RotateDir(string value, int v, ref char[] password, bool reverse = false)
        {
            for (var i = 0; i < v; i++)
            {
                var dir = value == "right" ? 1 : -1;
                var temp = password.GetValue(dir == 1 ? password.Length - 1 : 0);
                Array.Copy(password, dir == 1 ? 0 : 1, password, dir == 1 ? 1 : 0, password.Length - 1);
                password.SetValue(temp, dir == 1 ? 0 : password.Length - 1);
            }
        }

        private void Reverse(int v1, int v2, ref char[] password, bool reverse = false)
        {
            var loops = (int)((v2 - v1 + 1) / 2);
            for (var i = 0; i < loops; i++)
            {
                Swap(v1 + i, v2 - i, ref password);
            }
        }

        private void SwapLetter(char v1, char v2, ref char[] password, bool reverse = false)
        {
            var pos1 = Array.IndexOf(password, v1);
            var pos2 = Array.IndexOf(password, v2);
            Swap(pos1, pos2, ref password);
        }

        private void Move(int v1, int v2, ref char[] password, bool reverse = false)
        {
            var temp = password.GetValue(v1);
            Array.Copy(password, v1 + 1, password, v1, password.Length - (v1 + 1));
            Array.Copy(password, v2, password, v2 + 1, password.Length - (v2 + 1));
            password.SetValue(temp, v2);
        }

        private void Swap(int v1, int v2, ref char[] password, bool reverse = false)
        {
            var t = password[v1];
            password[v1] = password[v2];
            password[v2] = t;
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        public static void GetPer(char[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private static void GetPer(char[] list, int k, int m)
        {
            if (k == m)
            {
                Perms.Add((char[])list.Clone());
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

    }
}
