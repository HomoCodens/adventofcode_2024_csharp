using System.Drawing;

namespace AdventOfCode2024.Solvers.Day8;

internal record City
{
    public required int Width { get; init; }

    public required int Height { get; init; }

    public required Dictionary<char, Point[]> Antennae { get; init; }
}