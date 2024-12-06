using AdventOfCode2024.Solvers.Day6;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day6;

public class Day6Tests
{
    private Day6Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day6Solver();
    }

    [Test]
    public void SolvesPart1Correctly()
    {
        var input = Input.GetInput(6, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(41);
    }

    [Test]
    public void SolvesPart2Correctly()
    {
        var input = Input.GetInput(6, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(6);
    }
}