namespace AdventOfCode2024.Solvers.Day15;

internal enum Direction
{
    Up,
    Right,
    Down,
    Left
}

internal static class CharExtensions
{
    public static Direction ToDirection(this char x)
    {
        return x switch
        {
            '^' => Direction.Up,
            '>' => Direction.Right,
            'v' => Direction.Down,
            '<' => Direction.Left,
            _ => throw new Exception("ot onna appen"),
        };
    }
}