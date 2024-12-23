using AdventOfCode2024.Solvers.Day15;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day15;

public class Day15Tests
{
    private Day15Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day15Solver();
    }

    [Test]
    public void Example1_SolvesPart1()
    {
        var input = Input.GetInput(15, "example1.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(2028);
    }

    [Test]
    public void Example2_SolvesPart1()
    {
        var input = Input.GetInput(15, "example2.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(10092);
    }

    [Test]
    public void Example2_SolvesPart2()
    {
        var input = Input.GetInput(15, "example2.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(9021);
    }
}