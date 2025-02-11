namespace AdventOfCode2024.Solvers.Day2;

internal sealed class Day2Solver : SolverBase<UnusualData>
{
    internal override UnusualData ParseInput(string[] lines)
    {
        var reports = lines.Select(line => line.Split(' ').Select(int.Parse).ToArray()).ToArray();
        return new UnusualData { Reports = reports };
    }

    protected override ulong SolvePart1(UnusualData input)
    {
        return (ulong)input.Reports.Select(report =>
        {
            // return report.SkipLast(1).Zip(report.Skip(1)).Select(x => x.Second - x.First).Any(diff => diff == 0 || diff > 3);
            var sign = 0;
            for (var i = 1; i < report.Length; i++)
            {
                var diff = report[i] - report[i - 1];

                if (diff == 0)
                {
                    return false;
                }

                var localSign = Math.Sign(diff);
                if (sign == 0)
                {
                    sign = localSign;
                }

                if (sign != localSign || Math.Abs(diff) > 3)
                {
                    return false;
                }
            }
            return true;
        })
        .Select(ok => ok ? 1 : 0)
        .Sum();
    }

    protected override ulong SolvePart2(UnusualData input)
    {
        return (ulong)input.Reports.Select(report =>
        {

            var sign = 0;
            var dampened = false;
            var dampenedFirstElement = false;
            for (var i = 1; i < report.Length; i++)
            {
                // That, my dears, is horseshit. Ya should only skip an element on the iteration where the problem occurs.
                var dampeningOffset = dampened ? (dampenedFirstElement ? 0 : 1) : 0;
                var diff = report[i] - report[i - 1 - dampeningOffset];

                if (diff == 0)
                {
                    if (dampened)
                    {
                        Console.WriteLine(string.Join(" ", report));
                        Console.WriteLine("no bueno");
                        return false;
                    }

                    dampenedFirstElement = i == 1;
                    dampened = true;
                    continue;
                }

                var localSign = Math.Sign(diff);
                if (sign == 0)
                {
                    sign = localSign;
                }

                if (sign != localSign)
                {
                    if (dampened)
                    {
                        Console.WriteLine(string.Join(" ", report));
                        Console.WriteLine("no bueno");
                        return false;
                    }

                    dampenedFirstElement = i == 2;
                    sign = -sign;
                    dampened = true;
                    continue;
                }

                if (Math.Abs(diff) > 3)
                {
                    if (dampened)
                    {
                        Console.WriteLine(string.Join(" ", report));
                        Console.WriteLine("no bueno");
                        return false;
                    }
                    dampenedFirstElement = i == 1;
                    dampened = true;
                }
            }
            // Console.WriteLine("SII BUENO");
            return true;
        })
        .Select(ok => ok ? 1 : 0)
        .Sum();
    }
}