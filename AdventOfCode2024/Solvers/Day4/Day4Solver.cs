using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers.Day4;

internal partial class Day4Solver : SolverBase<string[]>
{
    internal override string[] ParseInput(string[] lines)
    {
        return lines;
    }

    protected override ulong SolvePart1(string[] input)
    {
        return (ulong)(FindXmases(input) + FindXmasesCol(input) + FindXmasesDiagUp(input) + FindXmasesDiagDown(input));
    }

    protected override ulong SolvePart2(string[] input)
    {
        var dim = input.Length;
        var count = 0;
        for (var i = 1; i < dim - 1; i++)
        {
            for (var j = 1; j < dim - 1; j++)
            {
                if (input[i][j] == 'A' && this.IsMasX(input, i, j))
                {
                    count++;
                }
            }
        }

        return (ulong)count;
    }

    private int FindXmases(IEnumerable<string> lines)
    {
        // This assuming that XMASAMX counts as two
        var xmasRegex = XmasRegex();
        var samxRegex = SamxRegex();
        return lines.Select(l => xmasRegex.Count(l) + samxRegex.Count(l)).Sum();
    }

    private int FindXmasesCol(string[] lines)
    {
        var dim = lines.Length;
        var columnwise = new string[dim];

        foreach (var col in Enumerable.Range(0, dim))
        {
            var colBuilder = new StringBuilder();
            foreach (var row in Enumerable.Range(0, dim))
            {
                colBuilder.Append(lines[row][col]);
            }
            columnwise[col] = colBuilder.ToString();
        }

        return FindXmases(columnwise);
    }

    private int FindXmasesDiagUp(string[] lines)
    {
        var dim = lines.Length;
        var nDiagonals = 2 * dim - 1;
        var diagonalwise = new string[nDiagonals];

        foreach (var diag in Enumerable.Range(0, nDiagonals))
        {
            var diagBuilder = new StringBuilder();
            var diagonalLenf = diag < dim ? diag + 1 : 2 * dim - 1 - diag;
            var startingRow = diag < dim ? diag : dim - 1;
            var startingCol = diag < dim ? 0 : diag - dim + 1;
            foreach (var idx in Enumerable.Range(0, diagonalLenf))
            {
                diagBuilder.Append(lines[startingRow - idx][startingCol + idx]);
            }
            diagonalwise[diag] = diagBuilder.ToString();
        }

        return FindXmases(diagonalwise);
    }

    private int FindXmasesDiagDown(string[] lines)
    {
        var dim = lines.Length;
        var nDiagonals = 2 * dim - 1;
        var diagonalwise = new string[nDiagonals];

        foreach (var diag in Enumerable.Range(0, nDiagonals))
        {
            var diagBuilder = new StringBuilder();
            var diagonalLenf = diag < dim ? diag + 1 : 2 * dim - 1 - diag;
            var startingRow = diag < dim ? 0 : diag - dim + 1;
            var startingCol = diag < dim ? dim - diag - 1 : 0;
            foreach (var idx in Enumerable.Range(0, diagonalLenf))
            {
                diagBuilder.Append(lines[startingRow + idx][startingCol + idx]);
            }
            diagonalwise[diag] = diagBuilder.ToString();
        }

        return FindXmases(diagonalwise);
    }

    private bool IsMasX(string[] lines, int row, int col)
    {
        var uppieLeftie = lines[row - 1][col - 1];
        var uppieRightie = lines[row - 1][col + 1];
        var downieLeftie = lines[row + 1][col - 1];
        var downieRightie = lines[row + 1][col + 1];

        return ((uppieLeftie == 'M' && downieRightie == 'S') ||
                (uppieLeftie == 'S' && downieRightie == 'M')) &&
                ((uppieRightie == 'M' && downieLeftie == 'S') ||
                (uppieRightie == 'S' && downieLeftie == 'M'));
    }

    [GeneratedRegex("XMAS")]
    private static partial Regex XmasRegex();
    [GeneratedRegex("SAMX")]
    private static partial Regex SamxRegex();
}