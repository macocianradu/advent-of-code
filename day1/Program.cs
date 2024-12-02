var input = File.ReadLines("input/input1.txt");

/* test 1
var list1 = new List<int>();
var list2 = new List<int>();

foreach(var line in input) 
{
    var items = line.Split(' ');
    list1.Add(int.Parse(items[0]));
    list2.Add(int.Parse(items[1]));
}

list1.Sort();
list2.Sort();

var result = 0;

for(var i = 0; i < list1.Count; i++) 
{
    result += Math.Abs(list1[i] - list2[i]);
} */

// test 2
var list1 = new Dictionary<int, int>();
var list2 = new Dictionary<int, int>();

foreach (var line in input)
{
    var items = line.Split(' ');
    var int1 = int.Parse(items[0]);
    var int2 = int.Parse(items[1]);
    if (list1.ContainsKey(int1))
    {
        list1[int1]++;
    }
    else
    {
        list1.Add(int1, 1);
    }

    if (list2.ContainsKey(int2))
    {
        list2[int2]++;
    }
    else
    {
        list2.Add(int2, 1);
    }
}

double result = 0;

foreach (var item in list1.Keys)
{
    if (list2.ContainsKey(item))
    {
        result += item * list1[item] * list2[item];
    }
}

Console.WriteLine(result);
