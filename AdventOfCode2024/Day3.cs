using System.Text.RegularExpressions;

namespace AdventOfCode;

public static class Day3
{
    public static int Part1(string inputPath)
    {
        var input = File.ReadAllText(inputPath);
        var commands = Parse(input);
        return commands.Sum(x => x.Item1 * x.Item2);
    }
    
    public static int Part2(string inputPath)
    {
        var input = File.ReadAllText(inputPath);
        var commands = ParseExtended(input);

        var enabled = true;
        var sum = 0;
        foreach (var command in commands)
        {
            switch (command)
            {
                case "do()":
                    enabled = true;
                    break;
                case "don't()":
                    enabled = false;
                    break;
                default:
                    if (enabled)
                    {
                        var (a, b) = Parse(command)[0];
                        sum += a * b;
                    }
                    break;
            }
        }

        return sum;
    }

    private static List<(int, int)> Parse(string input)
    {
        var mulCommands = Regex.Matches(input, @"mul\(([0-9]{1,3}),([0-9]{1,3})\)");
        return mulCommands.Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))).ToList();
    }
    
    private static List<string> ParseExtended(string input)
    {
        var pattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)";
        var commands = Regex.Matches(input, pattern);
        return commands.Select(x => x.Value).ToList();
    }
}