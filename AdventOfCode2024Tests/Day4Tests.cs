using AdventOfCode;

namespace AdventOfCode2024Tests;

public class Day4Tests
{
    private const string Input = """
                                 12345678
                                 23456789
                                 34567890
                                 45678901
                                 """;

    private static readonly List<string> Transposed = ["1234", "2345", "3456", "4567", "5678", "6789", "7890", "8901"];

    private static readonly List<string> Diagonals =
        ["1", "22", "333", "4444", "5555", "6666", "7777", "8888", "999", "00", "1",
        "4", "35", "246", "1357", "2468", "3579", "4680", "5791", "680", "79", "8"];

    private const string TestInput = """
                                     MMMSXXMASM
                                     MSAMXMSMSA
                                     AMXSXMAAMM
                                     MSAMASMSMX
                                     XMASAMXAMM
                                     XXAMMXXAMA
                                     SMSMSASXSS
                                     SAXAMASAAA
                                     MAMMMXMMMM
                                     MXMXAXMASX
                                     """;


    private const string MatrixInput = """
                                       1234
                                       2xxx
                                       3xxx
                                       4xxx
                                       5xxx
                                       """;

    [Test]
    public void Transpose()
    {
        var inputLines = Input.Trim().Split("\n").ToList();
        Assert.That(Transposed, Is.EqualTo(Day4.Transpose(inputLines)));
    }

    [Test]
    public void GetDiagonals()
    {
        var inputLines = Input.Trim().Split("\n").ToList();
        var output = Day4.GetDiagonals(inputLines);
        Assert.That(Diagonals, Has.Count.EqualTo(output.Count));
        var equivalent = Diagonals.All(x => output.Contains(x));
        Assert.That(equivalent, Is.True);
    }

    [Test]
    public void Part1()
    {
        Assert.That(Day4.Part1(TestInput), Is.EqualTo(18));
    }

    [Test]
    public void SplitToMatrices()
    {
        var lines = TestInput.Trim().Split("\n").ToList();
        var rows = lines.Count;
        var cols = lines[0].Length;
        var n = 3;

        var matrixCount = 56;
        
        Assert.That(Day4.SplitToMatrices(lines, n, n), Has.Count.EqualTo(matrixCount));
    }
    
    [Test]
    public void Part2()
    {
        Assert.That(Day4.Part2(TestInput), Is.EqualTo(9));
    }
}