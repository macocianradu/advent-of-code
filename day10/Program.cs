var input = File.ReadLines(args[0]);
var matrix = new List<List<int>>();
List<int> neighborX = [-1, 1, 0, 0];
List<int> neighborY = [0, 0, -1, 1];

for (var y = 0; y < input.Count(); y++)
{
    matrix.Add([]);
    var line = input.ElementAt(y);
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        int c = line[x] - '0';
        matrix[y].Add(c);
    }
}
var result = 0;

var heads = GetTrailheads(matrix);
heads.ForEach(h =>
{
    result += GetTrailheadScore(h.x, h.y, matrix, []).Count;
});

Console.WriteLine(result);

List<(int x, int y)> GetTrailheads(List<List<int>> map)
{
    List<(int x, int y)> heads = [];
    for (int y = 0; y < map.Count; y++)
    {
        for (int x = 0; x < map[y].Count; x++)
        {
            if (map[y][x] == 0)
            {
                heads.Add((x, y));
            }
        }
    }
    return heads;
}

HashSet<(int x, int y)> GetTrailheadScore(int x, int y, List<List<int>> map, HashSet<(int x, int y)> result)
{
    if (map[y][x] == 9)
    {
        result.Add((x, y));
        return result;
    }
    int value = map[y][x];
    for (int i = 0; i < 4; i++)
    {
        if (Inside(x + neighborX[i], y + neighborY[i], map))
        {
            var nextVal = map[y + neighborY[i]][x + neighborX[i]];
            if (nextVal == value + 1)
            {
                var peaks = GetTrailheadScore(x + neighborX[i], y + neighborY[i], map, result);
                foreach (var peak in peaks)
                {
                    result.Add(peak);
                }
            }
        }
    }
    return result;

}

bool Inside(int x, int y, List<List<int>> map)
{
    return x >= 0 && y >= 0 && y < map.Count && x < map[y].Count;
}
