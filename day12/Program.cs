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
            Console.Write(matrix[y][x]);
            (int p, int a) = GetRegion((0, 1), x, y, matrix);
            Console.WriteLine(": " + p + " * " + a);
            result += p * a;
        }
    }
}

Console.WriteLine(result);

(int permineter, int area) GetRegion((int perimeter, int area) fence,
        int x,
        int y,
        List<List<char>> map)
{
    char value = map[y][x];
    // Console.Clear();
    // PrintMap(map);
    // Thread.Sleep(500);
    map[y][x] = char.ToLower(value);
    for (int i = 0; i < 4; i++)
    {
        if (!IsInside(x + neighborX[i], y + neighborY[i], map))
        {
            fence.perimeter++;
            continue;
        }
        var neighbor = map[y + neighborY[i]][x + neighborX[i]];
        if (neighbor == value)
        {
            fence = GetRegion((fence.perimeter, fence.area + 1),
                    x + neighborX[i],
                    y + neighborY[i],
                    map);
            continue;
        }
        if (neighbor != map[y][x])
        {
            fence.perimeter++;
        }
    }
    return fence;
}

bool IsInside(int x, int y, List<List<char>> map)
{
    return x >= 0 && y >= 0 && y < map.Count && x < map[y].Count;
}

void PrintMap(List<List<char>> map)
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
