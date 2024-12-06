using AdventOfCode2024.Solvers.Day5;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day5;

public class Day5Tests
{
    private Day5Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day5Solver();
    }

    [Test]
    public void SolvesPart1Correctly()
    {
        var input = Input.GetInput(5, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(143);
    }

    [Test]
    public void SolvesPart2Correctly()
    {
        var input = Input.GetInput(5, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(123);
    }
}