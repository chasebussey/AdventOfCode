using AdventOfCode;

namespace AdventOfCode2024Tests;

public class Day7Tests
{
    private const string TestInput = """
                                     190: 10 19
                                     3267: 81 40 27
                                     83: 17 5
                                     156: 15 6
                                     7290: 6 8 6 15
                                     161011: 16 10 13
                                     192: 17 8 14
                                     21037: 9 7 18 13
                                     292: 11 6 16 20
                                     """;

    [Test]
    public void Part1()
    {
        Assert.That(Day7.Part1(TestInput), Is.EqualTo(3749));
    }

    [Test]
    public void Part2()
    {
        Assert.That(Day7.Part2(TestInput), Is.EqualTo(11387));
    }

    [Test]
    public void Concatenate()
    {
        Assert.That(Day7.Concatenate(1, 2), Is.EqualTo(12));
    }

    [Test]
    public void ValidateEquationWithConcatenation()
    {
        Assert.That(Day7.ValidateEquationWithConcatenation((7290, [6, 8, 6, 15])));
    }
}