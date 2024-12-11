using AdventOfCode2024.Solvers.Day10;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day10;

public class Day10Tests
{
    private Day10Solver solver = null!;

    [SetUp]
    public void Setup()
    {
        this.solver = new Day10Solver();
    }

    [Test]
    public void SolvePart1_Example1()
    {
        var input = Input.GetInput(10, "example1.txt");

        var result = this.solver.Solve(input);

        result.SolutionPart1.Should().Be(1);
    }

    [Test]
    public void SolvePart1_Example2()
    {
        var input = Input.GetInput(10, "example2.txt");

        var result = this.solver.Solve(input);

        result.SolutionPart1.Should().Be(36);
    }
}