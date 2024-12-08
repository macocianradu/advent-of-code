var input = File.ReadLines(args[0]);
var matrix = new List<List<char>>();
(int x, int y) position = (0, 0);
List<(int x, int y)> path = [];
char direction = '.';
for (var y = 0; y < input.Count(); y++)
{
    matrix.Add([]);
    var line = input.ElementAt(y);
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        char c = line[x];
        matrix[y].Add(c);
        if (c == '^' || c == '>' || c == 'v' || c == '<')
        {
            position = (x, y);
            direction = c;
        }
    }
}


var original = CopyMatrix(matrix);

var (startX, startY) = position;
var startDir = direction;

List<(int x, int y)> Walk(int x, int y, char dir, List<List<char>> matrix)
{
    var path = new List<(int x, int y)>();
    while (!Finish(x, y, matrix))
    {
        var (dX, dY) = GetDirection(dir);

        if (!path.Contains((x, y)))
        {
            path.Add((x, y));
        }
        if (!IsBlocked(x + dX, y + dY, matrix))
        {
            x += dX;
            y += dY;
            continue;
        }
        dir = GetNextDirection(dir);
    }
    return path;
}
path = Walk(position.x, position.y, direction, matrix);

var options = new List<List<List<char>>>();
path.ForEach((p) =>
{
    var copy = CopyMatrix(original);
    copy[p.y][p.x] = '#';
    options.Add(copy);
});

var result = 0;
//options.ForEach(o => { if (IsLoop(startX, startY, startDir, o)) result++; });
Parallel.ForEach(options, option =>
{
    if (IsLoop(startX, startY, startDir, option))
    {
        Interlocked.Increment(ref result);
    }
});

Console.WriteLine(result);

bool IsLoop(int startX, int startY, char dir, List<List<char>> matrix)
{
    int steps = 0;
    while (!Finish(startX, startY, matrix)
            && steps <= matrix[0].Count * matrix[0].Count)
    {
        var (x, y) = GetDirection(dir);

        if (!IsBlocked(startX + x, startY + y, matrix))
        {
            startX = startX + x;
            startY = startY + y;
            steps++;
            continue;
        }
        dir = GetNextDirection(dir);
        steps++;
    }

    return steps >= matrix[0].Count * matrix[0].Count;
}

Console.WriteLine(path.Count);

bool IsBlocked(int x, int y, List<List<char>> matrix)
{
    if (Finish(x, y, matrix))
    {
        return false;
    }
    return matrix[y][x] == '#';
}

bool Finish(int x, int y, List<List<char>> matrix)
{
    return x < 0
    || y < 0
    || y >= matrix.Count
    || x >= matrix[y].Count;
}

char GetNextDirection(char c)
{
    switch (c)
    {
        case '^': { return '>'; }
        case '>': { return 'v'; }
        case '<': { return '^'; }
        case 'v': { return '<'; }
        default:
            {
                throw new Exception($"Char {c} is not a valid guard direction");
            }
    }
}

(int x, int y) GetDirection(char c)
{
    switch (c)
    {
        case '^': { return (0, -1); }
        case '>': { return (1, 0); }
        case '<': { return (-1, 0); }
        case 'v': { return (0, 1); }
        default:
            {
                throw new Exception($"Char {c} is not a valid guard direction");
            }
    }
}

List<List<char>> CopyMatrix(List<List<char>> original)
{
    List<List<char>> copy = [];
    for (int y = 0; y < matrix.Count; y++)
    {
        copy.Add([]);
        for (int x = 0; x < matrix[y].Count; x++)
        {
            copy[y].Add(original[y][x]);
        }
    }
    return copy;
}
