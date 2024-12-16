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
robots.ForEach(r =>
        {
            var (x, y) = r.Move(100, width, height);
            Console.WriteLine((x, y));
            if (x < width / 2)
            {
                if (y < height / 2)
                {
                    q1++;
                }
                else if (y > height / 2)
                {
                    q3++;
                }
            }
            else if (x > width / 2)
            {
                if (y < height / 2)
                {
                    q2++;
                }
                else if (y > height / 2)
                {
                    q4++;
                }
            }
        });

Console.WriteLine(q1 * q2 * q3 * q4);

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
