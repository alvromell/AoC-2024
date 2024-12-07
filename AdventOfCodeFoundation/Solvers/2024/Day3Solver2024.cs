using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2024
{
    [Solves("2024/12/3")]
    internal class Day3Solver2024 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            /*
                Find all occurrences of "mul(X,Y)", where X and Y are one- to three digid numbers. Multiply these and sum the products.
                Option 1: Use Regex to match all occurrences: mul\((\d{1,3}),(\d{1,3})\)"
            */
            var rawInput = await input.GetRawInput();
            var sum = 0;
            foreach (Match regexMatch in matchMulExp(rawInput))
            {
                var product = extractAndMultiply(regexMatch);
                Console.WriteLine($"Found match: {regexMatch.ToString()}, with product {product}");
                sum += product;
            }
            //Console.WriteLine(rawInput.Length);
            return sum.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var rawInput = await input.GetRawInput();
            string enabledPattern = @"(^|do\(\)).*?($|don't\(\))";
            var sum = 0;
            var enabled = Regex.Matches(rawInput, enabledPattern, RegexOptions.Singleline);
            foreach (Match enabledInstruction in enabled)
            {
                foreach (Match mulMatch in matchMulExp(enabledInstruction.ToString()))
                {
                    var product = extractAndMultiply(mulMatch);
                    Console.WriteLine($"Found match: {mulMatch}, with product {product}");
                    sum += product;
                }
            }
            //Console.WriteLine(rawInput.Length);
            return sum.ToString();
        }

        private MatchCollection matchMulExp(string inp)
        {
            string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            return Regex.Matches(inp, mulPattern);
        }
        private int extractAndMultiply(Match mulExpGroup)
        {
            return int.Parse(mulExpGroup.Groups[1].Value) * int.Parse((mulExpGroup.Groups[2].Value));
        }
    }
}
