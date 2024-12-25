using System.Text;

using day21;

var input = File.ReadAllLines("input/input1.txt");
Dictionary<(char start, char end, int step), long> cache = new();

var result = 0L;
foreach (var line in input)
{
    var length = 0L;
    var s = 'A';
    foreach (var c in line)
    {
        length += GetLength(s, c, 25, Keypad.Map);
        s = c;
    }
    Console.WriteLine(": " + length + " * " + int.Parse(line[..^1]));

    result += length * int.Parse(line[..^1]);
}

long GetLength(char start, char end, int step, Dictionary<(char start, char end), List<string>> map)
{
    if (cache.TryGetValue((start, end, step), out var result))
    {
        return result;
    }
    if (step == 0)
    {
        return map[(start, end)][0].Length + 1;
    }
    var min = long.MaxValue;
    foreach (var possible in map[(start, end)])
    {
        var s = 'A';
        var length = 0L;
        var seq = possible.Append('A');
        foreach (var c in seq)
        {
            length += GetLength(s, c, step - 1, Robot.Map);
            s = c;
        }
        if (length < min)
        {
            min = length;
        }
    }
    cache.Add((start, end, step), min);

    return min;
}

//long GetLength(char start, char end, int step)
//{
//    if (cache.TryGetValue((start, end, step), out var result))
//    {
//        return result;
//    }
//    if (step == 0)
//    {
//        return Robot.Map[(start, end)][0].Length + 1;
//    }
//
//    long min = long.MaxValue;
//    foreach (var possible in Robot.Map[(start, end)])
//    {
//        var s = 'A';
//        var length = 0L;
//        foreach (var c in possible)
//        {
//            length += GetLength(s, c, step - 1);
//            s = c;
//        }
//        length += GetLength(s, 'A', step - 1);
//
//        if (length < min)
//        {
//        Console.WriteLine(possible);
//            min = length;
//        }
//    }
//    cache.Add((start, end, step), min);
//
//    return min;
//}

Console.WriteLine(result);
