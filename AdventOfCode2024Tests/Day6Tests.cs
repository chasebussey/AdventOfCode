using AdventOfCode;

namespace AdventOfCode2024Tests;

public class Day6Tests
{
    private const string TestInput = """
                                     ....#.....
                                     .........#
                                     ..........
                                     ..#.......
                                     .......#..
                                     ..........
                                     .#..^.....
                                     ........#.
                                     #.........
                                     ......#...
                                     """;


    [Test]
    public void Parse()
    {
        Assert.That(Day6.ParseInput(TestInput), Has.Count.EqualTo(10));
    }

    [Test]
    public void Part1()
    {
        var input = TestInput;
        Assert.That(Day6.Part1(input), Is.EqualTo(41));
    }

    [Test]
    public void Part2()
    {
        var input = TestInput;
        Assert.That(Day6.Part2(input), Is.EqualTo(6));
    }
}
