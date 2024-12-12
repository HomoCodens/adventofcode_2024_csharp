namespace AdventOfCode2024.Solvers.Day11;

internal class Day11Solver : SolverBase<ulong[]>
{
    protected override ulong SolvePart1(ulong[] input)
    {
        var x = (ulong)6;
        for (var i = 0; i <= 18; i++)
        {
            var nDigits = Math.Floor(Math.Log10(x)) + 1;
            var halfths = Math.Pow(10, nDigits / 2);
            Console.WriteLine($"{x} has {nDigits} digits and results in {halfths}");
            x = x * 10 + 6;
        }

        var stones = input.AsEnumerable();
        for (var i = 0; i < 25; i++)
        {
            Console.WriteLine(stones.Count());
            // Console.WriteLine(string.Join(' ', stones));
            stones = this.Blink(stones);
        }

        return (ulong)stones.Count();
    }

    protected override ulong SolvePart2(ulong[] input)
    {
        var memry = new Dictionary<(ulong, int), ulong>();
        return input.Select(s => AllTheStones(s, 75, memry)).Aggregate((acc, x) => acc + x);
    }

    internal override ulong[] ParseInput(string[] lines)
    {
        return lines.First().Split(' ').Select(ulong.Parse).ToArray();
    }

    private IEnumerable<ulong> Blink(IEnumerable<ulong> stones)
    {
        return stones.SelectMany(VolveStone);
    }

    private IEnumerable<ulong> VolveStone(ulong stone)
    {
        var nDigits = Math.Floor(Math.Log10(stone)) + 1;
        return stone switch
        {
            0 => [1ul],
            _ when nDigits % 2 == 0 => new ulong[] { (ulong)Math.Floor(stone / Math.Pow(10, nDigits / 2)), stone % (ulong)Math.Pow(10, nDigits / 2) },
            _ => [stone * 2024ul]
        };
    }

    private ulong AllTheStones(ulong stone, int stepsToGo, Dictionary<(ulong, int), ulong> memry)
    {
        if (stepsToGo == 0)
        {
            return 1;
        }

        if (!memry.ContainsKey((stone, stepsToGo)))
        {
            memry[(stone, stepsToGo)] = VolveStone(stone).Select(newStone => AllTheStones(newStone, stepsToGo - 1, memry)).Aggregate((acc, x) => acc + x);
        }

        return memry[(stone, stepsToGo)];
    }
}