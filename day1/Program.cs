var input = File.ReadLines("input/input1.txt");
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
}

Console.WriteLine(result);
