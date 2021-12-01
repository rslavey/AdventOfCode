using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day252015 : IAdventOfCodeData<string>
    {
        public long Result { get; set; }
        public string Input { get; set; }

        public string GetSolution(int partId)
        {
            var col = 3019;
            var row = 3010;

            var iterations = 0;
            for (int i = 1; i <= col; i++)
            {
                iterations += i;
            }
            for (int i = 1; i < row; i++)
            {
                iterations += col + i - 1;
            }

            long code = 20151125;
            for (int i = 1; i < iterations; i++)
            {
                code = (code * 252533) % 33554393;
            }
            
            Result = partId == 1 ?
                code :
                1;

            return $"{Result}";
        }

        private void Fight(ref (int damage, int armor, int hp) myStats, ref (int damage, int armor, int hp) bossStats, bool myTurn = true)
        {
            if (myTurn)
            {
                bossStats.hp -= myStats.damage > bossStats.armor ? myStats.damage - bossStats.armor : 1;
            }
            else
            {
                myStats.hp -= bossStats.damage > myStats.armor ? bossStats.damage - myStats.armor : 1;
            }
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllText(file);
        }

    }
}
