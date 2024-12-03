using System.Collections;
using AdventOfCode2024.Solvers.Day1;

namespace AdventOfCode2024.Solvers.Day1;

internal class Day1Solver : SolverBase<LocationIdLists>
{
    internal override LocationIdLists ParseInput(string[] lines)
    {
        var coordinates = lines.Select(l =>
        {
            var parts = l.Split("   ");
            return (
                int.Parse(parts[0]),
                int.Parse(parts[1])
            );
        });

        return new()
        {
            LeftList = coordinates.Select(x => x.Item1),
            RightList = coordinates.Select(x => x.Item2),
        };
    }

    protected override int SolvePart1(LocationIdLists input)
    {
        return input.LeftList.Order().Zip(input.RightList.Order()).Select((pair) => Math.Abs(pair.First - pair.Second)).Sum();
    }

    protected override int SolvePart2(LocationIdLists input)
    {
        var rightCounts = input.RightList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        return input.LeftList.Select(l => l * (rightCounts.TryGetValue(l, out int value) ? value : 0)).Sum();
    }
}