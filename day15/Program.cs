using System.Text;

var input = File.ReadLines(args[0]);
var matrix = new List<List<char>>();
(int x, int y) pos = (0, 0);

var l = 0;
for (; l < input.Count(); l++)
{
    if (string.IsNullOrWhiteSpace(input.ElementAt(l)))
    {
        break;
    }
    matrix.Add([]);
    var line = input.ElementAt(l);
    for (var x = 0; x < input.ElementAt(l).Length; x++)
    {
        char c = line[x];
        switch (c)
        {
            case '#':
                matrix[l].Add('#');
                matrix[l].Add('#');
                break;
            case 'O':
                matrix[l].Add('[');
                matrix[l].Add(']');
                break;
            case '.':
                matrix[l].Add('.');
                matrix[l].Add('.');
                break;
            case '@':
                pos.x = x * 2;
                pos.y = l;
                matrix[l].Add('@');
                matrix[l].Add('.');
                break;
        }
    }
}

StringBuilder move = new();
for (; l < input.Count(); l++)
{
    move.Append(input.ElementAt(l));
}

var moves = move.ToString();
var steps = 0;
foreach (var c in moves)
{
    steps++;
    if (CanMove(matrix, pos.x, pos.y, c))
    {
        Console.WriteLine(steps + " " + c + pos);
        Move(matrix, pos.x, pos.y, c);
        pos.x += c == '>' ? 1 : 0;
        pos.x += c == '<' ? -1 : 0;
        pos.y += c == '^' ? -1 : 0;
        pos.y += c == 'v' ? 1 : 0;
    }
    PrintMap(matrix);
    Thread.Sleep(100);
    Console.WriteLine();
}

var result = 0;
for (var y = 0; y < matrix.Count; y++)
{
    for (var x = 0; x < matrix[y].Count; x++)
    {
        if (matrix[y][x] == 'O')
        {
            result += 100 * y + x;
        }
    }
}

Console.WriteLine(result);


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

void Move(List<List<char>> map, int x, int y, char dir)
{
    if (map[y][x] == '.')
    {
        Switch(map, x, y, dir);
        return;
    }
    if (dir == '>')
    {
        Move(map, x + 1, y, dir);
        Switch(map, x, y, dir);
        return;
    }
    if (dir == '<')
    {
        Move(map, x - 1, y, dir);
        Switch(map, x, y, dir);
        return;
    }
    if (dir == 'v')
    {
        if (map[y][x] == '[')
        {
            Move(map, x, y + 1, dir);
            Move(map, x + 1, y + 1, dir);
            Switch(map, x, y, dir);
            return;
        }
        if (map[y][x] == ']')
        {
            Move(map, x, y + 1, dir);
            Move(map, x - 1, y + 1, dir);
            Switch(map, x, y, dir);
            return;
        }
        Move(map, x, y + 1, dir);
        Switch(map, x, y, dir);
        return;
    }
    if (dir == '^')
    {
        if (map[y][x] == '[')
        {
            Move(map, x, y - 1, dir);
            Move(map, x + 1, y - 1, dir);
            Switch(map, x, y, dir);
            return;
        }
        if (map[y][x] == ']')
        {
            Move(map, x, y - 1, dir);
            Move(map, x - 1, y - 1, dir);
            Switch(map, x, y, dir);
            return;
        }
        Move(map, x, y - 1, dir);
        Switch(map, x, y, dir);
        return;
    }
}

void Switch(List<List<char>> map, int x, int y, char dir)
{
    (int prevX, int prevY) = (x, y);
    switch (dir)
    {
        case '<':
            prevX++;
            break;
        case '>':
            prevX--;
            break;
        case 'v':
            prevY--;
            break;
        case '^':
            prevY++;
            break;
    }
    map[y][x] = map[prevY][prevX];
    map[prevY][prevX] = '.';
}

bool CanMove(List<List<char>> map, int x, int y, char dir)
{
    if (map[y][x] == '#')
    {
        return false;
    }
    if (map[y][x] == '.')
    {
        return true;
    }
    if (dir == '>')
    {
        return CanMove(map, x + 1, y, dir);
    }
    if (dir == '<')
    {
        return CanMove(map, x - 1, y, dir);
    }
    if (dir == 'v')
    {
        if (map[y][x] == '[')
        {
            return CanMove(map, x, y + 1, dir) && CanMove(map, x + 1, y + 1, dir);
        }
        if (map[y][x] == ']')
        {
            return CanMove(map, x, y + 1, dir) && CanMove(map, x - 1, y + 1, dir);
        }
        return CanMove(map, x, y + 1, dir);
    }
    if (dir == '^')
    {
        if (map[y][x] == '[')
        {
            return CanMove(map, x, y - 1, dir) && CanMove(map, x + 1, y - 1, dir);
        }
        if (map[y][x] == ']')
        {
            return CanMove(map, x, y - 1, dir) && CanMove(map, x - 1, y - 1, dir);
        }
        return CanMove(map, x, y - 1, dir);
    }
    return false;
}

