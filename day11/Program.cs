var input = File.ReadLines(args[0]);
string line = new([.. input.ElementAt(0)]);

List<double> stones = new(line.Split(' ').Select(double.Parse).ToList());
Dictionary<(double stone, int blinks), double> cache = [];

double result = 0;
foreach (var stone in stones)
{
    result += Stones(75, stone);
}

Console.WriteLine(result);

double Stones(int blinks, double stone)
{
    if (blinks == 0)
    {
        return 1;
    }
    double res = 0;
    if (cache.ContainsKey((stone, blinks)))
    {
        return cache[(stone, blinks)];
    }
    if (stone == 0)
    {
        res = Stones(blinks - 1, 1);
        cache.Add((stone, blinks), res);
        return res;
    }
    string str = stone.ToString();
    if (str.Length % 2 == 0)
    {
        if (!double.TryParse(str[..(str.Length / 2)], out double newValue))
        {
            newValue = 0;
        }
        double newStone = double.Parse(str[(str.Length / 2)..]);

        res = Stones(blinks - 1, newValue);
        res += Stones(blinks - 1, newStone);
        cache.Add((stone, blinks), res);
        return res;
    }

    res = Stones(blinks - 1, stone * 2024);
    cache.Add((stone, blinks), res);
    return res;
}
