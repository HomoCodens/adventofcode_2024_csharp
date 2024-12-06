namespace AdventOfCode2024.Solvers.Day6;

internal class Day6Solver : SolverBase<World>
{
    protected override int SolvePart1(World input)
    {
        var positionsVisited = GetPositionsVisited(input.Warehouse, input.GuardState);

        return positionsVisited.Count;
    }

    protected override int SolvePart2(World input)
    {
        var positionsVisited = GetPositionsVisited(input.Warehouse, input.GuardState);

        var count = 0;

        // Could optimize e.g. by filtering out those that do not hit an obstacle when turning right and goind straight
        foreach (var pos in positionsVisited)
        {
            if (!IsInWorld(input.Warehouse, pos))
            {
                continue;
            }

            var newWarehouse = input.Warehouse.Select(r => r.ToArray()).ToArray();
            newWarehouse[pos.Y][pos.X] = FloorType.Obstacool;

            if (DetectLoop(newWarehouse, input.GuardState))
            {
                count++;
            }
        }

        return count;
    }

    internal override World ParseInput(string[] lines)
    {
        var guardRow = lines.Where(l => l.Contains('^')).First();
        var guardCol = guardRow.IndexOf('^');

        var floor = lines.Select(l => l.Select(c => c switch
        {
            '.' => FloorType.Floor,
            '#' => FloorType.Obstacool,
            '^' => FloorType.Floor,
            _ => throw new InvalidOperationException()
        }).ToArray()).ToArray();

        return new World
        {
            GuardState = new GuardState
            {
                Position = new Point
                {
                    X = guardCol,
                    Y = Array.IndexOf(lines, guardRow),
                },
                Heading = new Point { X = 0, Y = -1 },
            },
            Warehouse = floor,
        };
    }

    private static GuardState Move(GuardState state, FloorType[][] world)
    {
        var newPosition = new Point
        {
            X = state.Position.X + state.Heading.X,
            Y = state.Position.Y + state.Heading.Y,
        };

        if (!IsInWorld(world, newPosition))
        {
            return state with { Position = newPosition };
        }

        if (world[newPosition.Y][newPosition.X] == FloorType.Obstacool)
        {
            return state with { Heading = new Point { X = -state.Heading.Y, Y = state.Heading.X } };
        }

        return state with { Position = newPosition };
    }

    private static bool IsInWorld(FloorType[][] world, Point position)
    {
        return position.X >= 0 && position.X < world[0].Length && position.Y >= 0 && position.Y < world.Length;
    }

    private static List<Point> GetPositionsVisited(FloorType[][] world, GuardState state)
    {
        var positionsVisited = new HashSet<Point>();

        while (IsInWorld(world, state.Position))
        {
            state = Move(state, world);

            if (IsInWorld(world, state.Position))
            {
                positionsVisited.Add(state.Position);
            }
        }

        return positionsVisited.ToList();
    }

    private static bool DetectLoop(FloorType[][] world, GuardState state)
    {
        var positionsVisited = new HashSet<GuardState>();

        while (IsInWorld(world, state.Position))
        {
            state = Move(state, world);

            if (positionsVisited.Contains(state))
            {
                return true;
            }

            positionsVisited.Add(state);
        }

        return false;
    }
}