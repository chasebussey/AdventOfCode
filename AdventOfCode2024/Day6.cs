using System.Text.RegularExpressions;

namespace AdventOfCode;

public static class Day6
{
    private const string GuardChars = @"[\^><v]{1}";

    public static int Part1(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        var map = ParseInput(input);

        var guardRow = map.First(x => Regex.IsMatch(x, GuardChars));
        var guardChar = Regex.Matches(guardRow, GuardChars)[0].Value;
        var startingPoint = (map.IndexOf(guardRow), guardRow.IndexOf(guardChar));

        var visited = FindPath(startingPoint, map);

        return visited.Count;
    }

    public static HashSet<(int, int)> FindPath((int, int) current, List<string> map)
    {
        var (row, col) = current;
        var visited = new HashSet<(int, int)>();
        while (row >= 0 && row < map.Count && col >= 0 && col < map[0].Length)
        {
            var guardChar = map[row][col];
            visited.Add((row, col));

            var next = guardChar switch
            {
                '^' => (row - 1, col),
                'v' => (row + 1, col),
                '<' => (row, col - 1),
                '>' => (row, col + 1),
                _ => (row, col)
            };
            
            if (next.Item1 < 0 || next.Item1 >= map.Count || next.Item2 < 0 || next.Item2 >= map[0].Length)
            {
                break;
            }
            
            var nextSpace = map[next.Item1][next.Item2];

            if (nextSpace == '#')
            {
                map = TurnGuard(map, current);
            }
            else
            {
                map = MoveGuard(map, current, next, guardChar);
                current = next;
                (row, col) = current;
            }
        }

        return visited;
    }

    public static List<string> TurnGuard(List<string> map, (int, int) current)
    {
        var (row, col) = current;
        var guardChar = map[row][col];
        var newChar = guardChar switch
        {
            '^' => '>',
            'v' => '<',
            '<' => '^',
            '>' => 'v',
            _ => guardChar
        };
        map[row] = map[row].Remove(col, 1).Insert(col, newChar.ToString());
        return map;
    }

    public static List<string> MoveGuard(List<string> map, (int, int) current, (int, int) next, char guardChar)
    {
        var (row, col) = current;
        var (nextRow, nextCol) = next;
        map[row] = map[row].Replace(guardChar, '.');
        map[nextRow] = map[nextRow].Remove(nextCol, 1).Insert(nextCol, guardChar.ToString());
        
        return map;
    }
    
    public static int Part2(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        var map = ParseInput(input);
        
        // probably my other methods should just not modify map in-place, but I'm lazy
        var origMap = new List<string>(map);

        var guardRow = map.First(x => Regex.IsMatch(x, GuardChars));
        var guardChar = Regex.Matches(guardRow, GuardChars)[0].Value;
        var startingPoint = (map.IndexOf(guardRow), guardRow.IndexOf(guardChar));

        var visited = FindPath(startingPoint, map);

        var sum = 0;
        
        foreach (var (row, col) in visited)
        {
            var currChar = origMap[row][col];
            
            // didn't know this syntax was possible until Rider suggested it
            if (currChar is '^' or '>' or '<' or 'v') continue;
            
            var newMap = new List<string>(origMap);
            newMap[row] = newMap[row].Remove(col, 1).Insert(col, "#");
            var makesCycle = HasCycle(startingPoint, newMap);
            if (makesCycle) sum++;
        }
        
        return sum;
    }
    
    public static bool HasCycle((int, int) current, List<string> map)
    {
        var (row, col) = current;
        var newVisited = new HashSet<(int, int, char)>();
        var hasCycle = false;
        while (row >= 0 && row < map.Count && col >= 0 && col < map[0].Length)
        {
            var guardChar = map[row][col];
            newVisited.Add((row, col, guardChar));

            var next = guardChar switch
            {
                '^' => (row - 1, col),
                'v' => (row + 1, col),
                '<' => (row, col - 1),
                '>' => (row, col + 1),
                _ => (row, col)
            };
            
            if (next.Item1 < 0 || next.Item1 >= map.Count || next.Item2 < 0 || next.Item2 >= map[0].Length)
            {
                break;
            }
            
            if (newVisited.Contains((next.Item1, next.Item2, guardChar)))
            {
                hasCycle = true;
                break;
            }
            
            var nextSpace = map[next.Item1][next.Item2];

            if (nextSpace == '#')
            {
                map = TurnGuard(map, current);
            }
            else
            {
                map = MoveGuard(map, current, next, guardChar);
                current = next;
                (row, col) = current;
            }
        }

        return hasCycle;
    }
    
    public static List<string> ParseInput(string input)
    {
        return input.Split("\n").ToList();
    }
}