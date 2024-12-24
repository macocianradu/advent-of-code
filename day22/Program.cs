using day22;

var input = File.ReadAllLines("input/input1.txt");

List<Dictionary<List<long>, long>> prices = new();

var monkey = 0;
foreach (var line in input)
{
    var secret = long.Parse(line);
    var priceChange = new List<long>();
    prices.Add(new (new ListComparer()));
    for (var step = 0; step < 2000; step++)
    {
        var newSecret = Step(secret);
        var change = newSecret%10 - secret%10;
        secret = newSecret;
            priceChange.Add(change);
        if (priceChange.Count < 4)
        {
            continue;
        }

        if (prices[monkey].ContainsKey(priceChange))
        {
            priceChange = [..priceChange.Skip(1)];
            continue;
        }
        prices[monkey].Add([..priceChange], secret%10);
            priceChange = [..priceChange.Skip(1)];
    }
    monkey++;
}

var max = 0L;
Dictionary<List<long>, long> sequences = new(new ListComparer());
foreach (var dict in prices)
{
    foreach (var (sequence, value) in dict)
    {
        if (!sequences.TryAdd(sequence, value))
        {
            sequences[sequence] += value;
        }

        if (max < sequences[sequence])
        {
            max = sequences[sequence];
        }
    }
}
    
Console.WriteLine(max);

long Step(long secret)
{
    long result = secret * 64L;
    secret = Mix(secret, result);
    secret = Prune(secret);
    result = secret / 32;
    secret = Mix(secret, result);
    secret = Prune(secret);
    result = secret * 2048;
    secret = Mix(secret, result);
    secret = Prune(secret);
    return secret;
}

long Mix(long secret, long nr)
{
    return nr ^ secret;
}

long Prune(long nr)
{
    return nr % 16777216;
}

