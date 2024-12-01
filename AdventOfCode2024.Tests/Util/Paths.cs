namespace AdventOfCode2024.Tests.Util;

public static class Paths
{
    public static string TestInputPath(int day, string fileName)
    {
        var assemblyDir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(assemblyDir, "TestData", $"Day{day}", fileName);
    }
}