namespace AdventOfCode2024.Solvers.Day3;

internal sealed record MulInstruction
{
    public required int Factor1 { get; init; }
    public required int Factor2 { get; init; }
}