using AdventOfCode2024.Solvers.Day14;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests;

public class Day14Tests
{
    private Day14Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new(11, 7);
    }

    [Test]
    public void Example1_SolvesPart1()
    {
        var input = Input.GetInput(14, "example1.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(12);
    }
}