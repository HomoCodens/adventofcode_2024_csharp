namespace AdventOfCode2024.Solvers.Day5;

internal record PrinterSetup
{
    public required IEnumerable<PageDependency> Dependencies { get; init; }

    public required int[][] Jobs { get; init; }
}