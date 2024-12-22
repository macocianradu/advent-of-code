var input = File.ReadAllLines("input/input1.txt");

List<List<char>> track = [];
var cheatSize = 20;
List<int> xN = [1, 0, -1, 0];
List<int> yN = [0, 1, 0, -1];
(int x, int y) start = (0, 0);
List<List<int>> distances = [];
Dictionary<(int x, int y), List<((int x, int y) end, int score)>> cheats = [];

for (var i = 0; i < input.Length; i++)
{
    track.Add([]);
    distances.Add([]);
    for (var x = 0; x < input[i].Length; x++)
    {
        track[i].Add(input[i][x]);
        if (track[i][x] == 'S')
        {
            start = (x, i);
        }
        distances[i].Add(int.MaxValue);
    }
}

SetDistances(start, 0, track);

for (int y = 1; y < track.Count - 1; y++)
{
    for (int x = 1; x < track[y].Count - 1; x++)
    {
        if (track[y][x] != '#')
        {
            var possibleCheats = PossibleCheats((x, y), track, cheatSize);
            List<((int x, int y) end, int score)> c = [];
            foreach (var cheat in possibleCheats)
            {
                var newDistance = cheat.distance + distances[y][x];
                if (newDistance < distances[cheat.Item1.y][cheat.Item1.x])
                {
                    c.Add(((cheat.Item1.x, cheat.Item1.y), distances[cheat.Item1.y][cheat.Item1.x] - newDistance));
                }
            }
            cheats.Add((x, y), c);
        }
    }

}

var result = 0;
Dictionary<int, int> results = [];

foreach ((var key, var value) in cheats)
{
    foreach ((var exit, var distance) in value)
    {
        if (distance >= 100)
        {
            result++;
        }
        if (distance == 73)
        {
            Console.WriteLine(key + " - " + exit);
        }
        if (results.TryGetValue(distance, out int t))
        {
            results[distance] = ++t;
            continue;
        }
        results.Add(distance, 1);
    }
}

foreach ((var key, var value) in results)
{
    if (key > 50)
    {
        Console.WriteLine(key + " - " + value);
    }
}


Console.WriteLine(result);

void SetDistances((int x, int y) position,
        int distance,
        List<List<char>> map)
{
    if (map[position.y][position.x] == '#')
    {
        return;
    }
    if (distances[position.y][position.x] <= distance)
    {
        return;
    }
    distances[position.y][position.x] = distance;
    if (map[position.y][position.x] == 'E')
    {
        return;
    }
    for (int i = 0; i < 4; i++)
    {
        var n = position;
        n.x += xN[i];
        n.y += yN[i];
        if (Inside(n, map))
        {
            SetDistances(n, distance + 1, map);
        }
    }
}

List<((int x, int y), int distance)> PossibleCheats((int x, int y) position,
        List<List<char>> map,
        int size)
{
    List<((int x, int y), int distance)> possible = [];
    for (int x = position.x - size; x <= position.x + size; x++)
    {
        for (int y = position.y - size; y <= position.y + size; y++)
        {
            if (Inside((x, y), map))
            {
                if (map[y][x] != '#')
                {
                    var dist = Math.Abs(position.x - x) + Math.Abs(position.y - y);
                    if (dist <= size)
                    {
                        possible.Add(((x, y),
                            dist));
                    }
                }
            }
        }
    }
    return possible;
}

static bool Inside<T>((int x, int y) position, List<List<T>> map)
{
    return position.y >= 0 && position.y < map.Count
        && position.x >= 0 && position.x < map[position.y].Count;
}

static void PrintCheats(Dictionary<(int x, int y), int> cheats)
{
    foreach ((var key, var value) in cheats)
    {
        Console.WriteLine(key + " - " + value + ", ");
    }
}
