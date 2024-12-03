namespace AdventOfCode;

public static class Day2
{
    public static int Part1(string inputPath)
    {
        var input = File.ReadAllText(inputPath);
        var reports = ParseInput(input);

        return reports.Count(IsSafe);
    }

    public static int Part2(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        var reports = ParseInput(input);

        var countSafe = 0;

        foreach (var report in reports)
        {
            if (IsSafe(report))
            {
                countSafe++;
                continue;
            }
            
            for (int i = 0; i < report.Count; i++)
            {
                
                var modifiedReport = report.ToList();
                modifiedReport.RemoveAt(i);

                if (!IsSafe(modifiedReport)) continue;
                countSafe++;
                break;
            }
        }
        
        return countSafe;
    }

    private static bool IsSafe(List<int> report)
    {
        var reportTuples = report.Zip(report.Skip(1)).ToList();
        var isAsc = reportTuples.All(tuple => tuple.First < tuple.Second);
        var isDesc = reportTuples.All(tuple => tuple.First > tuple.Second);

        // if the list isn't sorted, we're not safe
        if (!isAsc && !isDesc) return false;
            
        // else make sure no difference is between 1 and 3
        return reportTuples.All(tuple => 
            Math.Abs(tuple.First - tuple.Second) < 4 && Math.Abs(tuple.First - tuple.Second) > 0);
    }

    private static List<List<int>> ParseInput(string input)
    {
        var lines = input.Trim().Split("\n");

        return lines.Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();
    }
}