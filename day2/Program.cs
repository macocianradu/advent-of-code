var input = File.ReadLines("input/input1.txt");

var result = 0;

foreach (var line in input)
{
    if (LineIsSafe(line))
    {
        result++;
    }
}

Console.WriteLine(result);

bool LineIsSafe(string line)
{
    var levels = line.Split(' ').Select(int.Parse).ToList();

    bool increase = levels[0] < levels[1];
    for (var i = 0; i < levels.Count - 1; i++)
    {
        if (increase && levels[i] > levels[i + 1])
        {
            return false;
        }

        if (!increase && levels[i] < levels[i + 1])
        {
            return false;
        }

        var dif = Math.Abs(levels[i] - levels[i + 1]);
        if (dif == 0 || dif > 3)
        {
            return false;
        }
    }

    return true;
}
