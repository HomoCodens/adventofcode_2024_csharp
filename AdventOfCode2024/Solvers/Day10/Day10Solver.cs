using System.Drawing;

namespace AdventOfCode2024.Solvers.Day10;

internal class Day10Solver : SolverBase<int[][]>
{
    protected override ulong SolvePart1(int[][] input)
    {
        var trailieHeadies = input.SelectMany((row, y) => row.Select((cell, x) => (x, y, cell))).Where(c => c.cell == 0).ToArray();
        return (ulong)trailieHeadies.Select(head => this.Score(input, head.x, head.y, new List<Point>()).Distinct().Count()).Sum();
    }

    protected override ulong SolvePart2(int[][] input)
    {
        var trailieHeadies = input.SelectMany((row, y) => row.Select((cell, x) => (x, y, cell))).Where(c => c.cell == 0).ToArray();
        return (ulong)trailieHeadies.Select(head => this.Score(input, head.x, head.y, new List<Point>()).Count()).Sum();
    }

    internal override int[][] ParseInput(string[] lines)
    {
        return lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
    }

    private List<Point> Score(int[][] topology, int x, int y, List<Point> trailHeads)
    {
        this.Continue(topology, x, y, 0, -1, trailHeads);
        this.Continue(topology, x, y, 0, 1, trailHeads);
        this.Continue(topology, x, y, -1, 0, trailHeads);
        this.Continue(topology, x, y, 1, 0, trailHeads);

        return trailHeads;
    }

    private int GetAt(int[][] topology, int x, int y)
    {
        if (x < 0 || x >= topology[0].Length || y < 0 || y >= topology.Length)
        {
            return int.MaxValue;
        }

        return topology[y][x];
    }

    private List<Point> Continue(int[][] topology, int x, int y, int dx, int dy, List<Point> trailHeads)
    {
        var nextX = x + dx;
        var nextY = y + dy;

        var currentCell = this.GetAt(topology, x, y);
        var nextCell = this.GetAt(topology, nextX, nextY);

        if (nextCell - currentCell != 1)
        {
            return trailHeads;
        }

        if (this.GetAt(topology, nextX, nextY) == 9)
        {
            trailHeads.Add(new Point(nextX, nextY));
            return trailHeads;
        }

        return this.Score(topology, nextX, nextY, trailHeads);
    }
}