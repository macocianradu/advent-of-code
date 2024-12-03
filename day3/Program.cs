using System.Text.RegularExpressions;

var input = File.ReadLines(args[0]);

var regex = new Regex("mul\\(\\d+,\\d+\\)");
var result = 0;

var concat = input.Aggregate((a, b) => a + b);

var matches = regex.Matches(concat);

foreach (string match in matches.Select(m => m.ToString()))
{
    var numbers = new string(match.Skip(4).SkipLast(1).ToArray()).Split(',');
    result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
}

var invalids = concat.Split("don\'t").Skip(1);
foreach (var invalid in invalids)
{
    var remove = invalid.Split("do")[0];
    var removeMatches = regex.Matches(remove);

    foreach (string match in removeMatches.Select(m => m.ToString()))
    {
        var numbers = new string(match.Skip(4).SkipLast(1).ToArray()).Split(',');
        result -= int.Parse(numbers[0]) * int.Parse(numbers[1]);
    }
}

Console.WriteLine(result);
