using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day052016 : IAdventOfCodeData<string>
    {
        public string Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var passLength = 8;
            var password = new char[passLength];
            var counter = 0;
            var digits = Enumerable.Range(0, 8).Select(x => x.ToString()[0]);
            var rnd = new Random();
            DrawDoor();
            while (password.Any(x => x == '\0'))
            {
                var hashString = $"{Input}{counter++}";
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(hashString));
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("X2"));
                }
                var hash = strBuilder.ToString();
                if (hash.Substring(0, 5) == "00000")
                {
                    if (digits.Contains(hash[5]))
                    {
                        var digit = int.Parse(hash[5].ToString());
                        password[digit] = password[digit] == '\0' ? hash[6] : password[digit];
                    }
                }
                if (counter % 10000 == 0)
                {
                    OpenDoor();
                    var hackerString = password.Select(x => x == '\0' ? (char)(rnd.Next((int)'0', (int)'z')) : x).ToArray();
                    Console.SetCursorPosition(63, 10);
                    Console.Write(new string(hackerString));
                }
            }

            OpenDoor();
            Result = new string(password.ToArray());

            return $"{Result}";
        }

        private void OpenDoor()
        {
            for (var i = 1; i < 20; i++)
            {
                Console.SetCursorPosition(60, i);
                Console.Write(" ");
                Console.SetCursorPosition(60-i, i);
                Console.Write("|");
                Console.SetCursorPosition(60+i, i);
                Console.Write("|");
            }
        }

        private void DrawDoor()
        {
            for (var i = 1; i < 20; i++)
            {
                Console.SetCursorPosition(20, i);
                Console.Write("|");
                Console.SetCursorPosition(40, i);
                Console.Write("|");
                Console.SetCursorPosition(60, i);
                Console.Write("|");
            }
            for (var i = 21; i < 60; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("`");
                Console.SetCursorPosition(i, 19);
                Console.Write(".");
            }
            for (var i = 62; i < 72; i++)
            {
                Console.SetCursorPosition(i, 9);
                Console.Write("`");
                Console.SetCursorPosition(i, 11);
                Console.Write(".");
            }
            Console.SetCursorPosition(62, 10);
            Console.Write("`");
            Console.SetCursorPosition(71, 10);
            Console.Write("`");

        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllText(file);
        }
    }
}
