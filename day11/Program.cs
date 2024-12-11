var input = File.ReadLines(args[0]);
string line = new([.. input.ElementAt(0)]);

List<double> stones = line.Split(' ').Select(double.Parse).ToList();

for (int i = 0; i < 75; i++)
{
    Console.WriteLine(i);
    List<double> newStones = [];
    for (int s = 0; s < stones.Count; s++)
    {
        if (stones[s] == 0)
        {
            stones[s] = 1;
            continue;
        }
        var str = stones[s].ToString();
        var digits = str.Length;
        if (digits % 2 == 0)
        {
            if (!double.TryParse(str[..(digits / 2)], out double newValue))
            {
                newValue = 0;
            }
            double newStone = double.Parse(str[(digits / 2)..]);
            newStones.Add(newStone);
            stones[s] = newValue;
            continue;
        }
        stones[s] *= 2024;
    }
    stones.AddRange(newStones);
}

Console.WriteLine(stones.Count);
