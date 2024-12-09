var input = File.ReadLines(args[0]);
List<char> disk = [.. input.ElementAt(0)];
List<int> ids = [];
for (var id = 0; id < disk.Count; id += 2)
{
    int size = disk[id] - '0';
    while (size > 0)
    {
        ids.Add(id / 2);
        size--;
    }
    if (id + 1 < disk.Count)
    {
        size = disk[id + 1] - '0';
        while (size > 0)
        {
            ids.Add(-1);
            size--;
        }
    }
}

var solution = Arrange(ids);
var result = CheckSum(solution);
Console.WriteLine(result);

static double CheckSum(List<int> ids)
{
    double result = 0;
    for (int i = 0; i < ids.Count; i++)
    {
        if (ids[i] == -1)
        {
            continue;
        }
        result += i * ids[i];
    }
    return result;
}

static List<int> Arrange(List<int> ids)
{
    for (int i = 1; i < ids.Count; i++)
    {
        if (ids[i] > -1)
        {
            continue;
        }
        var pos = FindLastIdPos(ids);
        if (pos < i)
        {
            break;
        }
        ids[i] = ids[pos];
        ids[pos] = -1;
    }
    return ids;
}

static int FindLastIdPos(List<int> ids)
{
    for (int i = ids.Count - 1; i >= 0; i--)
    {
        if (ids[i] > -1)
        {
            return i;
        }
    }
    return 0;
}
