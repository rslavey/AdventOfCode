using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.randyslavey.AdventOfCode
{
    class Day122015 : IAdventOfCodeSingleData
    {
        public int Result { get; set; }
        public string InputValue { get; set; }
        private static IEnumerable<char[]> charSetsOfThree { get; set; }
        public string GetSolution(int partId)
        {
            var itemValues = GetNumbers(JObject.Parse(InputValue).Children().ToList(), 0d, new List<JToken>());
            var nonRedChildren = GetNonRedNumbers(JObject.Parse(InputValue).Children().ToList(), 0d, new List<JToken>());
            Result = partId == 1 ?
                itemValues.Where(x => x.Type == JTokenType.Integer).Sum(x => (int)x) :
                nonRedChildren.Where(x => x.Type == JTokenType.Integer).Sum(x => (int)x) ;

            return $"{Result}";
        }

        private List<JToken> GetNumbers(List<JToken> json, double v, List<JToken> children)
        {
            foreach(var token in json)
            {
                if (token.HasValues)
                {
                    GetNumbers(token.Children().ToList(), v, children);
                }
                else
                {
                    children.Add(token);
                }
            }
            return children;
        }

        private List<JToken> GetNonRedNumbers(List<JToken> json, double v, List<JToken> children)
        {
            foreach (var token in json)
            {
                if (token.Children().Count() > 0)
                {
                    if (!(token.Children().SelectMany(x => x).Any(x => x.ToString() == "red" && x.Parent.Type != JTokenType.Array)))
                    {
                        GetNonRedNumbers(token.Children().ToList(), v, children);
                    }
                }
                else
                {
                    children.Add(token);
                }
            }
            return children;
        }

        public void GetInputData(string file)
        {
            InputValue = File.ReadAllLines(file)[0];
        }


    }
}

