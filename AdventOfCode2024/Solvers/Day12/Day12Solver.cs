using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Solvers.Day6;

namespace AdventOfCode2024.Solvers.Day12;

internal class Day12Solver : SolverBase<char[][]>
{
    protected override ulong SolvePart1(char[][] input)
    {
        var regions = MapRegions(input);

        return regions.Aggregate(0ul, (acc, x) => acc + (ulong)(x.PerryMeter * x.Plots.Count));
    }

    protected override ulong SolvePart2(char[][] input)
    {
        var regions = MapRegions(input);

        return (ulong)regions.Select(r => GetNumberOfEdges(r) * r.Plots.Count).Sum();
    }

    internal override char[][] ParseInput(string[] lines)
    {
        return lines.Select(l => l.Select(c => c).ToArray()).ToArray();
    }

    private static char GetAt(char[][] farm, int x, int y)
    {
        var height = farm.Length;
        var width = farm[0].Length;

        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return '.';
        }

        return farm[y][x];
    }

    private static Region[] DiveIntoPlot(char[][] farm, HashSet<Point> openPlots)
    {
        if (openPlots.Count == 0)
        {
            return [];
        }

        var nextPlot = openPlots.First();
        var regionOfPlot = MapPlot(farm, [nextPlot]);
        openPlots.RemoveWhere(regionOfPlot.Plots.Contains);
        return [regionOfPlot, .. DiveIntoPlot(farm, openPlots)];
    }

    private static Region MapPlot(char[][] farm, HashSet<Point> plots, HashSet<Point>? plotsWeBeenTo = null, int perryMeter = 0)
    {
        plotsWeBeenTo ??= [];

        if (plots.Count == 0)
        {
            var aPlot = plotsWeBeenTo.First();
            return new Region()
            {
                Kind = farm[aPlot.Y][aPlot.X],
                Plots = [.. plotsWeBeenTo],
                PerryMeter = perryMeter,
            };
        }

        var plot = plots.First();
        var neegburs = GetNeegburs(farm, plot);
        var newNeegburs = neegburs.Where(p => !plotsWeBeenTo.Contains(p));
        plots.Remove(plot);
        return MapPlot(farm, [.. newNeegburs, .. plots], [plot, .. plotsWeBeenTo], perryMeter + 4 - neegburs.Length);
    }

    private static Point[] GetNeegburs(char[][] farm, Point plot)
    {
        var x = plot.X;
        var y = plot.Y;
        var type = GetAt(farm, x, y);
        var candidates = new (Point Plot, char Type)[]{
            (new Point(){ X = x + 1, Y = y     }, GetAt(farm, x + 1, y)),
            (new Point(){ X = x - 1, Y = y     }, GetAt(farm, x - 1, y)),
            (new Point(){ X = x,     Y = y + 1 }, GetAt(farm, x, y + 1)),
            (new Point(){ X = x,     Y = y - 1 }, GetAt(farm, x, y - 1)),
        };

        var types = candidates.Select(x => x.Type).ToArray();

        return candidates
                .Where((x) => x.Type == type)
                .Select(x => x.Plot)
                .ToArray();
    }

    private static Region[] MapRegions(char[][] farm)
    {
        var plotsWhatWeNeetToGoTo = new HashSet<Point>(farm.SelectMany((row, y) => row.Select((col, x) => new Point() { X = x, Y = y })).ToArray());
        return DiveIntoPlot(farm, plotsWhatWeNeetToGoTo);
    }

    private static int GetNumberOfEdges(Region region)
    {
        Console.WriteLine($"\nNow walking a region of {region.Kind}s");

        var plots = region.Plots;
        // Step 1: Find all plots with an edge on their left ("all" because of holes)
        var rows = plots.GroupBy(p => p.Y).Select(r => r.OrderBy(p => p.X)).ToArray();
        var lefties = rows.Aggregate(new HashSet<Point>(), (acc, row) => [.. acc, .. row.Where(p => !plots.Contains(p with { X = p.X - 1 }))]);

        return WalkEdges(region.Plots, lefties);
    }

    private static int WalkEdges(HashSet<Point> plots, HashSet<Point> candidates, int nEdges = 0)
    {
        if (candidates.Count == 0)
        {
            return nEdges;
        }

        var next = candidates.OrderBy(p => p.X + 10000 * p.Y).First();

        Console.WriteLine($"Begin walking from {next}");

        var (Visited, nEdgesOnEdge) = WalkTheEdge(plots, next);
        candidates.RemoveWhere(p => Visited.Contains((p, new Point() { X = -1, Y = 0 })));

        return WalkEdges(plots, candidates, nEdges + nEdgesOnEdge);
    }

    private static (IEnumerable<(Point Plot, Point Normal)> Visited, int nEdges) WalkTheEdge(HashSet<Point> plots,
                                                                                             Point current,
                                                                                             int nEdges = 0,
                                                                                             Point? normal = null,
                                                                                             HashSet<(Point Plot, Point Normal)>? visited = null)
    {
        normal ??= new Point() { X = -1, Y = 0 };
        visited ??= [];

        Console.WriteLine($"Now at {current}, looking {normal}, having counted {nEdges} edges so far.");

        if (visited.Contains((current, normal)))
        {
            return (visited, nEdges);
        }

        visited.Add((current, normal));

        var (next, nextNormal) = Step(plots, current, normal);

        return WalkTheEdge(plots, next, nEdges + (normal.X != nextNormal.X ? 1 : 0), nextNormal, visited);
    }

    private static (Point Next, Point NextNormal) Step(HashSet<Point> plots, Point currentPlot, Point currentNormal)
    {
        var nextStepCandidates = EdgeCandidates(plots, currentPlot, currentNormal);
        var nextStep = nextStepCandidates.Where(p => plots.Contains(p)).FirstOrDefault();

        // Right turn
        if (nextStep == nextStepCandidates[0])
        {
            return (nextStep, RotateNormalCw(currentNormal));
        }

        // Straight
        if (nextStep == nextStepCandidates[1])
        {
            return (nextStep, currentNormal);
        }

        // Left turn
        return (currentPlot, RotateNormalCcw(currentNormal));
    }

    private static Point[] EdgeCandidates(HashSet<Point> plots, Point plot, Point normal)
    {
        return normal switch
        {
            // Left edge
            { X: -1, Y: 0 } => [
                new Point()
                {
                    X = plot.X - 1,
                    Y = plot.Y + 1,
                },
                new Point()
                {
                    X = plot.X,
                    Y = plot.Y + 1,
                },
            ],
            // Top edge
            { X: 0, Y: -1 } => [
                new Point()
                {
                    X = plot.X - 1,
                    Y = plot.Y - 1,
                },
                new Point()
                {
                    X = plot.X - 1,
                    Y = plot.Y,
                },
            ],
            // Right edge
            { X: 1, Y: 0 } => [
                new Point()
                {
                    X = plot.X + 1,
                    Y = plot.Y - 1,
                },
                new Point()
                {
                    X = plot.X,
                    Y = plot.Y - 1,
                },
            ],
            // Bottom edge
            { X: 0, Y: 1 } => [
                new Point()
                {
                    X = plot.X + 1,
                    Y = plot.Y + 1,
                },
                new Point()
                {
                    X = plot.X + 1,
                    Y = plot.Y,
                },
            ],
            _ => throw new Exception("Yellow squigglies eunt domus"),
        };
    }

    private static Point RotateNormalCw(Point normal)
    {
        return new Point()
        {
            X = -normal.Y,
            Y = normal.X,
        };
    }

    private static Point RotateNormalCcw(Point normal)
    {
        return new Point()
        {
            X = normal.Y,
            Y = -normal.X,
        };
    }
}