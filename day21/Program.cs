using System.Text;

using day21;

var input = File.ReadAllLines("input/input1.txt");

var result = 0;
foreach (var line in input)
{
    var keypad = new Keypad();
    var robot = new Robot();
    var robot2 = new Robot();
    StringBuilder sequence = new();
    foreach (var c in line)
    {
        var x = keypad.X;
        var y = keypad.Y;
        var sequence1 = "";
        var seq = keypad.MoveRowCol(c);
        var x1 = keypad.X;
        var y1 = keypad.Y;
        if (seq is not null)
        {
            sequence1 = robot.MoveRobot(seq, robot2);
        }
        keypad.X = x;
        keypad.Y = y;
        seq = keypad.MoveColRow(c);
        var sequence2 = "";
        if (seq is null)
        {
            sequence.Append(sequence1);
            keypad.X = x1;
            keypad.Y = y1;
            continue;
        }
        sequence2 = robot.MoveRobot(seq, robot2);
        if (sequence1.Length > 0 && sequence1.Length < sequence2.Length)
        {
            sequence.Append(sequence1);
            keypad.X = x1;
            keypad.Y = y1;
            continue;
        }
        sequence.Append(sequence2);
    }
    Console.Write(sequence.ToString());
    Console.WriteLine(": " + sequence.Length + " * " + int.Parse(line[..^1]));

    result += sequence.Length * int.Parse(line[..^1]);
}


Console.WriteLine(result);
