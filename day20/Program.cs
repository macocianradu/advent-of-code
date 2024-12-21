var input = File.ReadAllLines(args[0]);

List<List<char>> track = [];
List<int> xN = [1, 0, -1, 0];
List<int> yN = [0, 1, 0, -1];
(int x, int y) start = (0, 0);
(int x, int y) end = (0, 0);
List<List<int>> distances = [];
Dictionary<(int x, int y), int> cheats = [];

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
        if (track[i][x] == 'E')
        {
            end = (x, i);
        }
        distances[i].Add(int.MaxValue);
    }
}

SetDistances(start, 0, track);

for (int y = 1; y < track.Count - 1; y++)
{
    for (int x = 1; x < track[y].Count - 1; x++)
    {
        for (int i = 0; i < 4; i++)
        {
            if (PossibleCheat((x, y), track, i))
            {
                (int x, int y) enter = (x - xN[i], y - yN[i]);
                (int x, int y) exit = (x + xN[i], y + yN[i]);
                var newDistance = distances[enter.y][enter.x] + 2;
                if (distances[exit.y][exit.x] > newDistance)
                {
                    cheats.Add((x, y), distances[exit.y][exit.x] - newDistance);
                }
            }
        }
    }

}

var result = 0;
Dictionary<int, int> results = [];

foreach ((var key, var value) in cheats)
{
    if (value >= 100)
    {
        result++;
    }
    if (results.TryGetValue(value, out int t))
    {
        results[value] = ++t;
        continue;
    }
    results.Add(value, 1);
}

//foreach ((var key, var value) in results)
//{
//    Console.WriteLine(key + " - " + value);
//}


Console.WriteLine(result);

void ResetDistances()
{
    for (int i = 0; i < distances.Count; i++)
    {
        for (int j = 0; j < distances[i].Count; j++)
        {
            distances[i][j] = int.MaxValue;
        }
    }
}

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

bool PossibleCheat((int x, int y) position, List<List<char>> map, int dir)
{
    if (cheats.ContainsKey((position.x, position.y)))
    {
        return false;
    }
    if (map[position.y][position.x] != '#')
    {
        return false;
    }
    (int x, int y) enter = (position.x - xN[dir], position.y - yN[dir]);
    (int x, int y) exit = (position.x + xN[dir], position.y + yN[dir]);
    return Inside(enter, map) && map[enter.y][enter.x] != '#' &&
        Inside(exit, map) && map[exit.y][exit.x] != '#';
}

static bool Inside<T>((int x, int y) position, List<List<T>> map)
{
    return position.y >= 0 && position.y < map.Count
        && position.x >= 0 && position.x < map[position.y].Count;
}

static List<List<T>> Clone<T>(List<List<T>> original)
{
    List<List<T>> clone = [];
    for (int y = 0; y < original.Count; y++)
    {
        clone.Add([]);
        for (int x = 0; x < original[y].Count; x++)
        {
            clone[y].Add(original[y][x]);
        }
    }
    return clone;
}

static void PrintCheats(Dictionary<(int x, int y), int> cheats)
{
    foreach ((var key, var value) in cheats)
    {
        Console.WriteLine(key + " - " + value + ", ");
    }
}
