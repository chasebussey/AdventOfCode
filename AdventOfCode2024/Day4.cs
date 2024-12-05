using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public static partial class Day4
{
    private const string Pattern1 = @"XMAS";
    private const string Pattern2 = @"SAMX";

    public static int Part1(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        var rows = Parse(input);
        var cols = Transpose(rows);
        var diagonals = GetDiagonals(rows);
        var sum = 0;
        
        // start by just finding all the XMAS and SMAX in the rows
        sum += rows.Select(x => CountMatches(x)).Sum();
        
        // then do the same for the columns
        sum += cols.Select(x => CountMatches(x)).Sum();
        
        // now the diagonals
        sum += diagonals.Select(x => CountMatches(x)).Sum();
        
        return sum;
    }
    
    public static int Part2(string inputPath)
    {
        var input = inputPath;
        if (File.Exists(inputPath)) input = File.ReadAllText(inputPath);
        
        var rows = Parse(input);
        
        var matrices = SplitToMatrices(rows, 3, 3);
        var sum = matrices.Count(IsXMas);
        return sum;
    }
    
    private static bool IsXMas(List<string> matrix)
    {
        // all x-mas have middle A
        if (matrix[1][1] != 'A') return false;
        var validPattern = @"([MS]).{1}\1";

        var transposed = Transpose(matrix);
        
        // either the top and bottom rows match the pattern, or the first and last column do
        var valid = (Regex.IsMatch(matrix[0], validPattern) && Regex.IsMatch(matrix[2], validPattern))
            ^ (Regex.IsMatch(transposed[0], validPattern) && Regex.IsMatch(transposed[2], validPattern));

        return valid;
    }

    private static int CountMatches(string input)
    {
        return Xmas().Matches(input).Count + Samx().Matches(input).Count;
    }
    
    // Note: this assumes a rectangular input
    // TODO: make this generic and move to a util library
    public static List<string> Transpose(List<string> rows)
    {
        var result = new List<string>();
        int width = rows[0].Length;
        
        for (int i = 0; i < width; i++)
        {
            var newRow = new StringBuilder();
            foreach (var row in rows)
            {
                newRow.Append(row[i]);
            }
            result.Add(newRow.ToString());
        }

        return result;
    }

    // Note: this also assumes a rectangular input
    // TODO: either find a better way to do this, or make all the loops match in style
    //       also, make it generic and move to a util library
    public static List<string> GetDiagonals(List<string> rows)
    {
        var diagonals = new List<string>();
        int rowCount = rows.Count;
        int colCount = rows[0].Length;

        for (var col = 0; col < colCount; col++)
        {
            var diagonal = new StringBuilder();
            for (var row = 0; row < rowCount; row++)
            {
                if (col + row >= colCount) break;
                diagonal.Append(rows[row][col + row]);
            }
            diagonals.Add(diagonal.ToString());
        }

        for (var row = 1; row < rowCount; row++)
        {
            var diagonal = new StringBuilder();
            for (var col = 0; col < colCount; col++)
            {
                if (row + col >= rowCount) break;
                diagonal.Append(rows[row + col][col]);
            }
            diagonals.Add(diagonal.ToString());
        }

        for (var col = colCount - 1; col >= 0; col--)
        {
            var diagonal = new StringBuilder();
            for (var row = 0; row < rowCount; row++)
            {
                if (col - row < 0) break;
                diagonal.Append(rows[row][col - row]);
            }
            diagonals.Add(diagonal.ToString());
        }

        for (var row = 1; row < rowCount; row++)
        {
            var diagonal = new StringBuilder();
            for (int r = row, col = colCount - 1; r < rowCount && col >= 0; r++, col--)
            {
                diagonal.Append(rows[r][col]);
            }
            diagonals.Add(diagonal.ToString());
        }

        return diagonals;
    }
    
    // TODO: make this generic and move to a util library
    public static List<List<string>> SplitToMatrices(List<string> input, int width, int height)
    {
        var result = new List<List<string>>();
        int rowCount = input.Count;
        int colCount = input[0].Length;

        for (int row = 0; row <= rowCount - height; row++)
        {
            for (int col = 0; col <= colCount - width; col++)
            {
                var subList = new List<string>();
                for (int r = 0; r < height; r++)
                {
                    subList.Add(input[row + r].Substring(col, width));
                }
                result.Add(subList);
            }
        }

        return result;
    }
    
    public static List<string> Parse(string input)
    {
        return input.Trim().Split("\n").ToList();
    }

    // These were a style suggestion from Rider, but I'm not sure if I like them
    [GeneratedRegex(Pattern1)]
    private static partial Regex Xmas();
    [GeneratedRegex(Pattern2)]
    private static partial Regex Samx();
}