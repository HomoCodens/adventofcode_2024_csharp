using AdventOfCode2024.Solvers.Day3;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day3;

public class Day3Tests
{
    private Day3Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day3Solver();
    }

    [Test]
    public void InputGetsParsedCorrectyl()
    {
        var input = Input.GetInput(3, "example.txt");

        var instructions = this.solver.ParseInput(input);

        instructions.Should().BeEquivalentTo(new MulInstruction[]
        {
            new() { Factor1 = 2, Factor2 = 4 },
            new() { Factor1 = 5, Factor2 = 5 },
            new() { Factor1 = 11, Factor2 = 8 },
            new() { Factor1 = 8, Factor2 = 5 },
        });
    }

    [Test]
    public void Part1_SolvesCorrectly()
    {
        var input = Input.GetInput(3, "example.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(161);
    }
}