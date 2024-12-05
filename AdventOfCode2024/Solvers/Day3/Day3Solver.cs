using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers.Day3;

internal class Day3Solver : SolverBase<NorthPoleTobogganRentalShopComputerInstruction[]>
{
    internal override NorthPoleTobogganRentalShopComputerInstruction[] ParseInput(string[] lines)
    {
        // var instructionPattern = new Regex(@"(?:(mul)\((\d{1,3}),(\d{1,3})\))|(?:(do)\(\))|(?:(don't)\(\))");
        var instructionPattern = new Regex(@"(mul|do|don't)\((?:(\d{1,3}),(\d{1,3})|)\)"); // still matches mul(), crossing fingers
        return lines.Select(line => instructionPattern.Matches(line))
                            .SelectMany(matches => matches.Select<Match, NorthPoleTobogganRentalShopComputerInstruction>(m => m.Groups[1].Value switch
                            {
                                "mul" => new MulInstruction()
                                {
                                    Factor1 = int.Parse(m.Groups[2].Value),
                                    Factor2 = int.Parse(m.Groups[3].Value)
                                },
                                "do" => new DoInstruction(),
                                "don't" => new DontInstruction(),
                                _ => throw new NotSupportedException(m.Groups[1].Value)
                            })).ToArray();
    }

    protected override int SolvePart1(NorthPoleTobogganRentalShopComputerInstruction[] input)
    {
        return input.Where(m => m is MulInstruction).Select(mul =>
        {
            var muul = (MulInstruction)mul;
            return muul.Factor1 * muul.Factor2;
        }).Sum();
    }

    protected override int SolvePart2(NorthPoleTobogganRentalShopComputerInstruction[] input)
    {
        return input.Aggregate((Sum: 0, Enabled: true), (acc, instruction) => instruction switch
        {
            MulInstruction mul => acc.Enabled ? (acc.Sum + mul.Factor1 * mul.Factor2, acc.Enabled) : acc,
            DoInstruction _ => (acc.Sum, true),
            DontInstruction _ => (acc.Sum, false),
            _ => throw new NotSupportedException(instruction.ToString())
        }).Sum;
    }
}