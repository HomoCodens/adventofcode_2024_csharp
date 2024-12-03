using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers.Day3;

internal class Day3Solver : SolverBase<MulInstruction[]>
{
    internal override MulInstruction[] ParseInput(string[] lines)
    {
        var mulPattern = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        return lines.Select(line => mulPattern.Matches(line))
                            .SelectMany(matches => matches.Select(m => new MulInstruction()
                            {
                                Factor1 = int.Parse(m.Groups[1].Value),
                                Factor2 = int.Parse(m.Groups[2].Value)
                            })).ToArray();
    }

    protected override int SolvePart1(MulInstruction[] input)
    {
        return input.Select(mul => mul.Factor1 * mul.Factor2).Sum();
    }

    protected override int SolvePart2(MulInstruction[] input)
    {
        throw new NotImplementedException();
    }
}