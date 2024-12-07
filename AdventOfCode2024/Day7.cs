namespace AdventOfCode;

public static class Day7
{
    public static long Part1(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        List<(long, long[])> equations = Parse(input);
        return equations.Where(x => ValidateEquation(x)).Sum(y => y.Item1);
    }
    
    public static long Part2(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        List<(long, long[])> equations = Parse(input);
        var validEquations = equations.Where(x => ValidateEquationWithConcatenation(x));
        return equations.Where(x => ValidateEquationWithConcatenation(x)).Sum(y => y.Item1);
    }

    public static bool ValidateEquation((long, long[]) equation, long root = 0)
    {
        var (target, numbers) = equation;
        if (root > target) return false;
        if (numbers.Length == 0 && root != target) return false;
        if (numbers.Length == 0 && root == target) return true;

        return ValidateEquation((target, numbers[1..]), root * numbers[0]) ||
               ValidateEquation((target, numbers[1..]), root + numbers[0]);
    }

    public static bool ValidateEquationWithConcatenation((long, long[]) equation, long root = 0)
    {
        var (target, numbers) = equation;
        if (root > target) return false;
        if (numbers.Length == 0 && root != target) return false;
        if (numbers.Length == 0 && root == target) return true;

        return ValidateEquationWithConcatenation((target, numbers[1..]), root * numbers[0]) ||
               ValidateEquationWithConcatenation((target, numbers[1..]), root + numbers[0]) ||
               ValidateEquationWithConcatenation((target, numbers[1..]), Concatenate(root, numbers[0]));
    }
    
    public static long Concatenate(long x, long y)
    {
        var xStr = x.ToString();
        var yStr = y.ToString();
        return long.Parse(xStr + yStr);
    }
    
    // this could return (long, int[]) from the input
    public static List<(long, long[])> Parse(string input)
    {
        var lines = input.Split("\n");
        var equations = new List<(long, long[])>();
        foreach (var line in lines)
        {
            var lhs = long.Parse(line.Split(":")[0]);
            var rhs = line.Split(":")[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse).ToArray();
            var equation = (lhs, rhs);
            equations.Add(equation);
        }

        return equations;
    }
}