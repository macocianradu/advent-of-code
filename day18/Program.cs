var input = File.ReadAllLines(args[0]);

var width = int.Parse(input[0]);
var height = int.Parse(input[1]);

List<int> xN = [1, 0, -1, 0];
List<int> yN = [0, 1, 0, -1];

Queue<(int x, int y)> bytes = [];
for (int l = 2; l < input.Length; l++)
{
    var line = input.ElementAt(l).Split(',');
    bytes.Enqueue((int.Parse(line[0]), int.Parse(line[1])));
}

List<List<bool>> map = [];
List<List<((int x, int y) from, int distance)>> distances = [];


for (int i = 0; i < width; i++)
{
    map.Add([]);
    distances.Add([]);
    for (int j = 0; j < height; j++)
    {
        map[i].Add(false);
        distances[i].Add(((-1, -1), int.MaxValue));
    }
}

SetDistances((0, 0), ((-1, -1), 0), map);
List<(int x, int y)> path = GetPath(distances);
for (int step = 0; step < int.MaxValue; step++)
{
    var (x, y) = bytes.Dequeue();
    map[y][x] = true;
    if (path.Contains((x, y)))
    {
        ResetDistances();
        SetDistances((0, 0), ((-1, -1), 0), map);
        path = GetPath(distances);
    }
    if (distances[height - 1][width - 1].distance == int.MaxValue)
    {
        Console.WriteLine((x, y));
        break;
    }
}

List<(int x, int y)> GetPath(List<List<((int x, int y) from, int distance)>> map)
{
    List<(int x, int y)> path = [];
    int x = width - 1;
    int y = height - 1;
    while (map[y][x].from != (-1, -1))
    {
        path.Add(map[y][x].from);
        (x, y) = map[y][x].from;
    }
    return path;
}

void ResetDistances()
{
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            distances[i][j] = ((-1, -1), int.MaxValue);
        }
    }
}

void SetDistances((int x, int y) position,
        ((int x, int y) from, int distance) distance,
        List<List<bool>> map)
{
    if (map[position.y][position.x])
    {
        return;
    }
    if (distances[position.y][position.x].distance <= distance.distance)
    {
        return;
    }
    distances[position.y][position.x] = distance;
    if (position == (width - 1, height - 1))
    {
        return;
    }
    for (int i = 0; i < 4; i++)
    {
        if (distances[height - 1][width - 1].distance < int.MaxValue)
        {
            return;
        }
        var n = position;
        n.x += xN[i];
        n.y += yN[i];
        if (Inside(n, map))
        {
            SetDistances(n, (position, distance.distance + 1), map);
        }
    }
}

static bool Inside((int x, int y) position, List<List<bool>> map)
{
    return position.y >= 0 && position.y < map.Count
        && position.x >= 0 && position.x < map[position.y].Count;
}

static void PrintMap(List<List<bool>> map)
{
    for (int y = 0; y < map.Count; y++)
    {
        Console.WriteLine();
        for (int x = 0; x < map[y].Count; x++)
        {
            Console.Write(map[y][x] ? '#' : '.');
        }
        Console.WriteLine();
    }
}

static void PrintDistances(List<List<((int x, int y) from, int distance)>> distances)
{
    for (int y = 0; y < distances.Count; y++)
    {
        Console.WriteLine();
        for (int x = 0; x < distances[y].Count; x++)
        {
            Console.Write(distances[y][x].distance.ToString().PadLeft(3, '0') + " ");
        }
        Console.WriteLine();
    }
}
