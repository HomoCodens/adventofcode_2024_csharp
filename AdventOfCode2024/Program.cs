using AdventOfCode2024.Solvers.Day1;
using AdventOfCode2024.Solvers.Day2;
using AdventOfCode2024.Solvers.Day3;
using AdventOfCode2024.Solvers.Day4;

var solver = new Day4Solver();

var lines = File.ReadAllLines("E:/repos/adventofcode_2024_csharp/AdventOfCode2024/Input/Day4/input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

var result = solver.Solve(lines);

Console.WriteLine(result.SolutionPart1);
Console.WriteLine(result.SolutionPart2);