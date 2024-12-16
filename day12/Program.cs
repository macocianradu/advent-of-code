var input = File.ReadLines(args[0]);
var matrix = new List<List<char>>();
List<int> neighborX = [-1, 1, 0, 0];
List<int> neighborY = [0, 0, -1, 1];

for (var y = 0; y < input.Count(); y++)
{
    matrix.Add([]);
    var line = input.ElementAt(y);
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        char c = line[x];
        matrix[y].Add(c);
    }
}
var result = 0;

for (var y = 0; y < matrix.Count; y++)
{
    for (var x = 0; x < matrix[y].Count; x++)
    {
        if (char.IsUpper(matrix[y][x]))
        {
            var (edges, area) = GetRegion(([], 1), x, y, matrix);
            result += edges.Count * area;
        }
    }
}

Console.WriteLine(result);

(List<(List<(int x, int y)> edge, int dir)> edges, int area) GetRegion(
        (List<(List<(int x, int y)> edge, int dir)> edges, int area) fence,
        int x,
        int y,
        List<List<char>> map)
{
    char value = map[y][x];
    //Console.Clear();
    //PrintMap(map);
    //Thread.Sleep(500);
    map[y][x] = char.ToLower(value);
    for (int i = 0; i < 4; i++)
    {
        if (!IsInside(x + neighborX[i], y + neighborY[i], map))
        {
            AddEdge(fence.edges, x, y, i);
            continue;
        }
        var neighbor = map[y + neighborY[i]][x + neighborX[i]];
        if (neighbor == value)
        {
            fence = GetRegion((fence.edges, fence.area + 1),
                    x + neighborX[i],
                    y + neighborY[i],
                    map);
            continue;
        }
        if (char.ToLower(value) != char.ToLower(neighbor))
        {
            AddEdge(fence.edges, x, y, i);
        }
    }
    return fence;
}

static bool IsInside(int x, int y, List<List<char>> map)
{
    return x >= 0 && y >= 0 && y < map.Count && x < map[y].Count;
}


void AddEdge(List<(List<(int x, int y)> edge, int dir)> edges, int x, int y, int dir)
{
    var lines = edges.FindAll(e => e.dir == dir && (
                e.edge.Contains((x, y + 1)) ||
                e.edge.Contains((x, y - 1)) ||
                e.edge.Contains((x - 1, y)) ||
                e.edge.Contains((x + 1, y)))).ToList();
    if (lines.Count == 0)
    {
        edges.Add(([(x, y)], dir));
        return;
    }
    lines[0].edge.Add((x, y));
    for (int i = 1; i < lines.Count; i++)
    {
        edges.Remove(lines[i]);
        lines[0].edge.AddRange(lines[i].edge);
    }
}

static void PrintEdges(List<(List<(int x, int y)> edge, int dir)> edges)
{
    foreach (var (edge, dir) in edges)
    {
        Console.Write(dir + ": ");
        edge.ForEach(p => Console.Write(p));
        Console.WriteLine();
    }
}

static void PrintMap(List<List<char>> map)
{
    for (int y = 0; y < map.Count; y++)
    {
        for (int x = 0; x < map[y].Count; x++)
        {
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
}
