using AdventOfCode2024.Solvers.Day1;
using AdventOfCode2024.Solvers.Day2;

var solver = new Day2Solver();

// var lines = File.ReadAllLines("/home/thoenis/projects/AoC24/AdventOfCode2024.Tests/TestData/Day1/example.txt");

var lines = File.ReadAllLines("E:/repos/adventofcode_2024_csharp/AdventOfCode2024/Input/Day2/input.txt");

var result = solver.Solve(lines);

Console.WriteLine(result.SolutionPart1);
Console.WriteLine(result.SolutionPart2);