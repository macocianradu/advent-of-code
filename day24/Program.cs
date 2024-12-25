using System.Collections;
using day24;

var input = File.ReadAllLines("input/input2.txt");

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


var xes = values.Where(x => x.Key.StartsWith('x')).ToList();
xes.Sort((x, y) => y.Key.CompareTo(x.Key));
var xs = BinaryToLong(xes
    .Select(s => s.Value? "1" : "0")
    .Aggregate((s1, s2) => s1 + s2));
var yes = values.Where(x => x.Key.StartsWith('y')).ToList();
yes.Sort((x, y) => y.Key.CompareTo(x.Key));
var ys = BinaryToLong(yes
    .Select(s => s.Value ? "1" : "0")
    .Aggregate((s1, s2) => s1 + s2));
var v1 = Compute(values, gates);
Console.WriteLine(xs + " + " + ys);
Check(v1, xs + ys);

static bool Check(Dictionary<string, bool> values, long expected)
{
    var toCheck = values.Where(x => x.Key.StartsWith('z')).ToList();
    toCheck.Sort((x, y) => y.Key.CompareTo(x.Key));

    var expectedBool = LongToBinary(expected).PadLeft(toCheck.Count, '0'); 
    Console.WriteLine(expectedBool + " - ");
    toCheck.ForEach(x => Console.Write(x.Value ? '1' : '0'));

    List<int> s = [];
    for (var i = 0; i < toCheck.Count; i++)
    {
        if (expectedBool[i] != (toCheck[i].Value ? '1' : '0'))
        {
            s.Add(i);
            Console.WriteLine(toCheck[i].Key);
        }
    }

    if (s.Count == 0)
    {
        return true;
    }

    Console.WriteLine("swap");
    toCheck[s[0]] = new(toCheck[s[0]].Key, !toCheck[s[0]].Value);
    toCheck[s[1]] = new(toCheck[s[1]].Key, !toCheck[s[1]].Value);
    return false;
}

static Dictionary<string, bool> Compute(Dictionary<string, bool> values, List<Gate> gates)
{
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
            gate.A1 = (gate.A1.Item1, a1);
            gate.A2 = (gate.A2.Item1, a2);
            values.Add(gate.Out, gate.Compute()!.Value);
            compute = true;
        }

        if (!compute)
        {
            return values;
        }
    }
}


static long BinaryToLong(string binary)
{
    return Convert.ToInt64(binary, 2);
}

static string LongToBinary(long value)
{
    return Convert.ToString(value, 2);
}

// see drawing lol
// chv,jpj,kgj,rts,vvw,z07,z12,z26
// rts <-> z07
// jpj <-> z12
// chv <-> vvw
// kgj <-> z26