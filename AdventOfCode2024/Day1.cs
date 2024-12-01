namespace AdventOfCode;

public static class Day1
{
    public static int Part1(string inputPath)
    {
        var input  = File.ReadAllText(inputPath);

        var (leftList, rightList) = ParseInput(input);
        
        var result = leftList.Zip(rightList, (left, right) => Math.Abs(left - right)).Sum();
        
        return result;
    }

    public static int Part2(string inputPath)
    {
        var input  = File.ReadAllText(inputPath);
        
        var (leftList, rightList) = ParseInput(input);

        var result = leftList.Sum(item => item * rightList.Count(i => i == item));

        return result;
    }

    private static (List<int> leftList, List<int> rightList) ParseInput(string input)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();
        
        foreach (var line in input.Split('\n'))
        {
            var left = int.Parse(line.Split("   ")[0]);
            var right = int.Parse(line.Split("   ")[1]);
    
            leftList.Add(left);
            rightList.Add(right);
        }

        leftList.Sort();
        rightList.Sort();
        
        return (leftList, rightList);
    }
}