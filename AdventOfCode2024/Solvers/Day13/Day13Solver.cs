using System.Text.RegularExpressions;
using AdventOfCode2024.Solvers.Day6;

namespace AdventOfCode2024.Solvers.Day13;

internal partial class Day13Solver : SolverBase<ClawMasheen[]>
{
    protected override ulong SolvePart1(ClawMasheen[] input)
    {
        return Solve(input, false);
    }

    protected override ulong SolvePart2(ClawMasheen[] input)
    {
        return Solve(input, true);
    }

    internal override ClawMasheen[] ParseInput(string[] lines)
    {
        return lines.Chunk(4).Select(ParseMasheen).ToArray();
    }

    private ulong Solve(ClawMasheen[] masheens, bool addALot)
    {
        var toAdd = 10_000_000_000_000L;
        var solvutions = masheens
                        .Select(m => m with
                        {
                            Prize = m.Prize with
                            {
                                X = m.Prize.X + (addALot ? toAdd : 0),
                                Y = m.Prize.Y + (addALot ? toAdd : 0),
                            }
                        })
                        .Select(m => SolveSystem(m.AButton, m.BButton, m.Prize))
                        .Where(s => Math.Floor(s.AButtonPresses) == s.AButtonPresses && Math.Floor(s.BButtonPresses) == s.BButtonPresses)
                        .Where(s => s.AButtonPresses > 0 && s.BButtonPresses > 0 && (addALot || (s.AButtonPresses <= 100 && s.BButtonPresses <= 100)))
                        .ToArray();

        return solvutions.Aggregate(0ul, (acc, s) => acc + 3ul * (ulong)s.AButtonPresses + (ulong)s.BButtonPresses);
    }

    private ClawMasheen ParseMasheen(string[] lines)
    {
        var bttnAPattern = AButtonRegex();
        var bttnBPattern = AnotherButtonRegex();
        var prizePattern = PrizeRegex();

        var aMatches = bttnAPattern.Match(lines[0]);
        var bMatches = bttnBPattern.Match(lines[1]);
        var prizeMatches = prizePattern.Match(lines[2]);

        return new ClawMasheen()
        {
            AButton = new LongVector()
            {
                X = int.Parse(aMatches.Groups["dx"].Value),
                Y = int.Parse(aMatches.Groups["dy"].Value)
            },
            BButton = new LongVector()
            {
                X = int.Parse(bMatches.Groups["dx"].Value),
                Y = int.Parse(bMatches.Groups["dy"].Value)
            },
            Prize = new LongVector()
            {
                X = int.Parse(prizeMatches.Groups["x"].Value),
                Y = int.Parse(prizeMatches.Groups["y"].Value)
            },

        };
    }

    private (double AButtonPresses, double BButtonPresses) SolveSystem(LongVector a, LongVector b, LongVector y)
    {
        var determinant = a.X * b.Y - a.Y * b.X;
        if (determinant == 0)
        {
            throw new Exception("dinnae expect that");
        }

        var solvution = (
            (b.Y * y.X - b.X * y.Y) / (double)determinant,
            (-a.Y * y.X + a.X * y.Y) / (double)determinant
        );

        return solvution;
    }

    [GeneratedRegex(@"Button B: X\+(?<dx>\d+), Y\+(?<dy>\d+)")]
    private static partial Regex AnotherButtonRegex();

    [GeneratedRegex(@"Button A: X\+(?<dx>\d+), Y\+(?<dy>\d+)")]
    private static partial Regex AButtonRegex();

    [GeneratedRegex(@"Prize: X=(?<x>\d+), Y=(?<y>\d+)")]
    private static partial Regex PrizeRegex();
}