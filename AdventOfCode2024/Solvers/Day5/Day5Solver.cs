namespace AdventOfCode2024.Solvers.Day5;

internal class Day5Solver : SolverBase<PrinterSetup>
{
    protected override int SolvePart1(PrinterSetup input)
    {
        return input.Jobs.Where(j => IsDotGud(input.Dependencies, j)).Select(GetMiddleThinger).Sum();
    }

    protected override int SolvePart2(PrinterSetup input)
    {
        var notGudJobs = input.Jobs.Where(j => !IsDotGud(input.Dependencies, j)).ToArray();

        return input.Jobs
                .Where(j => !IsDotGud(input.Dependencies, j))
                .Select(j => j.OrderBy(p => p, new PageComparer(GetDependenciesForJob(input.Dependencies, j))))
                .Select(x => GetMiddleThinger([.. x]))
                .Sum();
    }

    internal override PrinterSetup ParseInput(string[] lines)
    {
        var dependencies = new List<PageDependency>();

        var lineIndex = 0;
        while (!string.IsNullOrWhiteSpace(lines[lineIndex]))
        {
            var parts = lines[lineIndex].Split('|');
            dependencies.Add(new PageDependency
            {
                Page = int.Parse(parts[1]),
                Dependency = int.Parse(parts[0])
            });
            lineIndex++;
        }

        lineIndex++;

        var jobs = new List<int[]>();

        while (lineIndex < lines.Length)
        {
            jobs.Add(lines[lineIndex].Split(',').Select(int.Parse).ToArray());
            lineIndex++;
        }

        return new PrinterSetup
        {
            Dependencies = dependencies,
            Jobs = jobs.ToArray()
        };
    }

    private static bool IsDotGud(IEnumerable<PageDependency> dependencies, int[] job)
    {
        var hotPairs = GetDependenciesForJob(dependencies, job);

        foreach (var page in job)
        {
            if (hotPairs.Any(d => d.Page == page))
            {
                return false;
            }

            hotPairs = hotPairs.Where(d => d.Dependency != page);
        }

        return true;
    }

    private static int GetMiddleThinger(int[] job)
    {
        return job[(int)(job.Length / 2.0)];
    }

    private static IEnumerable<PageDependency> GetDependenciesForJob(IEnumerable<PageDependency> dependencies, int[] job)
    {
        return dependencies.Select(d => d with { }).Where(d => job.Contains(d.Page) && job.Contains(d.Dependency));
    }
}