var input = File.ReadAllLines(args[0]);

List<string> patterns = input[0].Split(',').Select(e => e.Trim()).ToList();
List<string> designs = [];
Dictionary<string, long> possibilities = [];

for (int i = 2; i < input.Length; i++)
{
    designs.Add(input[i]);
}

var possible = 0L;
foreach (var design in designs)
{
    possible += FitDesign(design);
}

Console.WriteLine(possible);

long FitDesign(string design)
{
    if (possibilities.TryGetValue(design, out long value))
    {
        return value;
    }
    if (string.IsNullOrWhiteSpace(design))
    {
        return 1;
    }
    var result = 0L;
    foreach (var pattern in patterns)
    {
        if (design.StartsWith(pattern))
        {
            result += FitDesign(new string(design.Skip(pattern.Length).ToArray()));
        }
    }
    possibilities.Add(design, result);
    return result;
}
