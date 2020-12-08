using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace com.randyslavey.AdventOfCode
{
    class Day042015 : IAdventOfCodeSingleData
    {
        public int Result { get; set; }
        public string InputValue { get; set; }
        public IEnumerable<(char value, int index)> IndexedInputValues { get; private set; }

        public string GetSolution(int partId)
        {
            string hash = "xxxxxx";
            int counter = -1;
            MD5 md5 = new MD5CryptoServiceProvider();

            while (hash.Substring(0,6) != "000000")
            {
                var hashString = $"{InputValue}{++counter}";
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(hashString));
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("X2"));
                }
                hash = strBuilder.ToString();
            }

            Result = partId == 1 ?
                counter :
                1;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            InputValue = File.ReadAllLines(file)[0];
            IndexedInputValues = InputValue.ToCharArray().Select((value, index) => (value, index));
        }
    }
}
