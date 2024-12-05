using System.Collections.Immutable;

var input = File.ReadLines(args[0]);

var rules = new Dictionary<int, List<int>>();
var updates = new List<List<int>>();
var correct = new List<int>();
bool rulesFinished = false;

foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        rulesFinished = true;
        continue;
    }
    if (!rulesFinished)
    {
        var pages = line.Split('|');
        var p1 = int.Parse(pages[0]);
        if (rules.TryGetValue(p1, out List<int>? value))
        {
            value.Add(int.Parse(pages[1]));
        }
        else
        {
            rules.Add(p1, [int.Parse(pages[1])]);
        }
        continue;
    }
    updates.Add(new List<int>(line.Split(',').Select(int.Parse).ToArray()));
}

var result = 0;
foreach (var line in updates)
{
    result += LineCorrect(line);
}

Console.WriteLine(result);

int LineCorrect(List<int> line)
{
    var corrected = false;
    var ok = false;
    while (!ok)
    {
        ok = true;
        for (int i = 0; i < line.Count; i++)
        {
            if (rules.TryGetValue(line[i], out List<int>? values))
            {
                foreach (var value in values)
                {
                    if (line.Take(i).Contains(value))
                    {
                        corrected = true;
                        line.Remove(value);
                        if (i + 1 > line.Count)
                        {
                            line.Add(value);
                        }
                        else
                        {
                            line.Insert(i + 1, value);
                        }
                        ok = false;
                        break;
                    }
                }
            }
        }
    }
    if (!corrected)
    {
        return 0;
    }
    return line.Skip(line.Count / 2).ToList()[0];
}
