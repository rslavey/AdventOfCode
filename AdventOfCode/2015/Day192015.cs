using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day192015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        public List<(string mol, string r)> FormattedInputValues = new List<(string mol, string r)>();
        public string Molecule;
        public HashSet<string> ResultingMols = new HashSet<string>();
        public string GetSolution(int partId)
        {
            if (partId == 1)
            {
                foreach(var (mol, r) in FormattedInputValues)
                {
                    for (var i = 0; i < Molecule.Length; i++)
                    {
                        if (Molecule[i].ToString() == mol)
                        {
                            var newMol = Molecule.Remove(i, 1).Insert(i, r);
                            if (!ResultingMols.Contains(newMol))
                            {
                                ResultingMols.Add(newMol);
                            }
                        }
                        if (i < Molecule.Length - 1 && Molecule.Substring(i,2) == mol){
                            var newMol = Molecule.Remove(i, 2).Insert(i, r);
                            if (!ResultingMols.Contains(newMol))
                            {
                                ResultingMols.Add(newMol);
                            }
                        }
                    }
                }
                Result = ResultingMols.Count();
            }
            else
            {
                var count = 0;
                while (Molecule != "e")
                {
                    foreach(var mol in FormattedInputValues.OrderByDescending(x => x.r.Length))
                    {
                        if (mol.r == Molecule)
                        {
                            Result = count++;
                            Molecule = mol.mol;
                            break;
                        }
                        else
                        {
                            for (var i = 0; i < Molecule.Length - mol.r.Length; i++)
                            {
                                if (Molecule.Substring(i, mol.r.Length) == mol.r)
                                {
                                    Molecule = Molecule.Remove(i, mol.r.Length).Insert(i, mol.mol);
                                    count++;
                                }
                            }

                        }
                    }
                }
            }

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            var r = new Regex(@"^(.*) => (.*)$");
            foreach(var line in File.ReadLines(file))
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }
                FormattedInputValues.Add((r.Match(line).Groups[1].Value, r.Match(line).Groups[2].Value));
            }
            Molecule = File.ReadLines(file).Last();
        }

    }
}
