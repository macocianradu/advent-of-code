using System.Text.RegularExpressions;

var input = File.ReadLines(args[0]);

var regex = new Regex("mul\\(\\d+,\\d+\\)");
var result = 0;

var count = 0;

foreach (var line in input)
{
    var matches = regex.Matches(line);
    foreach (string match in matches.Select(m => m.ToString()))
    {
        var numbers = new string(match.Skip(4).SkipLast(1).ToArray()).Split(',');
        result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
    }
}

Console.WriteLine(result);

// foreach (var line in input)
// {
//     var muls = line.Split("mul");
//     foreach (var mul in muls)
//     {
//         result += AddIfValid(mul);
//     }
// }

// int AddIfValid(string line)
// {
//     if (line[0] != '(')
//     {
//         return 0;
//     }
//     line = line[1..];
//     var split = line.Split(',');
//     if (split.Length < 2)
//     {
//         return 0;
//     }
//     var number1 = split[0];
//     var number2 = new string(split[1].TakeWhile(c => c != ')').ToArray());
//     if (!int.TryParse(number1, out var int1) || number1.Contains(' ')
//             || number1.Contains('\t'))
//     {
//         return 0;
//     }
// 
//     if (!int.TryParse(number2, out var int2) || number2.Contains(' ')
//             || number2.Contains('\t'))
//     {
//         return 0;
//     }
//     Console.WriteLine(++count + ": " + number1 + "-" + int1 + " " + number2 + "-" + int2);
// 
//     return int1 * int2;
// }
