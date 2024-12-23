using System.Text;

namespace day21;

public class Keypad
{
    public int X { get; set; } = 2;
    public int Y { get; set; } = 3;

    public string MoveString(string s)
    {
        StringBuilder seq = new();
        foreach (var m2 in s)
        {
            var x2 = X;
            var y2 = Y;
            var seq21 = MoveRowCol(m2);
            X = x2;
            Y = y2;
            var seq22 = MoveColRow(m2);
            if (seq21 is null)
            {
                seq.Append(seq22!);
                continue;
            }
            if (seq22 is null)
            {
                seq.Append(seq21!);
                continue;
            }
            if (seq21.Length < seq22.Length)
            {
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
            if (X == 0 && Y == 3)
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
            if (X == 0 && Y == 3)
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
            case '7':
            case '4':
            case '1':
                if (X != 0)
                {
                    X--;
                    return '<';
                }
                return null;
            case '8':
            case '5':
            case '2':
            case '0':
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
            case '9':
            case '6':
            case '3':
            case 'A':
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
            case '7':
            case '8':
            case '9':
                if (Y != 0)
                {
                    Y--;
                    return '^';
                }
                return null;
            case '4':
            case '5':
            case '6':
                if (Y > 1)
                {
                    Y--;
                    return '^';
                }
                if (Y < 1)
                {
                    Y++;
                    return 'v';
                }
                return null;
            case '1':
            case '2':
            case '3':
                if (Y > 2)
                {
                    Y--;
                    return '^';
                }
                if (Y < 2)
                {
                    Y++;
                    return 'v';
                }
                return null;
            case '0':
            case 'A':
                if (Y < 3)
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
