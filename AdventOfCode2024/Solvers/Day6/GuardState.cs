namespace AdventOfCode2024.Solvers.Day6;

internal record GuardState
{
    public required Point Position { get; init; }

    public required Point Heading { get; init; }
}