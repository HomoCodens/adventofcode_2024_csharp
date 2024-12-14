using AdventOfCode2024.Solvers.Day12;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day12;

public class Day12SolverTests
{
    private Day12Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day12Solver();
    }

    [Test]
    public void TwoByTwoFarm()
    {
        var input = new string[] {
            "AA",
            "AA"
        };

        var result = solver.Solve(input);

        result.SolutionPart1.Should().Be(32);
    }

    [Test]
    public void Example1_SolvesPart1()
    {
        var input = Input.GetInput(12, "example1.txt");

        var result = solver.Solve(input);

        result.SolutionPart1.Should().Be(140);
    }

    [Test]
    public void Example2_SolvesPart1()
    {
        var input = Input.GetInput(12, "example2.txt");

        var result = solver.Solve(input);

        result.SolutionPart1.Should().Be(772);
    }

    [Test]
    public void Example3_SolvesPart1()
    {
        var input = Input.GetInput(12, "example3.txt");

        var result = solver.Solve(input);

        result.SolutionPart1.Should().Be(1930);
    }

    [Test]
    public void Example1_SolvesPart2()
    {
        var input = Input.GetInput(12, "example1.txt");

        var result = solver.Solve(input);

        result.SolutionPart2.Should().Be(80);
    }

    [Test]
    public void Example2_SolvesPart2()
    {
        var input = Input.GetInput(12, "example2.txt");

        var result = solver.Solve(input);

        result.SolutionPart2.Should().Be(436);
    }

    [Test]
    public void Example3_SolvesPart2()
    {
        var input = Input.GetInput(12, "example3.txt");

        var result = solver.Solve(input);

        result.SolutionPart2.Should().Be(1206);
    }

    [Test]
    public void Example4_SolvesPart2()
    {
        var input = Input.GetInput(12, "example4.txt");

        var result = solver.Solve(input);

        result.SolutionPart2.Should().Be(236);
    }

    [Test]
    public void Example5_SolvesPart2()
    {
        var input = Input.GetInput(12, "example5.txt");

        var result = solver.Solve(input);

        result.SolutionPart2.Should().Be(368);
    }
}