using System.Numerics;
using System.Text;

namespace AdventOfCode2024.Solvers.Day9;

internal class Day9Solver : SolverBase<Block[]>
{
    protected override ulong SolvePart1(Block[] input)
    {
        Console.WriteLine(this.PrintBlocks(input, 16));
        var defragged = this.Defarg([.. input]);
        Console.WriteLine(this.PrintBlocks(defragged, 16));
        return this.CalculateChecksum(defragged);
    }

    protected override ulong SolvePart2(Block[] input)
    {
        throw new NotImplementedException();
    }

    internal override Block[] ParseInput(string[] lines)
    {
        var diskMap = lines.First().Select(x => int.Parse(x.ToString())).ToArray();
        var nBlocks = diskMap.Select((x, i) => i % 2 > 0 ? 0 : x).Sum();
        var nFiles = diskMap.Length / 2 + 1;
        var blocks = new List<Block>();
        var currentIndex = 0;

        for (var i = 0; i <= (2 * nFiles) - 1; i += 2)
        {
            var fileSize = diskMap[i];
            var emptySpaceAfterFile = i < nFiles ? diskMap[i + 1] : 0;
            for (var block = 0; block < fileSize; block++)
            {
                blocks.Add(new Block()
                {
                    FileId = i / 2,
                    Index = currentIndex++
                });
            }

            currentIndex += emptySpaceAfterFile;
        }

        return blocks.ToArray();
    }

    private Block[] Defarg(Block[] blocks)
    {
        var pointyFronty = 0;
        var pointyBacky = blocks.Length - 1;
        var index = 0;

        while (pointyFronty < pointyBacky)
        {
            if (blocks[pointyFronty].Index == index)
            {
                pointyFronty++;
            }
            else
            {
                blocks[pointyBacky] = blocks[pointyBacky] with { Index = index };
                pointyBacky--;
            }

            index++;
        }

        var bLocks = blocks.OrderBy(b => b.Index).ToArray();
        return blocks;
    }

    private ulong CalculateChecksum(Block[] blocks)
    {
        return blocks.Aggregate(0ul, (acc, b) => acc + (ulong)b.FileId * (ulong)b.Index);
    }

    private string PrintBlocks(Block[] blocks, int nBlocksOnHardDisk)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < nBlocksOnHardDisk; i++)
        {
            var derBlock = blocks.FirstOrDefault(b => b.Index == i);
            if (derBlock is not null)
            {
                sb.Append($"({derBlock.FileId})");
            }
            else
            {
                sb.Append(".");
            }
        }

        return sb.ToString();
    }
}