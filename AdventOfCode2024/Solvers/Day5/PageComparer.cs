namespace AdventOfCode2024.Solvers.Day5;

internal class PageComparer(IEnumerable<PageDependency> dependencies) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (DependsOn(x, y))
        {
            return 1;
        }

        if (DependsOn(y, x))
        {
            return -1;
        }

        return 0;
    }

    private bool DependsOn(int a, int b)
    {
        return dependencies.Any(d => d.Page == a && d.Dependency == b);
    }
}