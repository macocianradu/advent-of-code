using System.Collections;
using day24;

var input = File.ReadAllLines("input/input1.txt");

Dictionary<string, bool> values = [];
var line = 0;
for (; line < input.Length; line++)
{
    if (string.IsNullOrWhiteSpace(input[line]))
    {
        break;
    }
    var inp = input.ElementAt(line).Split(':');
    values.Add(inp[0], int.Parse(inp[1]) == 1);
}

line++;
List<Gate> gates = [];
for (; line < input.Length; line++)
{
    var v= input.ElementAt(line).Split("->");
    var output = v[1].Trim();
    var inp = v[0].Trim().Split(' ');
    bool? a1 = null;
    bool? a2 = null;
    if (values.TryGetValue(inp[0], out bool existing))
    {
        a1 = existing;
    }
    if (values.TryGetValue(inp[2], out existing))
    {
        a2 = existing;
    }
    switch (inp[1])
    {
        case "AND":
            gates.Add(new AndGate((inp[0], a1),(inp[2],a2), output));
            break;
        case "OR":
            gates.Add(new OrGate((inp[0], a1),(inp[2],a2), output));
            break;
        case "XOR":
            gates.Add(new XorGate((inp[0], a1),(inp[2],a2), output));
            break;
    }
}

while (true)
{
    var compute = false;

    foreach (var gate in gates)
    {
        if (values.ContainsKey(gate.Out))
        {
            continue;
        }

        if (!values.ContainsKey(gate.A1.Item1) || !values.ContainsKey(gate.A2.Item1))
        {
            continue;
        }
        var a1 = values[gate.A1.Item1];
        var a2 = values[gate.A2.Item1];
        gate.A1= (gate.A1.Item1, a1);
        gate.A2= (gate.A2.Item1, a2);
        values.Add(gate.Out, gate.Compute()!.Value); 
        compute = true;
    }

    if (!compute)
    {
        break;
    }
}

List<(string, char)> results = [];
foreach (var value in values.Where(v => v.Key.StartsWith('z')))
{
    results.Add((value.Key, value.Value ? '1': '0'));   
}
results.Sort((x, y) => y.Item1.CompareTo(x.Item1));

var binaryResult = "";
foreach (var result in results)
{
    binaryResult += result.Item2;
}

Console.WriteLine(Convert.ToInt64(binaryResult, 2));