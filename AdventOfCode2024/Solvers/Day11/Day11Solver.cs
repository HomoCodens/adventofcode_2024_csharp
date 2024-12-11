namespace AdventOfCode2024.Solvers.Day11;

internal class Day11Solver : SolverBase<ulong[]>
{
    protected override ulong SolvePart1(ulong[] input)
    {
        var stones = input.AsEnumerable();
        for (var i = 0; i < 25; i++)
        {
            Console.WriteLine(string.Join(' ', stones));
            stones = this.Blink(stones);
        }

        return (ulong)stones.Count();
    }

    protected override ulong SolvePart2(ulong[] input)
    {
        throw new NotImplementedException();
    }

    internal override ulong[] ParseInput(string[] lines)
    {
        return lines.First().Split(' ').Select(ulong.Parse).ToArray();
    }

    private IEnumerable<ulong> Blink(IEnumerable<ulong> stones)
    {
        return stones.SelectMany(s =>
        {
            var nDigits = Math.Ceiling(Math.Log10(s));
            return s switch
            {
                0 => [1ul],
                _ when nDigits > 0 && nDigits % 2 == 0 => new ulong[] { (ulong)Math.Floor(s / Math.Pow(10, nDigits / 2)), s % (ulong)Math.Pow(10, nDigits / 2) },
                _ => [s * 2024ul]
            };
        });
    }
}