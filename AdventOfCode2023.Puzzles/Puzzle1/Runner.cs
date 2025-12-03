using AdventOfCode2023.Puzzles.Utils;
using FluentAssertions;

namespace AdventOfCode2023.Puzzles.Puzzle1;

public class Runner
{
    [Test]
    [Arguments("TestInput", 142)]
    [Arguments("Input", 55477)]
    public void RunAlpha(string filename, int expected)
    {
        var actual = Execute(filename, text => Finder(text, false));
        actual.Should().Be(expected);
        return;
    }

    [Test]
    [Arguments("TestInput2", 281)]
    [Arguments("Input", 54414)]
    public void RunBeta(string filename, int expected)
    {
        var actual = Execute(filename, text => Finder(text, true));
        actual.Should().Be(expected);
    }

    private static int Finder(string text, bool replace)
    {
        Console.WriteLine($"Text         : {text}");
        text = replace ? GetReplacedText(text) : text;
        Console.WriteLine($"Replaced text: {text}");
        var nrs = text.Where(Char.IsNumber).ToArray();
        var value = (nrs[0] - '0') * 10 + (nrs.Last() - '0');
        Console.WriteLine($"Value        : {value}");
        return value;
    }

    private static string GetReplacedText(string text)
    {
        var pos = 0;
        while (pos < text.Length)
        {
            foreach (var kvp in _mapper)
            {
                if (text.IndexOf(kvp.Key) == pos)
                {
                    text =
                        text.Substring(0, pos + kvp.Key.Length).Replace(kvp.Key, kvp.Value) +
                        text.Substring(pos + kvp.Key.Length);
                    break;
                }
            }

            pos++;
        }

        return text;
    }

    private static readonly IDictionary<string, string> _mapper = new Dictionary<string, string>()
    {
        {"one", "1" },
        {"two", "2" },
        {"three", "3" },
        {"four", "4" },
        {"five", "5" },
        {"six", "6" },
        {"seven", "7" },
        {"eight", "8" },
        {"nine", "9" }
    };

    private static int Execute(string filename, Func<string, int> finder)
    {
        var lines = EmbeddedResourceReader.Read<Runner>(filename);

        var sum = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            sum += finder(line);


        }

        return sum;
    }
}