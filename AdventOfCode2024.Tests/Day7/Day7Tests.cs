using AdventOfCode2024.Solvers.Day7;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day7;

public class Day7Tests
{
    private Day7Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day7Solver();
    }

    [Test]
    public void ParseInput_ShouldParseInputCorrectly()
    {
        var input = new[]
        {
            "9: 1 2 3",
        };

        var parsed = new Day7Solver().ParseInput(input);

        parsed.Should().BeEquivalentTo([
            new CalibrationEquation
            {
                TestValue = 9,
                Operands = [1, 2, 3],
            }
        ]);
    }

    [Test]
    public void ArrayDeconstruction_Does()
    {
        var array = new ulong[] { 1, 2, 3 };

        var (first, rest) = array;

        first.Should().Be(1);
        rest.Should().BeEquivalentTo([2, 3]);
    }

    [Test]
    public void Part1_TheOneWithMulBeforePlus()
    {
        var input = new string[] { "7: 2 3 1" };

        var solution = solver.Solve(input);

        solution.SolutionPart1.Should().Be(7);
    }

    [Test]
    public void Part1_TheOneWithPlusBeforeMul()
    {
        var input = new string[] { "18: 2 4 3" };

        var solution = solver.Solve(input);

        solution.SolutionPart1.Should().Be(18);
    }

    [Test]
    public void ArrayDeconstruction_DoesWithOneElement()
    {
        var array = new ulong[] { 1 };

        var (first, rest) = array;

        first.Should().Be(1);
        rest.Should().BeEquivalentTo(new int[0]);
    }

    [Test]
    public void SolvePart1_ShouldSolvePart1Correctly()
    {
        var input = Input.GetInput(7, "example.txt");

        var result = this.solver.Solve(input);

        result.SolutionPart1.Should().Be(3749);
    }
}