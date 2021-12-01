using System.Collections.Generic;

namespace com.randyslavey.AdventOfCode
{
    public interface IAdventOfCode
    {
        string GetSolution(int partId);
        void GetInputData(string file);
    }
    public interface IAdventOfCodeData<T> : IAdventOfCode
    {
        T Input { get; set; }
    }
}
