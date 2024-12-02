using AdventOfCode2024.Solvers.Day2;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day2;

public class Day2Tests
{
    private Day2Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day2Solver();
    }

    [Test]
    public void Part1_SolvesCorrectly()
    {
        var input = Input.GetInput(2, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(2);
    }

    [Test]
    public void Part2_SolvesCorrectly()
    {
        var input = Input.GetInput(2, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(4);
    }
}