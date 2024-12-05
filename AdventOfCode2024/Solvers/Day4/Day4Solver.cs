using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;

internal partial class Day4Solver : SolverBase<string[]>
{
    internal override string[] ParseInput(string[] lines)
    {
        return lines;
    }

    protected override int SolvePart1(string[] input)
    {
        return this.FindXmases(input) + this.FindXmasesCol(input) + FindXmasesDiagUp(input);
    }

    protected override int SolvePart2(string[] input)
    {
        throw new NotImplementedException();
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
        var columnwise = new StringBuilder[dim];

        foreach (var col in Enumerable.Range(0, dim - 1))
        {
            var colBuilder = columnwise[col];
            foreach (var row in Enumerable.Range(0, dim - 1))
            {
                colBuilder.Append(lines[row][col]);
            }
        }

        return FindXmases(columnwise.Select(b => b.ToString()));
    }

    private int FindXmasesDiagUp(string[] lines)
    {
        var dim = lines.Length;
        var nDiagonals = 2 * dim - 1;
        var diagonalwise = new StringBuilder[nDiagonals];

        foreach (var diag in Enumerable.Range(0, nDiagonals - 1))
        {
            var diagBuilder = diagonalwise[diag];
            var diagonalLenf = diag < dim ? diag : 2 * dim - 1 - diag;
            var startingRow = diag < dim ? diag : dim;
            var startingCol = diag < dim ? 0 : 2 * diag - 1 - dim;
            foreach (var idx in Enumerable.Range(0, diagonalLenf))
            {
                diagBuilder.Append(lines[startingRow - idx][startingCol + idx]);
            }
        }

        return FindXmases(diagonalwise.Select(b => b.ToString()));
    }

    [GeneratedRegex("XMAS")]
    private static partial Regex XmasRegex();
    [GeneratedRegex("SAMX")]
    private static partial Regex SamxRegex();
}