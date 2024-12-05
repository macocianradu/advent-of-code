var input = File.ReadLines(args[0]);

var matrix = new List<List<char>>();

List<int> xNeighbors = [-1, 0, 1, -1, 1, -1, 0, 1];
List<int> yNeighbors = [-1, -1, -1, 0, 0, 1, 1, 1];
List<int> leftDiags = [0, 7];
List<int> rightDiags = [2, 5];

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
        if (matrix[y][x] == 'A')
        {
            bool left = false;
            for (var dir = 0; dir < leftDiags.Count; dir++)
            {
                var xNeighbor = xNeighbors[leftDiags[dir]];
                var yNeighbor = yNeighbors[leftDiags[dir]];
                if (!IsInside(x - xNeighbor, y - yNeighbor, matrix))
                {
                    continue;
                }
                if (ValidStrings(x - xNeighbor, y - yNeighbor, matrix, "MAS", leftDiags[dir]) > 0)
                {
                    left = true;
                    break;
                }
            }
            if (!left)
            {
                continue;
            }

            bool right = false;
            for (var dir = 0; dir < rightDiags.Count; dir++)
            {
                var xNeighbor = xNeighbors[rightDiags[dir]];
                var yNeighbor = yNeighbors[rightDiags[dir]];
                if (!IsInside(x - xNeighbor, y - yNeighbor, matrix))
                {
                    continue;
                }
                if (ValidStrings(x - xNeighbor, y - yNeighbor, matrix, "MAS", rightDiags[dir]) > 0)
                {
                    right = true;
                }
            }
            result += right ? 1 : 0;
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
