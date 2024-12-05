using AdventOfCode;

namespace AdventOfCode2024Tests;

public class Day5Tests
{
    private const string Input = """
                                 47|53
                                 97|13
                                 97|61
                                 97|47
                                 75|29
                                 61|13
                                 75|53
                                 29|13
                                 97|29
                                 53|29
                                 61|53
                                 97|53
                                 61|29
                                 47|13
                                 75|47
                                 97|75
                                 47|61
                                 75|61
                                 47|29
                                 75|13
                                 53|13
                                 
                                 75,47,61,53,29
                                 97,61,53,29,13
                                 75,29,13
                                 75,97,47,61,53
                                 61,13,29
                                 97,13,75,29,47
                                 """;
    [Test]
    public void Parse()
    {
        var (rules, updates) = Day5.Parse(Input);
        Assert.That(rules, Has.Count.EqualTo(21));
        Assert.That(updates, Has.Count.EqualTo(6));
    }

    [Test]
    public void IsValid()
    {
        var (rules, updates) = Day5.Parse(Input);
        Assert.That(Day5.IsValid(updates[0], rules), Is.True);
        Assert.That(Day5.IsValid(updates[3], rules), Is.False);
    }

    [Test]
    public void Part1()
    {
        Assert.That(Day5.Part1(Input), Is.EqualTo(143));
    }

    [Test]
    public void Part2()
    {
        Assert.That(Day5.Part2(Input), Is.EqualTo(123));
    }
}