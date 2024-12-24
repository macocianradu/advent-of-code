using System.Text;

namespace day21;

public class Robot
{
    static Dictionary<(string, int), string> cache = new ();
    public int X { get; set; } = 2;
    public int Y { get; set; } = 0;

    public string MoveRobot(string s, List<Robot> robots)
    {
        if (cache.ContainsKey((s, robots.Count)))
        {
            return cache[(s, robots.Count)];
        }
        StringBuilder seq = new();
        foreach (var m in s)
        {
            var robot = robots[0];
            var x = robot.X;
            var y = robot.Y;
            var seq1 = robot.MoveRowCol(m);
            var x1 = robot.X;
            var y1 = robot.Y;
            string seq1Res = "";
            if (seq1 is not null)
            {
                if (robots.Count > 2)
                {
                    seq1Res = MoveRobot(seq1, robots.Skip(1).ToList());
                }
                else
                {
                    seq1Res = MoveString(seq1);
                }
            }
            robot.X = x;
            robot.Y = y;
            var seq2 = robot.MoveColRow(m);
            var seq2Res = "";
            if (seq2 is null)
            {
                robot.X = x1;
                robot.Y = y1;
                seq.Append(seq1Res);
                continue;
            }

            if (robots.Count > 2)
            {
                seq2Res = MoveRobot(seq2, robots.Skip(1).ToList());
            }
            else
            {
                seq2Res = MoveString(seq2);
            }
            if (seq1Res.Length > 0 && seq1Res.Length < seq2Res.Length)
            {
                robot.X = x1;
                robot.Y = y1;
                seq.Append(seq1Res);
                continue;
            }
            seq.Append(seq2Res);
        }

        cache.Add((s, robots.Count), seq.ToString());
        return seq.ToString();
    }

    public string MoveString(string s)
    {
        StringBuilder seq = new();
        foreach (var m2 in s)
        {
            var x = X;
            var y = Y;
            var seq21 = MoveRowCol(m2);
            var x1 = X;
            var y1 = Y;
            X = x;
            Y = y;
            var seq22 = MoveColRow(m2);
            if (seq21 is null)
            {
                seq.Append(seq22!);
                continue;
            }
            if (seq22 is null)
            {
                X = x1;
                Y = y1;
                seq.Append(seq21!);
                continue;
            }
            if (seq21.Length < seq22.Length)
            {
                X = x1;
                Y = y1;
                seq.Append(seq21);
                continue;
            }
            seq.Append(seq22);
        }
        return seq.ToString();
    }

    public string? MoveRowCol(char c)
    {
        StringBuilder sequence = new();
        var move = MoveRow(c);
        while (move is not null)
        {
            if (X == 0 && Y == 0)
            {
                return null;
            }
            sequence.Append(move);
            move = MoveRow(c);
        }
        move = MoveCol(c);
        while (move is not null)
        {
            sequence.Append(move);
            move = MoveCol(c);
        }
        sequence.Append('A');
        return sequence.ToString();
    }

    public string? MoveColRow(char c)
    {
        StringBuilder sequence = new();
        var move = MoveCol(c);
        while (move is not null)
        {
            if (X == 0 && Y == 0)
            {
                return null;
            }
            sequence.Append(move);
            move = MoveCol(c);
        }
        move = MoveRow(c);
        while (move is not null)
        {
            sequence.Append(move);
            move = MoveRow(c);
        }
        sequence.Append('A');
        return sequence.ToString();
    }

    private char? MoveCol(char c)
    {
        switch (c)
        {
            case '<':
                if (X != 0)
                {
                    X--;
                    return '<';
                }
                return null;
            case '^':
            case 'v':
                if (X > 1)
                {
                    X--;
                    return '<';
                }
                if (X < 1)
                {
                    X++;
                    return '>';
                }
                return null;
            case 'A':
            case '>':
                if (X < 2)
                {
                    X++;
                    return '>';
                }
                return null;
            default:
                return null;
        }
    }
    private char? MoveRow(char c)
    {
        switch (c)
        {
            case '^':
            case 'A':
                if (Y != 0)
                {
                    Y--;
                    return '^';
                }
                return null;
            case '<':
            case 'v':
            case '>':
                if (Y < 1)
                {
                    Y++;
                    return 'v';
                }
                return null;
            default:
                return null;
        }
    }
}
