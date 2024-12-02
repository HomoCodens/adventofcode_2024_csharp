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

    [Test]
    public void Part2_HandlesZeroDiffs()
    {
        var input = new string[]{"8 6 4 4 2"};
        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(1);
    }

    [Test]
    public void Part2_DecreasingDampen()
    {
        var input = new string[]{"5 4 2 3 1"};
        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(1);
    }

    [Test]
    public void Part2_HandlesRemovalOfFirstElement()
    {
        var input = new string[]{"17 16 18 19 22 23"};
        var solution = this.solver.Solve(input);

        solution.SolutionPart2.Should().Be(1);
    }
}