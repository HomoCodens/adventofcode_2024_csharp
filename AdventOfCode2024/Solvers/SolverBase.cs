using AdventOfCode2024.Solvers.Interface;

namespace AdventOfCode2024.Solvers;

internal abstract class SolverBase<TInput> : ISolver
{
    public DayResult Solve(string[] lines)
    {
        var input = this.ParseInput(lines);

        var solutionPart1 = 0ul;
        var solutionPart2 = 0ul;

        try
        {
            solutionPart1 = this.SolvePart1(input);
        }
        catch (NotImplementedException)
        {
        }

        try
        {
            solutionPart2 = this.SolvePart2(input);
        }
        catch (NotImplementedException)
        {
        }

        return new()
        {
            SolutionPart1 = solutionPart1,
            SolutionPart2 = solutionPart2,
        };
    }

    internal abstract TInput ParseInput(string[] lines);

    protected abstract ulong SolvePart1(TInput input);

    protected abstract ulong SolvePart2(TInput input);
}