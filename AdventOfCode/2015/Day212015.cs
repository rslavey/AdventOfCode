using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day212015 : IAdventOfCodeData<string[]>
    {
        public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
            var hp = int.Parse(Input[0].Split(':')[1].Trim());
            var d = int.Parse(Input[1].Split(':')[1].Trim());
            var a = int.Parse(Input[2].Split(':')[1].Trim());
            var myStartingHP = 100;
            var minCost = int.MaxValue;
            var maxCost = 0;

            List<(string name, int cost, int damage, int armor)> weapons = new List<(string name, int cost, int damage, int armor)>
            {
                ("Dagger",8,4,0),
                ("Shortsword",10,5,0),
                ("Warhammer",25,6,0),
                ("Longsword",40,7,0),
                ("Greataxe",74,8,0)
            };

            List<(string name, int cost, int damage, int armor)> armors = new List<(string name, int cost, int damage, int armor)>
            {
                ("None",0,0,0),
                ("Leather",13,0,1),
                ("Chainmail",31,0,2),
                ("Splintmail",53,0,3),
                ("Bandedmail",75,0,4),
                ("Platemail",102,0,5),
            };

            List<(string name, int cost, int damage, int armor)> rings = new List<(string name, int cost, int damage, int armor)>
            {
                ("Damage +1",25,1,0),
                ("Damage +2",50,2,0),
                ("Damage +3",100,3,0),
                ("Defense +1",20,0,1),
                ("Defense +2",40,0,2),
                ("Defense +3",80,0,3),
                ("None",0,0,0)
            };

            foreach (var weapon in weapons)
            {
                foreach (var armor in armors)
                {
                    var ringCombos = new Combinations<(string name, int cost, int damage, int armor)>(rings.ToList(), 2).ToList();
                    ringCombos.Add(
                        new List<(string name, int cost, int damage, int armor)>
                        {
                            ("None",0,0,0),
                            ("None",0,0,0)
                        }
                    );
                    foreach (var ring in ringCombos) {
                        var cost = weapon.cost + armor.cost + ring[0].cost + ring[1].cost;
                        (int damage, int armor, int hp) myStats = (
                            damage: weapon.damage + armor.damage + ring[0].damage + ring[1].damage,
                            armor: weapon.armor + armor.armor + ring[0].armor + ring[1].armor,
                            myStartingHP
                        );

                        (int damage, int armor, int hp) bossStats = (
                            damage: d,
                            armor: a,
                            hp: hp
                        );

                        var myTurn = true;
                        while (myStats.hp > 0 && bossStats.hp > 0)
                        {
                            Fight(ref myStats, ref bossStats, myTurn);
                            myTurn = !myTurn;
                        }

                        if (myStats.hp > 0)
                        {
                            minCost = cost < minCost ? cost : minCost;
                        }
                        else
                        {
                            maxCost = cost > maxCost ? cost : maxCost;
                        }
                        
                    }
                }
            }

            Result = partId == 1 ?
                minCost :
                maxCost;

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
            Input = File.ReadAllLines(file);
        }

    }
}
