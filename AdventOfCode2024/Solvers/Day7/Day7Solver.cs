using System.Numerics;

namespace AdventOfCode2024.Solvers.Day7;

internal class Day7Solver : SolverBase<CalibrationEquation[]>
{
    protected override ulong SolvePart1(CalibrationEquation[] input)
    {
        var staufs = input.Where(equation => IsSolveABall(equation.TestValue, equation.Operands)).Select(equation => equation.TestValue).ToArray();

        var acc = 0UL;
        var overflowed = false;
        foreach (var stauf in staufs)
        {
            if (acc + stauf < acc)
            {
                overflowed = true;
                break;
            }

            acc += stauf;
        }
        return staufs.Aggregate((a, b) =>
        {
            var res = a + b;
            Console.WriteLine(res);
            return res;
        });
    }

    protected override ulong SolvePart2(CalibrationEquation[] input)
    {
        throw new NotImplementedException();
    }

    internal override CalibrationEquation[] ParseInput(string[] lines)
    {
        return lines.Select(l =>
        {
            var parts = l.Split(":");

            return new CalibrationEquation()
            {
                TestValue = ulong.Parse(parts[0]),
                Operands = parts[1].Trim().Split(" ").Select(ulong.Parse).ToArray(),
            };
        }).ToArray();
    }

    private bool IsSolveABall(ulong testValue, ulong[] operands, ulong akkumulator = 0)
    {
        if (operands is [])
        {
            return akkumulator == testValue;
        }

        if (akkumulator > testValue)
        {
            return false;
        }

        var (operand, rest) = operands;

        var plused = operand + akkumulator;
        var multed = operand * akkumulator;

        if (plused < akkumulator)
        {
            throw new Exception("This is not a valid solution");
        }

        if (multed < akkumulator)
        {
            throw new Exception("This is not a valid solution");
        }

        return IsSolveABall(testValue, rest, akkumulator + operand) || IsSolveABall(testValue, rest, akkumulator * operand);
    }
}
