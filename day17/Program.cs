using day17;

var input = File.ReadAllLines(args[0]);

var B = uint.Parse(input[1].Split(':')[1]);
var C = uint.Parse(input[2].Split(':')[1]);

var instructions = input[4].Split(':')[1].Split(',').Select(int.Parse).ToList();

var tasks = new List<Task>();
Computer computer;
List<uint> positions = [];
var digit = 1;
var revInstructions = new List<int>(instructions);
revInstructions.Reverse();
var a = 1UL;
while (positions.Count < instructions.Count)
{
    var upper = (long)Math.Pow(2, (digit + 1) * 3);
    uint pos = 0;
    uint start = 0;
    if (positions.Count == digit)
    {
        start = positions.Last() + 1;
        positions.RemoveAt(positions.Count - 1);
    }
    a = GetStartingPoint(positions) + start;
    pos = start;
    for (pos = start; pos < 8; a++, pos++)
    {
        computer = new Computer(a, B, C, instructions);
        while (computer.Execute() && computer.Output.Count < instructions.Count)
        {
        }

        if (Compare(computer.Output, revInstructions, digit))
        {
            positions.Add(pos);
            break;
        }
    }
    if (positions.Count < digit)
    {
        digit--;
        continue;
    }
    digit++;
}

PrintProgram(positions);

Console.WriteLine(a);

static bool Compare(List<ulong> output, List<int> instructions, int digits)
{
    output.Reverse();
    for (int i = 0; i < digits; i++)
    {
        if ((int)output[i] != instructions[i])
        {
            return false;
        }
    }
    return true;
}

static void PrintProgram<T>(List<T> program)
{
    program.ForEach(e => Console.Write(e + ","));
    Console.WriteLine();
}

bool Test(ulong a)
{
    var computer = new Computer(a, B, C, instructions);
    while (computer.Execute() && computer.Output.Count < instructions.Count)
    {
    }
    Console.Write(a + ": ");
    PrintProgram(computer.Output);

    for (int i = 0; i < computer.Output.Count; i++)
    {
        if (instructions[i] != (int)computer.Output[i])
        {
            return false;
        }
    }
    if (instructions.Count != computer.Output.Count)
    {
        return false;
    }

    Console.WriteLine(a);
    return true;
}

static ulong GetStartingPoint(List<uint> positions)
{
    ulong result = (ulong)Math.Pow(2, 3 * positions.Count);
    for (int i = 0; i < positions.Count; i++)
    {
        result += (ulong)Math.Pow(2, 3 * (positions.Count - i)) * positions[i];
    }
    return result;
}
