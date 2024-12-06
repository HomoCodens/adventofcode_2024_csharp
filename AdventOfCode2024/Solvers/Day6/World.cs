namespace AdventOfCode2024.Solvers.Day6;

internal record World
{
    public required GuardState GuardState { get; init; }

    public required FloorType[][] Warehouse { get; init; }
}