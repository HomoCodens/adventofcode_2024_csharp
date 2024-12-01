using AdventOfCode2024.Solvers.Day1;

var solver = new Day1Solver();

// var lines = File.ReadAllLines("/home/thoenis/projects/AoC24/AdventOfCode2024.Tests/TestData/Day1/example.txt");

var lines = File.ReadAllLines("/home/thoenis/projects/AoC24/AdventOfCode2024/Input/Day1/input.txt");

var result = solver.Solve(lines);

Console.WriteLine(result.SolutionPart1);
Console.WriteLine(result.SolutionPart2);