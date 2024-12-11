using AdventOfCode2024.Solvers.Day11;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day11;

public class Day11Tests
{
    private Day11Solver Solver = new();

    [SetUp]
    public void Setup()
    {
        this.Solver = new Day11Solver();
    }

    [Test]
    public void SolvesPart1()
    {
        var input = Input.GetInput(11, "example1.txt");

        var result = this.Solver.Solve(input);

        result.SolutionPart1.Should().Be(55312);
    }
}