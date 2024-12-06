var input = File.ReadLines(args[0]);

var matrix = new List<List<char>>();
(int x, int y) position = (0, 0);
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

var result = 1;

while (!Finish(position.x, position.y, matrix))
{
    var (x, y) = GetDirection(direction);
    matrix[position.y][position.x] = 'x';
    if (!IsBlocked(position.x + x, position.y + y, matrix))
    {
        position = (position.x + x, position.y + y);
        if (!Finish(position.x, position.y, matrix) && matrix[position.y][position.x] != 'x')
        {
            result++;
        }
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
