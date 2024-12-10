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
//for (var i = 0; i < solution.Count; i++)
//{
//    if (solution[i] == -1)
//    {
//        Console.Write('.');
//        continue;
//    }
//    Console.Write(solution[i]);
//}
//Console.WriteLine();
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
    var id = ids.Last();

    Console.WriteLine("id: " + id + " size: ");
    while (id >= 0)
    {
        var pos = FindIdPos(ids, id);
        var size = ContinousRegionSize(ids, pos);
        Console.WriteLine("id: " + id + " size: " + size);
        Swap(ids, pos, size);
        id--;
    }
    return ids;
}

static List<int> Swap(List<int> ids, int pos, int size)
{
    var value = ids[pos];
    for (int i = 0; i < pos; i++)
    {
        if (ids[i] != -1)
        {
            continue;
        }
        if (size <= ContinousRegionSize(ids, i))
        {
            while (size > 0)
            {
                ids[i] = value;
                ids[pos] = -1;
                i++;
                pos++;
                size--;
            }
        }
    }
    return ids;
}

static int FindIdPos(List<int> ids, int id)
{
    for (int i = ids.Count - 1; i >= 0; i--)
    {
        if (ids[i] == id)
        {
            while (ids[i - 1] == id && i - 1 > 0)
            {
                i--;
            }
            return i;
        }
    }
    return 0;
}

static int ContinousRegionSize(List<int> ids, int index)
{
    var space = 0;
    var value = ids[index];
    while (index < ids.Count && ids[index] == value)
    {
        space++;
        index++;
    }
    return space;
}
