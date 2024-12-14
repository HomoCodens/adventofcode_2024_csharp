using AdventOfCode2024.Solvers.Day6;

namespace AdventOfCode2024.Solvers.Day12;

internal record Region
{
    public required char Kind { get; init; }

    public required HashSet<Point> Plots { get; init; }

    public required int PerryMeter { get; init; }
}