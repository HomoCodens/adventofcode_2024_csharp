namespace AdventOfCode2024.Solvers.Day15;

internal record LanternfishWarehouse
{
    public required Point Robbit { get; init; }

    public required HashSet<Point> Walls { get; init; }

    public required HashSet<Point> Bokses { get; init; }
}