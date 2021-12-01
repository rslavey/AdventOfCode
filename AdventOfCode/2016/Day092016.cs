using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day092016 : IAdventOfCodeData<string>
    {
        public long Result { get; set; }
        public string Input { get; set; }
        long uncompLength = 0;
        public string GetSolution(int partId)
        {
            if (partId == 1)
            {
                var uncompressed = new StringBuilder();
                for (var i = 0; i < Input.Length; i++)
                {
                    if (Input[i] != '(')
                    {
                        uncompressed.Append(Input[i]);
                    }
                    else
                    {
                        var marker = Input.Substring(i + 1, (Input.IndexOf(')', i) - (i + 1)));
                        var charLen = int.Parse(marker.Split('x')[0]);
                        var repeat = int.Parse(marker.Split('x')[1]);
                        var charsToRepeat = Input.Substring(Input.IndexOf(')', i) + 1, charLen);
                        for (var rept = 0; rept < repeat; rept++)
                        {
                            uncompressed.Append(charsToRepeat);
                        }
                        i = Input.IndexOf(')', i) + charLen;
                    }
                }

                Result = uncompressed.Length;
            }
            else
            {
                GetUncompressedDataLenth(Input);
                //var uncompressed = new StringBuilder();
                //for (var i = 0; i < Input.Length; i++)
                //{
                //    if (Input[i] != '(')
                //    {
                //        uncompressed.Append(Input[i]);
                //    }
                //    else
                //    {
                //        var marker = Input.Substring(i + 1, (Input.IndexOf(')', i) - (i + 1)));
                //        var charLen = int.Parse(marker.Split('x')[0]);
                //        var repeat = int.Parse(marker.Split('x')[1]);
                //        var charsToRepeat = Input.Substring(Input.IndexOf(')', i) + 1, charLen);
                //        var newText = new char[0];
                //        for (var rept = 0; rept < repeat; rept++)
                //        {
                //            newText = newText.Concat(charsToRepeat).ToArray();
                //        }
                //        var insertIndex = Input.IndexOf(')', i) + 1;
                //        Input = Input.Remove(insertIndex, charLen);
                //        Input = Input.Insert(insertIndex, new string(newText));
                //        i = insertIndex - 1;
                //    }
                //}

                Result = uncompLength;
            }
            return $"{Result}";
        }

        private void GetUncompressedDataLenth(string inputValue)
        {
            if (!inputValue.Contains("("))
            {
                uncompLength += inputValue.Length;
            }
            else
            {
                for (var i = 0; i < inputValue.Length; i++)
                {
                    if (inputValue[i] != '(')
                    {
                        uncompLength++;
                    }
                    else
                    {
                        var marker = inputValue.Substring(i + 1, (inputValue.IndexOf(')', i) - (i + 1)));
                        var charLen = int.Parse(marker.Split('x')[0]);
                        var repeat = int.Parse(marker.Split('x')[1]);
                        var charsToRepeat = inputValue.Substring(inputValue.IndexOf(')', i) + 1, charLen);
                        var newString = new StringBuilder();
                        if (charsToRepeat.Contains('('))
                        {
                            for (var rept = 0; rept < repeat; rept++)
                            {
                                newString.Append(charsToRepeat);
                            }
                            GetUncompressedDataLenth(newString.ToString());
                        }
                        else
                        {
                            uncompLength += charsToRepeat.Length * repeat;
                        }
                        i = inputValue.IndexOf(')', i) + charLen;
                    }
                }
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllText(file);
        }

    }
}
