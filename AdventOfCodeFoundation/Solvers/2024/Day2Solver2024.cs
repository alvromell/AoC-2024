using AdventOfCodeFoundation.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AdventOfCodeFoundation.Solvers._2024
{
    [Solves("2024/12/2")]
    internal class Day2Solver2024 : ISolver
    {
        private bool _debug = false;
        public async Task<string> SolvePartOne(Input input)
        {
            var lines = await input.GetInputLines();

            // For each line, check if it's an increasing or decreasing series, with a max difference of 3 between adjacent elements.
            // Store number of safe and unsafe rows in variables or array?

            var safeRows = 0;
            var unsafeRows = 0;

            foreach (var line in lines)
            {
                var lineArr = line.Split(' ').Select(c => int.Parse(c)).ToArray();
                if (determineIfSafeRow(lineArr).isSafe) safeRows++;
                else unsafeRows++;
            }
            if (_debug) Console.WriteLine($"safeRows: {safeRows}, unsafeRows: {unsafeRows}, totalRows:{lines.Count()}");

            return safeRows.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var lines = await input.GetInputLines();

            // For each line, check if it's an increasing or decreasing series, with a max difference of 3 between adjacent elements.

            var safeRows = 0;
            var unsafeRows = 0;

            foreach (var line in lines)
            {
                if (_debug) Console.WriteLine($"-- Original line: {line}");

                var lineArr = line.Split(' ').Select(c => int.Parse(c)).ToArray();
                var res = determineIfSafeRow(lineArr);
                if (res.isSafe) safeRows++;
                else if ((determineIfSafeRow(lineArr.Where((item, index) => index != res.unsafeIndex-1).ToList().ToArray()).isSafe) ||
                        (determineIfSafeRow(lineArr.Where((item, index) => index != res.unsafeIndex).ToList().ToArray()).isSafe) ||
                        (determineIfSafeRow(lineArr.Where((item, index) => index != res.unsafeIndex + 1).ToList().ToArray()).isSafe)) safeRows++;
                else unsafeRows++;
                if (_debug) Console.WriteLine($"----- Safe rows: {safeRows} -----");
            }
            if (_debug) Console.WriteLine($"safeRows: {safeRows}, unsafeRows: {unsafeRows}, totalRows:{lines.Count()}");

            return safeRows.ToString();
        }

        private (bool isSafe,int unsafeIndex) determineIfSafeRow(int[] lineArr)
        {
            bool asc = lineArr[1] > lineArr[0];
            bool desc = lineArr[1] < lineArr[0];

            if (!asc && !desc) { if (_debug) Console.WriteLine($"line: {string.Join(" ", lineArr)}, safe?: {false}"); return (false, 0); };
            bool isSafe = true;
            var unsafeChar = 0;
            if(_debug) Console.WriteLine($"Line: {string.Join(", ", lineArr)}, asc? {asc}, desc? {desc}");

            for (int i = 0; i < lineArr.Length - 1; i++)
            {
                if (asc && (lineArr[i + 1] <= lineArr[i] || lineArr[i + 1] > lineArr[i] + 3))
                {
                    isSafe = false;
                    unsafeChar = i;
                    if (_debug) Console.WriteLine($"Asc Unsafe! currentNr: {lineArr[i]}");
                    break;
                }
                else if (desc && (lineArr[i + 1] >= lineArr[i] || lineArr[i + 1] < lineArr[i] - 3))
                {
                    isSafe = false;
                    unsafeChar = i;
                    if (_debug) Console.WriteLine($"Desc Unsafe! currentNr: {lineArr[i]}");
                    break;
                }
            }
            if (_debug) Console.WriteLine($"line: {string.Join(" ", lineArr)}, safe?: {isSafe}");
            return (isSafe, unsafeChar);
        }

    }
}
