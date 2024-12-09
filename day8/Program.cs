var input = File.ReadLines(args[0]);
var antennas = new Dictionary<char, List<(int x, int y)>>();
var antinodes = new List<(int x, int y)>();

var xBound = input.Count();
var yBound = input.ElementAt(0).Length;
for (var y = 0; y < input.Count(); y++)
{
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        var c = input.ElementAt(y)[x];
        if (c != '.')
        {
            if (antennas.TryGetValue(c, out List<(int x, int y)>? value))
            {
                value.Add((x, y));
            }
            else
            {
                antennas.Add(c, [(x, y)]);
            }
        }
    }
}

foreach (var key in antennas.Keys)
{
    SetAntinodes(antennas[key], xBound, yBound);
}

Console.WriteLine(antinodes.Count);

void SetAntinodes(List<(int x, int y)> antennas, int xBound, int yBound)
{
    foreach ((int x, int y) in antennas)
    {
        foreach ((int x1, int y1) in antennas)
        {
            if (x == x1 && y == y1)
            {
                continue;
            }
            int dx = x >= x1 ? x1 - x : -(x - x1);
            int dy = y >= y1 ? y1 - y : -(y - y1);
            int low = LowestMultiplier(x, dx, xBound);
            int high = HighestMultiplier(x, dx, xBound);
            if (low > high)
            {
                var t = low;
                low = high;
                high = low;
            }
            for (int mul = low;
                    mul <= high;
                    mul++)
            {
                int ax = x + dx * mul;
                int ay = y + dy * mul;
                if (Inside(ax, ay, xBound, yBound))
                {
                    if (!antinodes.Contains((ax, ay)))
                    {
                        antinodes.Add((ax, ay));
                    }
                }
            }
        }
    }
}

static bool Inside(int x, int y, int xBound, int yBound)
{
    return x >= 0 && x < xBound && y >= 0 && y < yBound;
}

static int LowestMultiplier(int x, int dx, int bound)
{
    int mul = 0;
    if (dx == 0)
    {
        return 0;
    }
    while (x + dx * mul >= 0 && x + dx * mul < bound)
    {
        mul--;
    }

    return mul;
}

static int HighestMultiplier(int x, int dx, int bound)
{
    int mul = 0;
    if (dx == 0)
    {
        return 0;
    }
    while (x + dx * mul >= 0 && x + dx * mul < bound)
    {
        mul++;
    }
    return mul;
}
