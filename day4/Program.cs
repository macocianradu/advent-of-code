var input = File.ReadLines(args[0]);

var matrix = new List<List<char>>();

List<int> xNeighbors = [-1, 0, 1, -1, 1, -1, 0, 1];
List<int> yNeighbors = [-1, -1, -1, 0, 0, 1, 1, 1];

for (var line = 0; line < input.Count(); line++)
{
    matrix.Add([]);
    foreach (char c in input.ElementAt(line))
    {
        matrix[line].Add(c);
    }
}

var result = 0;

for (var y = 0; y < matrix.Count; y++)
{
    for (var x = 0; x < matrix[y].Count; x++)
    {
        for (var dir = 0; dir < xNeighbors.Count; dir++)
        {
            result += ValidStrings(x, y, matrix, "XMAS", dir);
        }
    }
}


Console.WriteLine(result);

int ValidStrings(int x, int y, List<List<char>> matrix, string check, int dir)
{
    int result = 0;
    if (matrix[y][x] != check[0])
    {
        return result;
    }
    check = new string(check.Skip(1).ToArray());
    if (string.IsNullOrWhiteSpace(check))
    {
        return 1;
    }
    if (IsInside(x + xNeighbors[dir], y + yNeighbors[dir], matrix))
    {
        result += ValidStrings(x + xNeighbors[dir], y + yNeighbors[dir], matrix, check, dir);
    }
    return result;

}

bool IsInside(int x, int y, List<List<char>> matrix)
{
    if (y >= 0 && y < matrix.Count)
    {
        return x >= 0 && x < matrix[y].Count;
    }
    return false;
}
