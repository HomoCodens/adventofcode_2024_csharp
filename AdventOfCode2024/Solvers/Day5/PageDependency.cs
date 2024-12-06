namespace AdventOfCode2024.Solvers.Day5;

internal record PageDependency
{
    public int Page { get; init; }
    public int Dependency { get; init; }
}