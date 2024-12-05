namespace AdventOfCode;

public static class Day5
{
    public static int Part1(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath); 
        
        var (rules, updates) = Parse(input);

        var validUpdates = updates.Where(x => IsValid(x, rules));
        var mids = validUpdates.Select(x => int.Parse(x[x.Count / 2])).ToList();
        return mids.Sum();
    }

    public static int Part2(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath); 
        
        var (rules, updates) = Parse(input);

        var invalidUpdates = updates.Where(x => !IsValid(x, rules));
        var validatedUpdates = invalidUpdates.Select(x => MakeValid(x, rules)).ToList();
        var mids = validatedUpdates.Select(x => int.Parse(x[x.Count / 2])).ToList();
        return mids.Sum();
    }
    
    public static bool IsValid(List<string> update, List<(string, string)> rules)
    {
        foreach (var rule in rules)
        {
            var firstIndex = update.IndexOf(rule.Item1);
            var secondIndex = update.IndexOf(rule.Item2);
            
            if (firstIndex == -1 || secondIndex == -1) continue;

            if (secondIndex < firstIndex) return false;
        }

        return true;
    }
    
    public static List<string> MakeValid(List<string> update, List<(string, string)> rules)
    {
        foreach (var rule in rules)
        {
            var firstIndex = update.IndexOf(rule.Item1);
            var secondIndex = update.IndexOf(rule.Item2);
            
            if (firstIndex == -1 || secondIndex == -1) continue;

            if (secondIndex < firstIndex)
            {
                (update[firstIndex], update[secondIndex]) = (update[secondIndex], update[firstIndex]);
            }
        }

        return IsValid(update, rules) ? update : MakeValid(update, rules);
    }

    public static (List<(string, string)>, List<List<string>>) Parse(string input)
    {
        var parts = input.Split("\n\n");
        var rules = parts[0].Split("\n").Select(x => (x.Split("|")[0], x.Split("|")[1])).ToList();
        var updates = parts[1].Split("\n").Select(x => x.Split(",").ToList()).ToList();
        return (rules, updates);
    }
}