using AdventOfCode2024.Solvers.Day8;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day8;

public class Day8Tests
{
    private Day8Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day8Solver();
    }

    [Test]
    public void Part1_SolvesCorrectyl()
    {
        var input = Input.GetInput(8, "example0.txt");

        var solution = solver.Solve(input);

        solution.SolutionPart1.Should().Be(2);
    }

    [Test]
    public void Part1Example2_SolvesCorrectyl()
    {
        var input = Input.GetInput(8, "example1.txt");

        var solution = solver.Solve(input);

        solution.SolutionPart1.Should().Be(14);
    }

    [Test]
    public void Part2Example1_SolvesCorrectyl()
    {
        var input = Input.GetInput(8, "example1.txt");

        var solution = solver.Solve(input);

        solution.SolutionPart2.Should().Be(34);
    }

    [Test]
    public void Part1Example3_SolvesCorrectyl()
    {
        var input = Input.GetInput(8, "example2.txt");

        var solution = solver.Solve(input);

        solution.SolutionPart2.Should().Be(9);
    }
}