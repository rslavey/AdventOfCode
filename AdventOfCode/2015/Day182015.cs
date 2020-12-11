using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace com.randyslavey.AdventOfCode
{
    class Day182015 : IAdventOfCodeUngroupedData
    {
        public int Result { get; set; }
        public string[] InputValues { get; set; }
        public bool[][] FormattedInputValues { get; set; }

        public string GetSolution(int partId)
        {
            var steps = 100;
            var gridSize = 100;
            bool[,] f = JaggedToMultidimensional(FormattedInputValues);
            if (partId == 2)
            {
                f[0, 0] = true;
                f[0, 99] = true;
                f[99, 0] = true;
                f[99, 99] = true;
            }
            for (var i = 0; i < steps; i++)
            {
                bool[,] n = new bool[gridSize, gridSize];
                if (partId == 2)
                {
                    n[0, 0] = true;
                    n[0, 99] = true;
                    n[99, 0] = true;
                    n[99, 99] = true;
                }

                for (var xdir = 0; xdir < gridSize; xdir++)
                {
                    for (var ydir = 0; ydir < gridSize; ydir++)
                    {
                        if (partId == 2 && ((xdir == 0 && ydir == 0) || (xdir == 0 && ydir == 99) || (xdir == 99 && ydir == 0) || (xdir == 99 && ydir == 99)))
                        {
                            n[xdir, ydir] = true;
                            continue;
                        }
                        var lightCount = 0;
                        for (var xmove = xdir - 1; xmove <= xdir + 1; xmove++)
                        {
                            for (var ymove = ydir - 1; ymove <= ydir + 1; ymove++)
                            {
                                if (xmove >= 0 && ymove >= 0 && xmove < gridSize && ymove < gridSize && !(ymove == ydir && xmove == xdir))
                                {
                                    lightCount += f[xmove,ymove] ? 1 : 0;
                                }
                            }
                        }
                        n[xdir,ydir] = (f[xdir,ydir] ? lightCount == 2 | lightCount == 3 : lightCount == 3);
                    }

                }
                f = (bool[,])n.Clone();
            }

            var count = 0;
            foreach(var l1 in f)
            {
                count += l1 ? 1 : 0;
            }

            Result = partId == 1 ?
                count :
                count;

            return $"{Result}";
        }

        public void GetInputData(string file)
        {
            FormattedInputValues = File.ReadAllLines(file).Select(x => x.ToCharArray().Select(xx => xx == '#').ToArray()).ToArray();
        }

        public T[,] JaggedToMultidimensional<T>(T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                cols = jaggedArray[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }

    }
}
