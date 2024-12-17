using day17;

var input = File.ReadAllLines(args[0]);

int A = int.Parse(input[0].Split(':')[1]);
int B = int.Parse(input[1].Split(':')[1]);
int C = int.Parse(input[2].Split(':')[1]);

var instructions = input[4].Split(':')[1].Split(',').Select(int.Parse).ToList();

var computer = new Computer(A, B, C, instructions);

while (computer.Execute())
{
}

for (var i = 0; i < computer.Output.Count - 1; i++)
{
    Console.Write(computer.Output[i] + ",");
}
Console.WriteLine(computer.Output.Last());
Console.WriteLine();
