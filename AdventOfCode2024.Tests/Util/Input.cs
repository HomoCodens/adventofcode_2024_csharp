namespace AdventOfCode2024.Tests.Util;

public static class Input
{
    public static string[] GetInput(int day, string fileName)
    {
        return File.ReadAllLines(Paths.TestInputPath(day, fileName));
    }
}