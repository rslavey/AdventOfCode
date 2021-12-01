using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace com.randyslavey.AdventOfCode
{
    class Day222015 : IAdventOfCodeData<string[]>
    {
		//NOTE: I quit on part 2. It ran fine on part 1, but it never found an answer on part 2, and I didn't want to write an entire game. This solution belongs to https://gist.github.com/markheath/7e3fa33f54f012136083#file-advent-of-code-day-22-cs
		public int Result { get; set; }
        public string[] Input { get; set; }

        public string GetSolution(int partId)
        {
			var chooser = new SpellChooser();
			var win = false;
			while (!win)
            {
				var player = new PlayerStatus(50, 0, 0, 500);
				var boss = new PlayerStatus(71, 10, 0, 500);
				win = Battle(player, boss, chooser, true);
            }

            return $"{chooser.bestWinningAmount}";
        }

        public void GetInputData(string file)
        {
            Input = File.ReadAllLines(file);
        }

		class QueueSpellChooser : ISpellChooser
		{
			private Queue<Spell> spellQueue;
			public QueueSpellChooser(IEnumerable<Spell> spells)
			{
				spellQueue = new Queue<Spell>(spells);
			}
			public Spell GetNextSpell(PlayerStatus status)
			{
				return spellQueue.Dequeue();
			}
			public void WinningAmount(int amount)
			{
			}
			public void NewBattle()
			{
			}
		}

		interface ISpellChooser
		{
			void NewBattle();
			Spell GetNextSpell(PlayerStatus status);
			void WinningAmount(int amount);
		}

		class SpellChooser : ISpellChooser
		{
			public int bestWinningAmount = Int32.MaxValue;
			public List<string> choices = new List<string>();
			private Random rand = new Random();
			public Spell GetNextSpell(PlayerStatus status)
			{
				var options = spells.Where(s => s.Cost <= status.Mana &&
					s.Cost + status.ManaSpent < bestWinningAmount &&
					!status.IsSpellActive(s.Name)).ToList();
				if (options.Count == 0) return null;

				var t = options[rand.Next(options.Count)].GetType();

				var spell = (Spell)Activator.CreateInstance(t);
				choices.Add(spell.Name);
				return spell;
			}
			public void NewBattle()
			{
				choices.Clear();
			}
			public void WinningAmount(int amount)
			{
				if (amount < bestWinningAmount)
				{
					Console.WriteLine("NEW BEST AMOUNT {0}", amount);
					foreach (var choice in choices)
						Console.WriteLine(choice);
					bestWinningAmount = amount;
				}
			}
		}


		abstract class Spell
		{
			public Spell(string name, int cost)
			{
				Name = name;
				Cost = cost;
			}
			public int Cost { get; }
			public string Name { get; }
			public abstract void Use(PlayerStatus self, PlayerStatus opponent, bool debug);
			public abstract int Timer { get; }
		}

		class MagicMissile : Spell
		{
			public MagicMissile() : base("Magic Missile", 53) { }
			public override void Use(PlayerStatus self, PlayerStatus opponent, bool debug)
			{
				if (debug) Console.WriteLine("Magic Missile deals 4 damage");
				opponent.AddHitPoints(-4);
			}
			public override int Timer { get { return 0; } }
		}

		class Drain : Spell
		{
			public Drain() : base("Drain", 73) { }
			public override void Use(PlayerStatus self, PlayerStatus opponent, bool debug)
			{
				if (debug) Console.WriteLine("Player casts Drain, dealing 2 damage, and healing 2 hit points");
				self.AddHitPoints(2);
				opponent.AddHitPoints(-2);
			}
			public override int Timer { get { return 0; } }
		}

		class Shield : Spell
		{
			public Shield() : base("Shield", 113) { }
			private int timer = 6;
			public override void Use(PlayerStatus self, PlayerStatus opponent, bool debug)
			{
				if (timer == 6)
				{
					if (debug) Console.WriteLine("Shield increases armor by 7");
					self.AddArmor(7);
				}
				if (timer == 1)
				{
					if (debug) Console.WriteLine("Shield wears off, decreasing armor by 7");
					self.AddArmor(-7);
				}
				timer--;
			}
			public override int Timer { get { return timer; } }
		}

		class Poison : Spell
		{
			public Poison() : base("Poison", 173) { }
			private int timer = 6;
			public override void Use(PlayerStatus self, PlayerStatus opponent, bool debug)
			{
				if (debug) Console.WriteLine("Poison deals 3 damage");
				opponent.AddHitPoints(-3);
				timer--;
			}
			public override int Timer { get { return timer; } }
		}

		class Recharge : Spell
		{
			public Recharge() : base("Recharge", 229) { }
			private int timer = 5;
			public override void Use(PlayerStatus self, PlayerStatus opponent, bool debug = true)
			{
				if (debug) Console.WriteLine("Recharge increases mana by 101");
				self.AddMana(101);
				timer--;
			}
			public override int Timer { get { return timer; } }
		}

		static List<Spell> spells = new List<Spell>()
{
	new MagicMissile(),
	new Drain(),
	new Shield(),
	new Recharge(),
	new Poison(),
};

		bool Battle(PlayerStatus player, PlayerStatus boss, ISpellChooser spellChooser, bool hard, bool debug = false)
		{
			spellChooser.NewBattle();
			while (player.HitPoints > 0 && boss.HitPoints > 0)
			{
				if (debug)
				{
					Console.WriteLine("-- Player turn --");
					Console.WriteLine($"Player has {player.HitPoints} hit points, {player.Armor} armor, {player.Mana} mana");
					Console.WriteLine($"Boss has {boss.HitPoints} hit points");
				}
				if (hard)
				{
					player.AddHitPoints(-1);
					if (player.HitPoints <= 0) return false;
				}
				player.UseSpells(boss, debug);
				var spell = spellChooser.GetNextSpell(player);
				// can't afford any more spells (or this battle won't be cheapest)
				if (spell == null) return false;

				if (debug) Console.WriteLine($"Player casts {spell.Name}");
				player.AddSpell(spell, boss, debug);

				if (debug)
				{
					Console.WriteLine("-- Boss turn --");
					Console.WriteLine($"Player has {player.HitPoints} hit points, {player.Armor} armor, {player.Mana} mana");
					Console.WriteLine($"Boss has {boss.HitPoints} hit points");
				}
				player.UseSpells(boss, debug);
				if (boss.HitPoints > 0) player.HitBy(boss, debug);
			}

			var playerWins = player.HitPoints > 0;
			if (playerWins) spellChooser.WinningAmount(player.ManaSpent);
			return playerWins;
		}

		class PlayerStatus
		{
			private List<Spell> activeSpells = new List<Spell>();
			public PlayerStatus(int hp, int d, int a, int m)
			{
				HitPoints = hp;
				Damage = d;
				Armor = a;
				Mana = m;
			}
			public int HitPoints { get; private set; }
			public int Damage { get; private set; }
			public int Armor { get; private set; }
			public int Mana { get; private set; }
			public int ManaSpent { get; private set; }

			public void AddHitPoints(int hitPoints)
			{
				HitPoints += hitPoints;
			}

			public void AddArmor(int armor)
			{
				Armor += armor;
			}

			public void AddMana(int mana)
			{
				Mana += mana;
			}

			public void AddSpell(Spell spell, PlayerStatus opponent, bool debug = false)
			{
				Mana -= spell.Cost;
				ManaSpent += spell.Cost;
				if (Mana < 0) throw new ArgumentException("Can't afford this spell");
				if (spell.Timer == 0)
				{
					spell.Use(this, opponent, debug);
				}
				else
				{
					activeSpells.Add(spell);
				}
			}

			public void UseSpells(PlayerStatus boss, bool debug = false)
			{
				foreach (var spell in activeSpells)
				{
					if (boss.HitPoints > 0)
					{
						spell.Use(this, boss, debug);
						if (debug)
							Console.WriteLine($"{spell.Name}'s timer is now {spell.Timer}");
					}
				}
				activeSpells.RemoveAll(s => s.Timer == 0);
			}

			public void HitBy(PlayerStatus boss, bool debug = false)
			{
				var bossDamage = Math.Max(1, boss.Damage - Armor);
				if (debug) Console.WriteLine($"Boss attacks for {bossDamage} damage");
				HitPoints -= bossDamage;
			}

			public bool IsSpellActive(string spellName)
			{
				return activeSpells.Any(s => s.Name == spellName);
			}
		}

	}
}

