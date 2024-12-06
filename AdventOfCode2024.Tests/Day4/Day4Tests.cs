using AdventOfCode2024.Solvers.Day4;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day4;

public class Day4Tests
{
    private Day4Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day4Solver();
    }

    [Test]
    public void Part1_SolvesCorrectly()
    {
        var input = Input.GetInput(4, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(18);
    }

    [Test]
    public void Part2_SolvesCorrectly()
    {
        var input = Input.GetInput(4, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(9);
    }
}