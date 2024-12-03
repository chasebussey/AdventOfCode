using AdventOfCode;

namespace AdventOfCode2024Tests;

public class Tests
{
    [TestCase("7 6 4 2 1", ExpectedResult = true)]
    [TestCase("1 2 7 8 9", ExpectedResult = false)]
    [TestCase("9 7 6 2 1", ExpectedResult = false)]
    [TestCase("1 3 2 4 5", ExpectedResult = true)]
    [TestCase("8 6 4 4 1", ExpectedResult = true)]
    [TestCase("1 3 6 7 9", ExpectedResult = true)]
    public bool Part2(string input)
    {
        return Day2.Part2(input) == 1;
    }
}