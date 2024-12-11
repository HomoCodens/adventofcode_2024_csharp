using AdventOfCode2024.Solvers.Day9;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day9;

public class Day9Tests
{
    private Day9Solver solver = null!;

    [SetUp]
    public void Setup()
    {
        this.solver = new Day9Solver();
    }

    [Test]
    public void ParseInput()
    {
        var input = new string[] { "12345" };
        var result = this.solver.ParseInput(input);

        result.Should().BeEquivalentTo([
            new Block() { FileId = 0, Index = 0 },
            new Block() { FileId = 1, Index = 3 },
            new Block() { FileId = 1, Index = 4 },
            new Block() { FileId = 1, Index = 5 },
            new Block() { FileId = 2, Index = 10 },
            new Block() { FileId = 2, Index = 11 },
            new Block() { FileId = 2, Index = 12 },
            new Block() { FileId = 2, Index = 13 },
            new Block() { FileId = 2, Index = 14 },
        ]);
    }

    [Test]
    public void SolvePart1()
    {
        var input = Input.GetInput(9, "example1.txt");
        var result = this.solver.Solve(input);

        result.SolutionPart1.Should().Be(1928);
    }
}