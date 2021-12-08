using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day042021 : IAdventOfCodeData<string[]>
    {
        public string Result { get; set; }
        public string[] Input { get; set; }
        public List<Card> Cards = new List<Card>();
        public List<int> Draws = new List<int>();
        public string GetSolution(int partId)
        {
            for (var i = 0; i < Draws.Count(); i++)
            {
                foreach (var card in Cards)
                {
                    var cs = card.Spots.FirstOrDefault(x => x.Value == Draws[i]);
                    if (cs != default)
                    {
                        cs.Covered = true;
                    }
                }
                foreach (var card in Cards)
                {
                    if (IsWinner(card) && (partId == 1 || card.WinRound == 0))
                    {
                        //Console.WriteLine(card.DrawBoard());
                        if (partId == 1)
                        {
                            return $"{card.GetScore(Draws[i])}";
                        }
                        else
                        {
                            card.WinRound = i;
                        }
                    }
                }
                if (partId == 2)
                {
                    if (Cards.Count(x => x.WinRound == 0) == 0)
                    {
                        return $"{Cards.OrderByDescending(x => x.WinRound).FirstOrDefault().GetScore(Draws[i])}";
                    }
                }
            }
            return $"{Result}";
        }

        private bool IsWinner(Card card)
        {
            var isWinner = false;
            var horWinner = card.Spots.Where(x => x.Covered).GroupBy(x => x.X).Select(g => new { 
                k = g.Key,
                count = g.Count()
            }).OrderByDescending(x => x.count).FirstOrDefault()?.count == 5;

            var verWinner = card.Spots.Where(x => x.Covered).GroupBy(x => x.Y).Select(g => new {
                k = g.Key,
                count = g.Count()
            }).OrderByDescending(x => x.count).FirstOrDefault()?.count == 5;

            //var diagDownWinner = card.Spots.Where(x => x.Covered && x.X == x.Y).Count() == 5;

            //var diagUpWinner = card.Spots.Where(x => x.Covered && x.X + x.Y == 4).Count() == 5;

            if (horWinner || verWinner)// || diagDownWinner || diagUpWinner)
            {
                isWinner = true;
            }
            return isWinner;
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
            Draws = Input[0].Split(',').Select(x => int.Parse(x)).ToList();
            Card c = new Card();
            var curCardRow = 0;
            for (var i = 1; i < Input.Length; i++)
            {
                if (Input[i] == string.Empty)
                {
                    if (c.Spots.Count() > 0)
                    {
                        Cards.Add(c);
                    }
                    c = new Card();
                    curCardRow = 0;
                    continue;
                }
                for (var ii = 0; ii < 5; ii++)
                {
                    var n = new string(Input[i].Skip(ii * 3).Take(3).ToArray());
                    var col = int.Parse(n.Trim());
                    c.Spots.Add(new CardSpot { X = ii, Y = curCardRow, Value = col, Covered = false });
                }
                curCardRow++;
            }
            Cards.Add(c);
        }

        internal class Card
        {
            internal List<CardSpot> Spots = new List<CardSpot>();
            internal int WinRound { get; set; }
            internal string DrawBoard()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < 5; i++)
                {
                    sb.AppendLine(string.Join(" ", Spots.Where(x => x.Y == i).OrderBy(x => x.X).Select(x => $"{(x.Covered ? "+" : string.Empty)}{x.Value.ToString().PadLeft(2, ' ')}").ToArray()));
                }
                return sb.ToString();
            }

            internal int GetScore(int winningNumber)
            {
                return Spots.Where(x => !x.Covered).Sum(x => x.Value) * winningNumber;
            }
        }
        internal class CardSpot
        {
            internal int X { get; set; }
            internal int Y { get; set; }
            internal int Value { get; set; }
            internal bool Covered { get; set; }
        }
    }
}
