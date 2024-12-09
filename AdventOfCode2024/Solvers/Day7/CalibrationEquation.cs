using System.Numerics;

namespace AdventOfCode2024.Solvers.Day7;

internal record CalibrationEquation
{
    public required ulong TestValue { get; init; }

    public required ulong[] Operands { get; init; }
}