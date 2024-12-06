var input = File.ReadLines(args[0]);

var matrix = new List<List<char>>();
var visited = new List<List<(bool u, bool l, bool r, bool d)>>();

(int x, int y) position = (0, 0);
char direction = '.';
for (var y = 0; y < input.Count(); y++)
{
    matrix.Add([]);
    visited.Add([]);
    var line = input.ElementAt(y);
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        char c = line[x];
        matrix[y].Add(c);
        visited[y].Add((false, false, false, false));
        if (c == '^' || c == '>' || c == 'v' || c == '<')
        {
            position = (x, y);
            direction = c;
        }
    }
}

var startPos = position;
var result = 0;

while (!Finish(position.x, position.y, matrix))
{
    visited[position.y][position.x] = MarkVisited(direction, visited[position.y][position.x]);
    if (PossibleLoop(position.x, position.y, direction, matrix))
    {
        var objPos = (position.x + GetDirection(direction).x, position.y + GetDirection(direction).y);
        if (objPos != startPos)
        {
            result++;
        }

    }
    var (x, y) = GetDirection(direction);
    if (!IsBlocked(position.x + x, position.y + y, matrix))
    {
        position = (position.x + x, position.y + y);
        continue;
    }
    direction = GetNextDirection(direction);
}

Console.WriteLine(result);

bool IsBlocked(int x, int y, List<List<char>> matrix)
{
    if (Finish(x, y, matrix))
    {
        return false;
    }
    return matrix[y][x] == '#';
}

bool PossibleLoop(int x, int y, char dir, List<List<char>> matrix)
{
    char nextD = GetNextDirection(dir);
    (int x, int y) pos = (x, y);
    while (!Finish(pos.x, pos.y, matrix))
    {
        if (Visited(nextD, visited[pos.y][pos.x]))
        {
            return true;
        }
        var (pX, pY) = GetDirection(nextD);
        if (!IsBlocked(pos.x + pX, pos.y + pY, matrix))
        {
            pos = (pos.x + pX, pos.y + pY);
            continue;
        }
        nextD = GetNextDirection(nextD);
    }
    return false;
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

bool Visited(char direction, (bool u, bool l, bool r, bool d) mark)
{
    switch (direction)
    {
        case '^': { return mark.u; }
        case '>': { return mark.r; }
        case '<': { return mark.l; }
        case 'v': { return mark.d; }
        default:
            {
                throw new Exception($"Char {direction} is not a valid guard direction");
            }
    }
}

(bool u, bool l, bool r, bool d) MarkVisited(char direction, (bool u, bool l, bool r, bool d) prev)
{
    switch (direction)
    {
        case '^':
            {
                prev.u = true;
                break;
            }
        case '>':
            {
                prev.r = true;
                break;
            }
        case '<':
            {
                prev.l = true;
                break;
            }
        case 'v':
            {
                prev.d = true;
                break;
            }
    }
    return prev;
}
