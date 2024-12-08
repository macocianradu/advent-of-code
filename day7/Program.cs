var lines = File.ReadLines(args[0]);

double calibration = 0;
foreach (var line in lines)
{
    var result = double.Parse(line.Split(':')[0]);
    var values = line.Split(": ")[1].Split(' ').Select(double.Parse).ToList();
    var operators = new List<int>();
    for (int i = 0; i < values.Count - 1; i++)
    {
        operators.Add(0);
    }

    do
    {
        var test = Test(result, operators, values);
        if (test > 0)
        {
            calibration += test;
            Console.WriteLine(result);
            break;
        }
        operators = NextPermutation(operators);
    } while (operators is not null);
}

Console.WriteLine(calibration);

List<int>? NextPermutation(List<int> operations)
{
    for (int i = 0; i < operations.Count; i++)
    {
        if (operations[i] < 2)
        {
            operations[i]++;
            for (int j = 0; j < i; j++)
            {
                operations[j] = 0;
            }
            return operations;
        }
    }
    return null;
}

double Test(double result, List<int> operations, List<double> values)
{
    double val = values[0];
    for (int i = 1; i < values.Count; i++)
    {
        switch (operations[i - 1])
        {
            case 0:
                {
                    val += values[i];
                    break;
                }
            case 1:
                {
                    val *= values[i];
                    break;
                }
            case 2:
                {
                    val = double.Parse(val.ToString() + values[i].ToString());
                    break;
                }
            default:
                throw new Exception("impossible");
        }
    }
    if (val == result)
    {
        return result;
    }
    // Console.Write(result + " - " + val + " ");
    // operations.ForEach(p => Console.Write(p + " "));
    // Console.WriteLine();
    return 0;
}

