namespace AdventOfCode2024.Solvers.Day1;

internal sealed record LocationIdLists
{
    public required IEnumerable<int> LeftList { get; init; }

    public required IEnumerable<int> RightList { get; init; }
}