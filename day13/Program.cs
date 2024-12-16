using Day13;

var input = File.ReadAllLines(args[0]);

List<Machine> machines = [];
for (var line = 0; line < input.Length; line++)
{
    var a = input[line].Split(':')[1].Split(',');
    var ax = int.Parse(a[0].Split('+')[1]);
    var ay = int.Parse(a[1].Split('+')[1]);

    var b = input[line + 1].Split(':')[1].Split(',');
    var bx = int.Parse(b[0].Split('+')[1]);
    var by = int.Parse(b[1].Split('+')[1]);

    var prize = input[line + 2].Split(':')[1].Split(',');
    var prizex = int.Parse(prize[0].Split('=')[1]);
    var prizey = int.Parse(prize[1].Split('=')[1]);
    machines.Add(new((ax, ay), (bx, by), (prizex, prizey)));

    line += 3;
}

long result = 0;

machines.ForEach(m =>
{
    var tokens = m.GetFewestTokens();
    if (tokens != long.MaxValue)
    {
        result += tokens;
    }
});

Console.WriteLine(result);
