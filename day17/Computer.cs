namespace day17;

public class Computer(ulong A, ulong B, ulong C, List<int> Instructions)
{
    public int IP { get; set; }
    public List<ulong> Output { get; set; } = [];

    public bool Execute()
    {
        if (IP >= Instructions.Count)
        {
            return false;
        }
        uint instruction = (uint)Instructions[IP];
        uint op = (uint)Instructions[IP + 1];
        var operation = GetOperation(instruction);
        operation()(op);
        return true;
    }

    private void Adv(uint op)
    {
        var deno = GetOperand(op);
        A = (ulong)Math.Truncate(A / Math.Pow(2, deno));
        IP += 2;
    }

    private void Bxl(uint op)
    {
        B ^= op;
        IP += 2;
    }

    private void Bst(uint op)
    {
        B = (uint)GetOperand(op) % 8;
        IP += 2;
    }

    private void Jnz(uint op)
    {
        if (A == 0)
        {
            IP += 2;
            return;
        }
        IP = (int)op;
    }

    private void Bxc(uint op)
    {
        B ^= C;
        IP += 2;
    }

    private void Out(uint op)
    {
        var x = GetOperand(op);
        Output.Add(x % 8);
        IP += 2;
    }

    private void Bdv(uint op)
    {
        var deno = GetOperand(op);
        B = (ulong)Math.Truncate(A / Math.Pow(2, deno));
        IP += 2;
    }

    private void Cdv(uint op)
    {
        var deno = GetOperand(op);
        C = (ulong)Math.Truncate(A / Math.Pow(2, deno));
        IP += 2;
    }

    private Func<Action<uint>> GetOperation(uint opcode)
    {
        return opcode switch
        {
            0 => () => (uint op) => Adv(op),
            1 => () => (uint op) => Bxl(op),
            2 => () => (uint op) => Bst(op),
            3 => () => (uint op) => Jnz(op),
            4 => () => (uint op) => Bxc(op),
            5 => () => (uint op) => Out(op),
            6 => () => (uint op) => Bdv(op),
            7 => () => (uint op) => Cdv(op),
            _ => throw new Exception("Opcode doesn't exist"),
        };
    }

    private ulong GetOperand(uint op)
    {
        if (op < 4)
        {
            return op;
        }
        if (op == 4)
        {
            return A;
        }
        if (op == 5)
        {
            return B;
        }
        if (op == 6)
        {
            return C;
        }
        if (op == 7)
        {
            throw new Exception("Invalid operand");
        }
        throw new Exception("Invalid operand");
    }
}
