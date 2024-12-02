var input = File.ReadLines("input/input1.txt");

var result = 0;

var lineNr = 0;
foreach (var line in input)
{
    var levels = line.Split(' ').Select(int.Parse).ToList();
    if (LineIsSafe(levels, false))
    {
        result++;
    }
    lineNr++;
}

Console.WriteLine(result);

bool LineIsSafe(List<int> levels, bool dampened)
{
    bool increase = levels[0] < levels[1];
    for (var i = 0; i < levels.Count - 1; i++)
    {
        if (increase && levels[i] > levels[i + 1])
        {
            if (dampened)
            {
                return false;
            }
            return SplitIsSafe(levels, i);
        }

        if (!increase && levels[i] < levels[i + 1])
        {
            if (dampened)
            {
                return false;
            }
            return SplitIsSafe(levels, i);
        }

        var dif = Math.Abs(levels[i] - levels[i + 1]);
        if (dif == 0 || dif > 3)
        {
            if (dampened)
            {
                return false;
            }
            return SplitIsSafe(levels, i);
        }
    }

    return true;
}

bool SplitIsSafe(List<int> levels, int i)
{
    var lines = new List<List<int>>();
    var newLine1 = new List<int>(levels);
    var newLine2 = new List<int>(levels);
    if (i > 0)
    {
        lines.Add(new List<int>(levels));
        lines[0].RemoveAt(i - 1);
    }
    newLine1.RemoveAt(i);
    newLine2.RemoveAt(i + 1);
    lines.Add(newLine1);
    lines.Add(newLine2);

    if (i < levels.Count - 1)
    {
        var line = new List<int>(levels);
        line.RemoveAt(i + 1);
        lines.Add(line);
    }
    return lines.Any(l => LineIsSafe(l, true));
}

string ListString(List<int> list) =>
        list.Select(l => l.ToString()).Aggregate((l1, l2) => l1 + " " + l2);
