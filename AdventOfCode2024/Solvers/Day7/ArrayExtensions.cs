namespace AdventOfCode2024.Solvers.Day7;

internal static class ArrayExtensions
{
    public static void Deconstruct(this ulong[] array, out ulong first, out ulong[] rest)
    {
        first = array[0];
        rest = array[1..];
    }
}