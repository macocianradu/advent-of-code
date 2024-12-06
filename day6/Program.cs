var input = File.ReadLines(args[0]);

//encoding time
int EMPTY = 0;
int UP = 1;
int LEFT = 10;
int RIGHT = 100;
int DOWN = 1000;
int OBSTACLE = 10000;

var matrix = new List<List<int>>();
var objs = new List<(int x, int y)>();

int startX = 0;
int startY = 0;
char direction = '.';
for (var y = 0; y < input.Count(); y++)
{
    matrix.Add([]);
    var line = input.ElementAt(y);
    for (var x = 0; x < input.ElementAt(y).Length; x++)
    {
        char c = line[x];
        if (c == '.')
        {
            matrix[y].Add(EMPTY);
        }
        if (c == '#')
        {
            matrix[y].Add(OBSTACLE);
        }
        if (c == '^')
        {
            matrix[y].Add(UP);
            startX = x;
            startY = y;
            direction = c;
        }
        if (c == '>')
        {
            matrix[y].Add(RIGHT);
            startX = x;
            startY = y;
            direction = c;
        }
        if (c == 'v')
        {
            matrix[y].Add(DOWN);
            startX = x;
            startY = y;
            direction = c;
        }
        if (c == '<')
        {
            matrix[y].Add(LEFT);
            startX = x;
            startY = y;
            direction = c;
        }
    }
}

Walk(startX, startY, direction, matrix, 0, true);
Console.WriteLine(objs.Contains((startX, startY)));
Console.WriteLine(objs.Count);

//true loop
//false exit
bool Walk(int x, int y, char dir, List<List<int>> matrix, int id, bool parent)
{
    while (!Finish(x, y, matrix))
    {
        //if (id == 41)
        //{
        //    Console.Clear();
        //    PrintState(x, y, dir, matrix);
        //    Console.WriteLine(id);
        //    Thread.Sleep(500);
        //}
        var (dX, dY) = GetDirection(dir);
        if (!IsBlocked(x + dX, y + dY, matrix))
        {
            if (parent)
            {
                if (!Finish(x + dX, y + dY, matrix))
                {
                    var copy = CopyMatrix(matrix);
                    copy[y + dY][x + dX] = OBSTACLE;
                    if (Walk(x, y, dir, copy, id++, false))
                    {
                        if (!objs.Contains((x + dX, y + dY)))
                        {
                            objs.Add((x + dX, y + dY));
                        }
                    }
                }
            }
            var value = GetDirValue(dir);
            x += dX;
            y += dY;
            if (Finish(x, y, matrix))
            {
                break;
            }
            if (Walked(matrix[y][x], value))
            {
                return true;
            }
            matrix[y][x] += GetDirValue(dir);
            continue;
        }
        dir = GetNextDirection(dir);
    }
    return false;
}

bool Walked(int cellValue, int dirValue)
{
    var modValue = dirValue == 1 ? 2 : dirValue;
    return cellValue / dirValue % modValue == 1;
}

void PrintState(int x, int y, char dir, List<List<int>> matrix)
{
    for (int j = 0; j < matrix.Count; j++)
    {
        for (int i = 0; i < matrix[y].Count; i++)
        {
            Console.Write(' ');
            if (j == y && x == i)
            {
                Console.Write(direction);
                continue;
            }
            Console.Write(GetChar(matrix[j][i]));
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


bool IsBlocked(int x, int y, List<List<int>> matrix)
{
    if (Finish(x, y, matrix))
    {
        return false;
    }
    return matrix[y][x] == OBSTACLE;
}


bool Finish(int x, int y, List<List<int>> matrix)
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

int GetDirValue(char c)
{
    switch (c)
    {
        case '^': { return UP; }
        case '>': { return RIGHT; }
        case '<': { return LEFT; }
        case 'v': { return DOWN; }
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

List<List<int>> CopyMatrix(List<List<int>> matrix)
{
    var copy = new List<List<int>>();
    for (int y = 0; y < matrix.Count; y++)
    {
        copy.Add([]);
        for (int x = 0; x < matrix[y].Count; x++)
        {
            copy[y].Add(matrix[y][x]);
        }
    }
    return copy;
}

char GetChar(int cell)
{
    switch (cell)
    {
        case 0: return '.';
        case 1: return '↑';
        case 10: return '←';
        case 11: return '↖';
        case 100: return '→';
        case 101: return '↗';
        case 110: return '⇄';
        case 111: return '↟';
        case 1000: return '↓';
        case 1001: return '↕';
        case 1010: return '↙';
        case 1011: return '↞';
        case 1100: return '↘';
        case 1101: return '↠';
        case 1110: return '↡';
        case 1111: return '*';
        case 10000: return '#';
        default: throw new Exception("Invalid value");
    }
}
