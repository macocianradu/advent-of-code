using Day14;

var width = 101;
var height = 103;
var input = File.ReadLines(args[0]);
var robots = new List<Robot>();

foreach (var line in input)
{
    var l = line.Split(' ');
    var pos = l[0].Split('=')[1].Split(',');
    var vel = l[1].Split('=')[1].Split(',');

    robots.Add(new((int.Parse(pos[0]),
                    int.Parse(pos[1])),
                (int.Parse(vel[0]),
                 int.Parse(vel[1]))));
}

var q1 = 0;
var q2 = 0;
var q3 = 0;
var q4 = 0;
var steps = 0;
while (true)
{
    steps++;
    robots.ForEach(r =>
            {
                r.Move(1, width, height);
                if (r.Position.x < width / 2)
                {
                    if (r.Position.y < height / 2)
                    {
                        q1++;
                    }
                    else if (r.Position.y > height / 2)
                    {
                        q3++;
                    }
                }
                else if (r.Position.x > width / 2)
                {
                    if (r.Position.y < height / 2)
                    {
                        q2++;
                    }
                    else if (r.Position.y > height / 2)
                    {
                        q4++;
                    }
                }
            });
    foreach (var robo in robots)
    {
        if (robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 1) is not null &&
            robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 2) is not null &&
            robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 3) is not null &&
            robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 4) is not null &&
            robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 5) is not null &&
            robots.Find(r =>
                    r.Position.x == robo.Position.x
                    && r.Position.y == robo.Position.y - 6) is not null)
        {
            Console.Clear();
            Console.WriteLine(steps);
            PrintPosition(robots);
            Thread.Sleep(1000);
        }
    }
}

void PrintPosition(List<Robot> robots)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (robots.Find(e => e.Position.x == x && e.Position.y == y) is null)
            {
                Console.Write('.');
                continue;
            }
            Console.Write('#');
        }
        Console.WriteLine();
    }
}
