using System.Net;
using day23;

var input = File.ReadAllLines("input/input1.txt");

Dictionary<string, List<string>> connections = [];
foreach (var line in input)
{
    var pcs = line.Split('-');
    if (connections.TryGetValue(pcs[0], out List<string>? connections1))
    {
        connections1.Add(pcs[1]);
    }
    else
    {
        connections.Add(pcs[0], [pcs[1]] );
    }

    if (connections.TryGetValue(pcs[1], out List<string>? connections2))
    {
        connections2.Add(pcs[0]);
    }
    else
    {
        connections.Add(pcs[1], [pcs[0]]);
    }
}

HashSet<List<string>> networks = new(new ListComparer());

foreach (var (key, value) in connections)
{
    foreach (var connection in value)
    {
        networks.Add(new([key, connection]));
    }
}

while (true)
{
    List<(List<string> list, string key)> toAdd = [];

    foreach (var (key, value) in connections)
    {
        foreach (var network in networks)
        {
            if (!network.Contains(key) && network.All(e => value.Contains(e) && connections[e].Contains(key)))
            {
                toAdd.Add((network, key));
            }
        }
    }
    
    if (toAdd.Count > 0)
    {
        toAdd.ForEach(a =>
        {
            networks.Remove(a.list);
            networks.Add([..a.list, a.key]);
        });
        continue;
    }

    break;
}
var sort = new List<List<string>>(networks);
sort.Sort((a, b) => b.Count.CompareTo(a.Count));
var lan = sort[0];
lan.Sort();

lan.ForEach(e => Console.Write(e + ","));