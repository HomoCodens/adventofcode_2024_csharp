using AdventOfCode2024.Solvers.Day13;
using AdventOfCode2024.Tests.Util;

namespace AdventOfCode2024.Tests.Day13;

public class Day13Tests
{
    private Day13Solver solver = null!;

    [SetUp]
    public void SetUp()
    {
        this.solver = new Day13Solver();
    }

    [Test]
    public void ParsesInputCorrectyl()
    {
        var input = Input.GetInput(13, "example1.txt");

        var parsed = this.solver.ParseInput(input);

        parsed.Should().BeEquivalentTo(
            [
                new ClawMasheen()
                {
                    AButton = new Solvers.Day6.Point()
                    {
                        X = 94,
                        Y = 34,
                    },
                    BButton = new Solvers.Day6.Point()
                    {
                        X = 22,
                        Y = 67,
                    },
                    Prize = new Solvers.Day6.Point()
                    {
                        X = 8400,
                        Y = 5400,
                    }
                },
                new ClawMasheen()
                {
                    AButton = new Solvers.Day6.Point()
                    {
                        X = 26,
                        Y = 66,
                    },
                    BButton = new Solvers.Day6.Point()
                    {
                        X = 67,
                        Y = 21,
                    },
                    Prize = new Solvers.Day6.Point()
                    {
                        X = 12748,
                        Y = 12176,
                    }
                },
                new ClawMasheen()
                {
                    AButton = new Solvers.Day6.Point()
                    {
                        X = 17,
                        Y = 86,
                    },
                    BButton = new Solvers.Day6.Point()
                    {
                        X = 84,
                        Y = 37,
                    },
                    Prize = new Solvers.Day6.Point()
                    {
                        X = 7870,
                        Y = 6450,
                    }
                },
                new ClawMasheen()
                {
                    AButton = new Solvers.Day6.Point()
                    {
                        X = 69,
                        Y = 23,
                    },
                    BButton = new Solvers.Day6.Point()
                    {
                        X = 27,
                        Y = 71,
                    },
                    Prize = new Solvers.Day6.Point()
                    {
                        X = 18641,
                        Y = 10279,
                    }
                },
            ]
        );
    }

    [Test]
    public void Example1_SolvesPart1()
    {
        var input = Input.GetInput(13, "example1.txt");

        var solution = this.solver.Solve(input);

        solution.SolutionPart1.Should().Be(480);
    }
}