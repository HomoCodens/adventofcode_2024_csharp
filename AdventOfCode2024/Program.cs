using AdventOfCode2024.Solvers.Day1;
using AdventOfCode2024.Solvers.Day10;
using AdventOfCode2024.Solvers.Day11;
using AdventOfCode2024.Solvers.Day12;
using AdventOfCode2024.Solvers.Day13;
using AdventOfCode2024.Solvers.Day2;
using AdventOfCode2024.Solvers.Day3;
using AdventOfCode2024.Solvers.Day4;
using AdventOfCode2024.Solvers.Day5;
using AdventOfCode2024.Solvers.Day6;
using AdventOfCode2024.Solvers.Day7;
using AdventOfCode2024.Solvers.Day8;
using AdventOfCode2024.Solvers.Day9;

var solver = new Day13Solver();

var lines = File.ReadAllLines("/home/thoenis/projects/AoC24/AdventOfCode2024/Input/Day13/input.txt");

var result = solver.Solve(lines);
Console.WriteLine(result.SolutionPart1);
Console.WriteLine(result.SolutionPart2);