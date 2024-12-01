using AdventOfCode2024.Solvers.Day1;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day1;

public class Day1Tests
{
    private Day1Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day1Solver();
    }

    [Test]
    public void Part1_SolvesCorrectly()
    {
        var input = Input.GetInput(1, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(11);
    }

    [Test]
    public void Part2_SolvesCorrectly()
    {
        var input = Input.GetInput(1, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(31);
    }
}