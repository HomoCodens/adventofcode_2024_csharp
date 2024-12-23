// In which we lern that Day6's Point is in fact still a reference
// type and we somehow got away with that until now?
namespace AdventOfCode2024.Solvers.Day15;

internal class Day15Solver : SolverBase<Day15Input>
{
    protected override ulong SolvePart1(Day15Input input)
    {
        return SolveEr(input, BuildTrainStraight);
    }

    protected override ulong SolvePart2(Day15Input input)
    {
        return SolveEr(input with { Warehouse = Embiggify(input.Warehouse) }, BuildTrainTree);
    }

    internal override Day15Input ParseInput(string[] lines)
    {
        var delinemiter = Array.IndexOf(lines, "");
        var map = lines[..delinemiter];
        var directions = lines[(delinemiter + 1)..];

        var mapElements = map.SelectMany((l, y) => l.Select((c, x) => (c, new Point() { X = x, Y = y })))
        .GroupBy(p => p.Item1)
        .ToDictionary(g => g.Key, g => g.Select(x => x.Item2).ToList());

        var walls = mapElements['#'].Select(x => x!).ToHashSet();
        var bokses = mapElements['O'].Select(x => x!).ToHashSet();
        var robbit = mapElements['@'].Single()!;

        return new()
        {
            Warehouse = new()
            {
                Bokses = bokses,
                Robbit = robbit,
                Walls = walls,
            },
            Moves = directions.SelectMany(l => l.Select(c => c.ToDirection())).ToArray(),
        };
    }

    private static ulong SolveEr(Day15Input input, Func<LanternfishWarehouse, Point, (List<Point>, bool)> trainBuilder)
    {
        return Score(input.Moves.Aggregate(input.Warehouse, (w, d) => Step(w, d, trainBuilder)).Bokses);
    }

    private static LanternfishWarehouse Step(
        LanternfishWarehouse warehouse,
        Direction direction,
        Func<LanternfishWarehouse, Point, (List<Point>, bool)> trainBuilder)
    {
        var v = direction switch
        {
            Direction.Up => new Point() { X = 0, Y = -1 },
            Direction.Down => new Point() { X = 0, Y = 1 },
            Direction.Left => new Point() { X = -1, Y = 0 },
            Direction.Right => new Point() { X = 1, Y = 0 },
            _ => throw new Exception("Can you not count, C#?"),
        };

        var (train, canMove) = trainBuilder(warehouse, v);
        var result = warehouse;
        if (canMove)
        {
            result = warehouse with
            {
                Robbit = new Point() { X = warehouse.Robbit.X + v.X, Y = warehouse.Robbit.Y + v.Y },
                Bokses = [.. warehouse.Bokses.Where(b => !train.Contains(b)), .. train.Select(p => new Point() { X = p.X + v.X, Y = p.Y + v.Y })],
            };
        }
        return result;
    }

    private static (List<Point> Bokses, bool CanEvenMove) BuildTrainStraight(LanternfishWarehouse warehouse, Point velocidad)
    {
        var wagons = new List<Point>();
        var at = new Point() { X = warehouse.Robbit.X + velocidad.X, Y = warehouse.Robbit.Y + velocidad.Y };
        while (warehouse.Bokses.Contains(at))
        {
            wagons.Add(at);
            at = new Point()
            {
                X = at.X + velocidad.X,
                Y = at.Y + velocidad.Y,
            };
        }

        return (wagons, !warehouse.Walls.Contains(at));
    }

    private static (List<Point> Bokses, bool CanEvenMove) BuildTrainTree(LanternfishWarehouse warehouse, Point velocidad)
    {
        var ats = new Queue<Point>();
        ats.Enqueue(new Point() { X = warehouse.Robbit.X, Y = warehouse.Robbit.Y });
        var bokses = new List<Point>();

        while (ats.Count > 0)
        {
            var at = ats.Dequeue();
            var isRobbit = at == warehouse.Robbit;
            if (warehouse.Walls.Contains(at with { X = at.X + velocidad.X, Y = at.Y + velocidad.Y }) ||
                warehouse.Walls.Contains(at with { X = at.X + velocidad.X + (isRobbit ? 0 : 1), Y = at.Y + velocidad.Y }))
            {
                return ([], false);
            }

            if (velocidad.Y == 0)
            {
                var next = new Point() { X = at.X + (isRobbit && velocidad.X > 0 ? 1 : 2) * velocidad.X, Y = at.Y };
                if (warehouse.Bokses.Contains(next))
                {
                    bokses.Add(next);
                    ats.Enqueue(next);
                }
            }
            else
            {
                var next = Array.Empty<Point>();
                if (at == warehouse.Robbit)
                {
                    next = [
                        new() { X = at.X - 1, Y = at.Y + velocidad.Y },
                        new() { X = at.X, Y = at.Y + velocidad.Y },
                    ];
                }
                else
                {
                    next = [
                        new() { X = at.X - 1, Y = at.Y + velocidad.Y },
                        new() { X = at.X, Y = at.Y + velocidad.Y },
                        new() { X = at.X + 1, Y = at.Y + velocidad.Y },
                    ];
                }

                var nextWithBokses = next.Where(warehouse.Bokses.Contains).ToArray();

                bokses.AddRange(nextWithBokses);
                foreach (var n in nextWithBokses)
                {
                    ats.Enqueue(n);
                }
            }
        }

        return (bokses, true);
    }

    private static ulong Score(IEnumerable<Point> bokses)
    {
        return bokses.Aggregate(0ul, (acc, b) => acc + (ulong)(100 * b.Y + b.X));
    }

    private static void PrintWarehouse(LanternfishWarehouse warehouse, bool embigglified = false)
    {
        var bottomRight = warehouse.Walls.MaxBy(p => p.X + p.Y);

        for (var row = 0; row <= bottomRight.Y; row++)
        {
            for (var col = 0; col <= bottomRight.X; col++)
            {
                var cell = new Point() { X = col, Y = row };
                if (cell == warehouse.Robbit)
                {
                    Console.Write("@");
                }
                else if (warehouse.Walls.Contains(cell))
                {
                    Console.Write('#');
                }
                else if (warehouse.Bokses.Contains(cell))
                {
                    Console.Write(embigglified ? "[" : "O");
                }
                else
                {
                    Console.Write(embigglified && warehouse.Bokses.Contains(new Point() { X = col - 1, Y = row }) ? "]" : ".");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("===================");
    }

    private static LanternfishWarehouse Embiggify(LanternfishWarehouse warehouse)
    {
        return new LanternfishWarehouse()
        {
            Bokses = warehouse.Bokses.Select(b => new Point() { X = 2 * b.X, Y = b.Y }).ToHashSet(),
            Walls = warehouse.Walls.SelectMany(w => new Point[]
            {
                new() { X = 2*w.X, Y = w.Y },
                new() { X = 2*w.X + 1, Y = w.Y}, // Technically probably not even needed
            }).ToHashSet(),
            Robbit = new Point()
            {
                X = 2 * warehouse.Robbit.X,
                Y = warehouse.Robbit.Y,
            },
        };
    }
}