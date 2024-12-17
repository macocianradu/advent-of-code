using day16;

var input = File.ReadLines(args[0]);
var maze = new List<List<char>>();

Raindeer raindeer = default!;
List<List<(List<(int x, int y)> path, long distance)>> distance = [];
(int x, int y) end = (0, 0);
for (var l = 0; l < input.Count(); l++)
{
    var line = input.ElementAt(l);
    maze.Add([]);
    distance.Add([]);
    for (var x = 0; x < line.Length; x++)
    {
        char c = line[x];
        maze[l].Add(c);
        distance[l].Add(([], -1));
        if (c == 'S')
        {
            raindeer = new Raindeer(x, l);
        }
        if (c == 'E')
        {
            end = (x, l);
        }
    }
}

var counter = 0;
Move(maze, distance, [], raindeer.X, raindeer.Y, 0, 1);
Console.WriteLine(distance[end.y][end.x].distance);
HashSet<(int x, int y)> distinctNodes = [];

distance[end.y][end.x].path.ForEach(e => distinctNodes.Add(e));
Console.WriteLine(distance[end.y][end.x].distance + ": " + (distinctNodes.Count + 1));

static void PrintMap<T>(List<List<T>> map)
{
    for (var y = 0; y < map.Count; y++)
    {
        for (var x = 0; x < map[y].Count; x++)
        {
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
}


static void PrintPaths(List<List<char>> map, List<(int x, int y)> paths)
{
    for (var y = 0; y < map.Count; y++)
    {
        for (var x = 0; x < map[y].Count; x++)
        {
            if (paths.Contains((x, y)))
            {
                Console.Write('O');
                continue;
            }
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
}

static void PrintDistances(List<List<(List<(int x, int y)> path, long distance)>> complete)
{

    var distances = complete.Select(e => e.Select(e => e.distance).ToList()).ToList();
    for (var y = 0; y < distances.Count; y++)
    {
        for (var x = 0; x < distances[y].Count; x++)
        {
            Console.Write(distances[y][x].ToString().PadRight(5) + " ");
        }
        Console.WriteLine();
    }
}

long Max(List<List<(List<(int x, int y)> path, long distance)>> distances)
{
    long max = -1;
    var d = distances.Select(e => e.Select(e => e.distance).ToList()).ToList();
    for (var y = 0; y < d.Count; y++)
    {
        for (var x = 0; x < d[y].Count; x++)
        {
            if (max < d[y][x])
            {
                max = d[y][x];
            }
        }
    }
    return max;
}

void Move(List<List<char>> maze,
        List<List<(List<(int x, int y)> path, long distance)>> distances,
        List<(int x, int y)> visited,
        int x,
        int y,
        int distance,
        int dir)
{
    if (maze[y][x] == '#')
    {
        return;
    }
    counter++;
    if (counter == 1000000)
    {
        Console.WriteLine(Max(distances));
        counter = 0;
    }
    if (distances[y][x].distance != -1 && distance > distances[y][x].distance + 2000)
    {
        return;
    }
    if (distance == distances[y][x].distance)
    {
        distances[y][x].path.AddRange(Clone(visited));
    }
    else if (distance < distances[y][x].distance || distances[y][x].distance == -1)
    {
        distances[y][x] = (Clone(visited), distance);
    }
    if (maze[y][x] == 'E')
    {
        return;
    }
    visited.Add((x, y));
    switch (dir)
    {
        case 0:
            if (!visited.Contains((x, y - 1)))
            {
                Move(maze, distances, Clone(visited), x, y - 1, distance + 1, 0);
            }
            if (!visited.Contains((x + 1, y)))
            {
                Move(maze, distances, Clone(visited), x + 1, y, distance + 1001, 1);
            }
            if (!visited.Contains((x - 1, y)))
            {
                Move(maze, distances, Clone(visited), x - 1, y, distance + 1001, 3);
            }
            break;
        case 1:
            if (!visited.Contains((x, y - 1)))
            {
                Move(maze, distances, Clone(visited), x, y - 1, distance + 1001, 0);
            }
            if (!visited.Contains((x, y + 1)))
            {
                Move(maze, distances, Clone(visited), x, y + 1, distance + 1001, 2);
            }
            if (!visited.Contains((x + 1, y)))
            {
                Move(maze, distances, Clone(visited), x + 1, y, distance + 1, 1);
            }
            break;
        case 2:
            if (!visited.Contains((x, y + 1)))
            {
                Move(maze, distances, Clone(visited), x, y + 1, distance + 1, 2);
            }
            if (!visited.Contains((x + 1, y)))
            {
                Move(maze, distances, Clone(visited), x + 1, y, distance + 1001, 1);
            }
            if (!visited.Contains((x - 1, y)))
            {
                Move(maze, distances, Clone(visited), x - 1, y, distance + 1001, 3);
            }
            break;
        case 3:
            if (!visited.Contains((x, y - 1)))
            {
                Move(maze, distances, Clone(visited), x, y - 1, distance + 1001, 0);
            }
            if (!visited.Contains((x, y + 1)))
            {
                Move(maze, distances, Clone(visited), x, y + 1, distance + 1001, 2);
            }
            if (!visited.Contains((x - 1, y)))
            {
                Move(maze, distances, Clone(visited), x - 1, y, distance + 1, 3);
            }
            break;
    }
}

static List<(int x, int y)> Clone(List<(int x, int y)> original)
{
    List<(int x, int y)> clone = [];
    foreach (var (x, y) in original)
    {
        clone.Add((x, y));
    }
    return clone;
}
