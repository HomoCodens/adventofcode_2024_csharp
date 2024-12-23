namespace AdventOfCode2024.Solvers.Day15;

internal record Day15Input
{
    public required LanternfishWarehouse Warehouse { get; init; }

    public required Direction[] Moves { get; init; }
}