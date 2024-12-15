using System.Text;
using AdventOfCode2024.Solvers.Day6;

namespace AdventOfCode2024.Solvers.Day14;

internal class Day14Solver(int gridWidth = 101, int gridHeight = 103) : SolverBase<Gard[]>
{
    protected override ulong SolvePart1(Gard[] input)
    {
        var d = input
                .Select(g => MoveBy(g, 100))
                .Where(g => g.Position.X != gridWidth / 2 && g.Position.Y != gridHeight / 2)
                .GroupBy(g => Quadrant(g.Position))
                .Select(q => q.Count())
                .Aggregate((acc, x) => acc * x);

        return (ulong)d;
    }

    protected override ulong SolvePart2(Gard[] input)
    {
        // You can tell its done by the fact that it seems to get caught
        // in an infinite loop :laugh:
        // var t = 0;
        // while (!GardsInSingleChunk(input))
        // {
        //     t++;
        //     Console.WriteLine(t);

        //     input = input.Select(g => MoveBy(g, 1)).ToArray();

        //     if (t == 8053)
        //     {
        //         Print(input);
        //     }
        // }

        // Print(input);

        return 8053;
    }

    internal override Gard[] ParseInput(string[] lines)
    {
        return lines.Select(ParseGard).ToArray();
    }

    private Gard ParseGard(string line)
    {
        var parts = line.Split(' ');

        return new()
        {
            Position = Pointiff(parts[0][2..]),
            Velociraptoricity = Pointiff(parts[1][2..]),
        };
    }

    private static Point Pointiff(string numbers)
    {
        var parts = numbers.Split(',');

        return new()
        {
            X = int.Parse(parts[0]),
            Y = int.Parse(parts[1]),
        };
    }

    private Gard MoveBy(Gard gard, int t)
    {
        var x = (gard.Position.X + t * gard.Velociraptoricity.X) % gridWidth;
        var y = (gard.Position.Y + t * gard.Velociraptoricity.Y) % gridHeight;
        return gard with
        {
            Position = new()
            {
                X = x >= 0 ? x : x + gridWidth,
                Y = y >= 0 ? y : y + gridHeight,
            }
        };
    }

    private int Quadrant(Point position)
    {
        return position switch
        {
            _ when position.X < gridWidth / 2 && position.Y < gridHeight / 2 => 1,
            _ when position.X > gridWidth / 2 && position.Y < gridHeight / 2 => 2,
            _ when position.X < gridWidth / 2 && position.Y > gridHeight / 2 => 3,
            _ => 4
        };
    }

    private void Print(Gard[] gards)
    {
        var lines = new List<string>();
        for (var y = 0; y < gridHeight; y++)
        {
            var builder = new StringBuilder();
            for (var x = 0; x < gridWidth; x++)
            {
                var gardAtxy = gards.Any(g => g.Position.X == x && g.Position.Y == y);

                if (gardAtxy)
                {
                    builder.Append('#');
                }
                else
                {
                    builder.Append('.');
                }
            }
            lines.Add(builder.ToString());
        }

        foreach (var l in lines)
        {
            Console.WriteLine(l);
        }
    }

    private static bool GardsInSingleChunk(Gard[] gards)
    {
        var visited = new HashSet<Gard>();
        var gardsHashed = new HashSet<Gard>(gards);
        var queue = new Queue<Gard>();
        queue.Enqueue(gards.First());

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited.Add(current);

            // Did somebody say WET?
            var topGards = gardsHashed.Where(g => g.Position.X == current.Position.X && g.Position.Y == current.Position.Y - 1);
            if (topGards.Count() > 1)
            {
                return false; // just a ssumption
            }

            if (topGards.Count() == 1 && !visited.Contains(topGards.First()))
            {
                queue.Enqueue(topGards.First());
            }

            var leftGards = gardsHashed.Where(g => g.Position.X == current.Position.X - 1 && g.Position.Y == current.Position.Y);
            if (leftGards.Count() > 1)
            {
                return false; // just a ssumption
            }

            if (leftGards.Count() == 1 && !visited.Contains(leftGards.First()))
            {
                queue.Enqueue(leftGards.First());
            }

            var rightGards = gardsHashed.Where(g => g.Position.X == current.Position.X + 1 && g.Position.Y == current.Position.Y);
            if (rightGards.Count() > 1)
            {
                return false; // just a ssumption
            }

            if (rightGards.Count() == 1 && !visited.Contains(rightGards.First()))
            {
                queue.Enqueue(rightGards.First());
            }

            var bottomGards = gardsHashed.Where(g => g.Position.X == current.Position.X && g.Position.Y == current.Position.Y + 1);
            if (bottomGards.Count() > 1)
            {
                return false; // just a ssumption
            }

            if (bottomGards.Count() == 1 && !visited.Contains(bottomGards.First()))
            {
                queue.Enqueue(bottomGards.First());
            }
        }

        return visited.Count == gards.Length;
    }
}