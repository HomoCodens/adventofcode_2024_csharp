using System.Drawing;
using System.Numerics;

namespace AdventOfCode2024.Solvers.Day8;

internal class Day8Solver : SolverBase<City>
{
    protected override ulong SolvePart1(City input)
    {
        var antinodes = new HashSet<Point>();
        foreach (var (_, positions) in input.Antennae)
        {
            for (var i = 0; i < positions.Length - 1; i++)
            {
                var a = positions[i];
                for (var j = i + 1; j < positions.Length; j++)
                {
                    var b = positions[j];
                    var derVektor = new Point()
                    {
                        X = b.X - a.X,
                        Y = b.Y - a.Y,
                    };

                    antinodes.Add(new Point()
                    {
                        X = a.X - derVektor.X,
                        Y = a.Y - derVektor.Y,
                    });

                    antinodes.Add(new Point()
                    {
                        X = a.X + 2 * derVektor.X,
                        Y = a.Y + 2 * derVektor.Y,
                    });
                }
            }
        }

        return (ulong)antinodes.Where(p => IsInGrid(p, input.Width, input.Height)).Count();
    }

    protected override ulong SolvePart2(City input)
    {
        var harmonicAntinodes = new HashSet<Point>();
        foreach (var (_, positions) in input.Antennae)
        {
            for (var i = 0; i < positions.Length - 1; i++)
            {
                var a = positions[i];
                for (var j = i + 1; j < positions.Length; j++)
                {
                    var b = positions[j];
                    var derVektor = new Point()
                    {
                        X = b.X - a.X,
                        Y = b.Y - a.Y,
                    };

                    // eeh? why only BigInteger haz?
                    var gcd = BigInteger.GreatestCommonDivisor(derVektor.X, derVektor.Y);

                    var derAnführungszeichenEinheitsAnführungszeichenVektor = new Point()
                    {
                        X = (int)(derVektor.X / gcd),
                        Y = (int)(derVektor.Y / gcd),
                    };

                    harmonicAntinodes.Add(a);
                    var backwardPoint = new Point()
                    {
                        X = a.X - derAnführungszeichenEinheitsAnführungszeichenVektor.X,
                        Y = a.Y - derAnführungszeichenEinheitsAnführungszeichenVektor.Y,
                    };
                    while (IsInGrid(backwardPoint, input.Width, input.Height))
                    {
                        harmonicAntinodes.Add(backwardPoint);
                        backwardPoint = new Point()
                        {
                            X = backwardPoint.X - derAnführungszeichenEinheitsAnführungszeichenVektor.X,
                            Y = backwardPoint.Y - derAnführungszeichenEinheitsAnführungszeichenVektor.Y,
                        };
                    }

                    var forwardPoint = new Point()
                    {
                        X = a.X + derAnführungszeichenEinheitsAnführungszeichenVektor.X,
                        Y = a.Y + derAnführungszeichenEinheitsAnführungszeichenVektor.Y,
                    };
                    while (IsInGrid(forwardPoint, input.Width, input.Height))
                    {
                        harmonicAntinodes.Add(forwardPoint);
                        forwardPoint = new Point()
                        {
                            X = forwardPoint.X + derAnführungszeichenEinheitsAnführungszeichenVektor.X,
                            Y = forwardPoint.Y + derAnführungszeichenEinheitsAnführungszeichenVektor.Y,
                        };
                    }
                }
            }
        }

        return (ulong)harmonicAntinodes.Count();
    }

    internal override City ParseInput(string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        var antennae = new Dictionary<char, Point[]>();

        for (var line = 0; line < height; line++)
        {
            for (var column = 0; column < width; column++)
            {
                var c = lines[line][column];
                if (c == '.')
                {
                    continue;
                }

                if (!antennae.ContainsKey(c))
                {
                    antennae[c] = [];
                }

                antennae[c] = antennae[c].Append(new Point(column, line)).ToArray();
            }
        }

        return new City()
        {
            Width = width,
            Height = height,
            Antennae = antennae,
        };
    }

    private bool IsInGrid(Point point, int width, int height)
    {
        return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
    }
}