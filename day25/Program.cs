var input = File.ReadAllLines("input/input1.txt");

List<(int start, int end)> lockSchematics = [];
List<(int start, int end)> keySchematics = [];
List<List<int>> locks = [];
List<List<int>> keys = [];
var start = 0;
bool isLock = false;
for(var line = 0; line < input.Length; line++)
{
    if (start == line)
    {
        if (input.ElementAt(line)[0] == '#')
        {
            isLock = true;
        }
        if (input.ElementAt(line)[0] == '.')
        {
            isLock = false;
        }
    }
    if (string.IsNullOrWhiteSpace(input.ElementAt(line)))
    {
        if (isLock)
        {
            lockSchematics.Add((start, line - 1));
        }
        else
        {
            keySchematics.Add((start, line - 1));
        }
        start = line + 1;
        continue;
    }
}
if (isLock)
{
    lockSchematics.Add((start, input.Length - 1));
}
else
{
    keySchematics.Add((start, input.Length - 1));
}


foreach (var schematic in lockSchematics)
{
    List<int> l = [];
    for (var x = 0; x < input[0].Length; x++)
    {
        for (var y = schematic.start; y <= schematic.end; y++)
        {
            if (input[y][x] == '.')
            {
                l.Add(y - schematic.start - 1);
                break;
            }
        }
    }
    locks.Add(l);
}

foreach (var schematic in keySchematics)
{
    List<int> k = [];
    for (var x = 0; x < input[0].Length; x++)
    {
        for (var y = schematic.end; y >= schematic.start; y--)
        {
            if (input[y][x] == '.')
            {
                k.Add(schematic.end - y - 1);
                break;
            }
        }
    }
    keys.Add(k);
}

var result = 0;
foreach (var key in keys)
{
    foreach(var l in locks)
    {
        if (Fits(key, l))
        {
            result++;
        }
    }
}

Console.WriteLine(result);

static bool Fits(List<int> key, List<int> l)
{
    for (var i = 0; i < key.Count; i++)
    {
        if (key[i] + l[i] > 5)
        {
            return false;
        }
    }
    return true;
}
