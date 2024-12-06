using AdventOfCode2024.Solvers.Interface;

namespace AdventOfCode2024.Solvers;

internal abstract class SolverBase<TInput> : ISolver
{
    public DayResult Solve(string[] lines)
    {
        var input = this.ParseInput(lines);

        var solutionPart1 = 0;
        var solutionPart2 = 0;

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

    protected abstract int SolvePart1(TInput input);

    protected abstract int SolvePart2(TInput input);
}