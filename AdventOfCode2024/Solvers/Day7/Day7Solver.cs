namespace AdventOfCode2024.Solvers.Day7;

internal class Day7Solver : SolverBase<CalibrationEquation[]>
{
    protected override ulong SolvePart1(CalibrationEquation[] input)
    {
        var staufs = input.Where(equation => IsSolveABall(equation.TestValue, equation.Operands, false)).Select(equation => equation.TestValue).ToArray();

        return staufs.Aggregate((a, b) => a + b);
    }

    protected override ulong SolvePart2(CalibrationEquation[] input)
    {
        var staufs = input.Where(equation => IsSolveABall(equation.TestValue, equation.Operands, true)).Select(equation => equation.TestValue).ToArray();

        return staufs.Aggregate((a, b) => a + b);
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

    internal static ulong Concat(ulong a, ulong b)
    {
        var nDigitsB = Math.Floor(Math.Log10(b)) + 1;

        return (ulong)Math.Pow(10, nDigitsB) * a + b;
    }

    private static bool IsSolveABall(ulong testValue, ulong[] operands, bool allowConcat, ulong akkumulator = 0)
    {
        if (akkumulator == 0)
        {
            (var firstOperand, operands) = operands;
            akkumulator = firstOperand;
        }

        if (operands is [])
        {
            return akkumulator == testValue;
        }

        if (akkumulator > testValue)
        {
            return false;
        }

        var (operand, rest) = operands;

        return IsSolveABall(testValue, rest, allowConcat, akkumulator + operand) ||
                IsSolveABall(testValue, rest, allowConcat, akkumulator * operand) ||
                (allowConcat && IsSolveABall(testValue, rest, allowConcat, Concat(akkumulator, operand)));
    }
}
