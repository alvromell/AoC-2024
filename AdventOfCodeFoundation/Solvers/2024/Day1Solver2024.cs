using AdventOfCodeFoundation.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCodeFoundation.Solvers._2024
{
    [Solves("2024/12/1")]
    internal class Day1Solver2024 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            bool debug = false;
            var lines = await input.GetInputLines();
            var X = new List<int>(); // Left column values
            var Y = new List<int>(); // Right column values

            foreach (var line in lines) {
                X.Add(int.Parse(line.Split("   ")[0]));
                Y.Add(int.Parse(line.Split("   ")[1]));
            }
            X.Sort();
            Y.Sort();

            var sortedPairs = X.Zip(Y);
            var sum = 0;
            foreach (var (First, Second) in sortedPairs)
            {
                if(debug) Console.WriteLine($"{First}, {Second}");
                sum += Math.Abs(Second - First);
            }

            return sum.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var lines = await input.GetInputLines();

            // Count occurrences for each number in the right column, store in a dict
            // Loop through left column and multiply by occurences in the dict for that nr.

            var X = new List<int>(); // List for left column values
            var occurDict = new Dictionary<int, int>();

            foreach (var line in lines)
            {
                X.Add(int.Parse(line.Split("   ")[0]));
                var y = int.Parse(line.Split("   ")[1]);

                occurDict[y] = occurDict.ContainsKey(y) ? occurDict[y] + 1 : 1;
                
            }
            return X.Aggregate(0, (acc, curr) => acc + (curr * occurDict.GetValueOrDefault(curr,0))).ToString();
        }

    }
}
