namespace day17;

public class Computer(int A, int B, int C, List<int> Instructions)
{
    public int IP { get; set; }
    public List<int> Output { get; set; } = [];

    public bool Execute()
    {
        //Console.Write("A: " + A);
        //Console.Write(" B: " + B);
        //Console.Write(" C: " + C);
        if (IP >= Instructions.Count)
        {
            return false;
        }
        int instruction = Instructions[IP];
        int op = Instructions[IP + 1];
        //Console.Write(" i: " + instruction + " op: " + op + " => ");
        var operation = GetOperation(instruction);
        operation()(op);
        //Console.Write(" A: " + A);
        //Console.Write(" B: " + B);
        //Console.Write(" C: " + C);
        return true;
    }

    private void Adv(int op)
    {
        var deno = GetOperand(op);
        A = Convert.ToInt32(Math.Floor(A / Math.Pow(2, deno)));
        IP += 2;
    }

    private void Bxl(int op)
    {
        B ^= op;
        IP += 2;
    }

    private void Bst(int op)
    {
        B = GetOperand(op) % 8;
        IP += 2;
    }

    private void Jnz(int op)
    {
        if (A == 0)
        {
            IP += 2;
            return;
        }
        IP = op;
    }

    private void Bxc(int op)
    {
        B ^= C;
        IP += 2;
    }

    private void Out(int op)
    {
        var x = GetOperand(op);
        Output.Add(x % 8);
        IP += 2;
    }

    private void Bdv(int op)
    {
        var deno = GetOperand(op);
        B = Convert.ToInt32(Math.Floor(A / Math.Pow(2, deno)));
        IP += 2;
    }

    private void Cdv(int op)
    {
        var deno = GetOperand(op);
        C = Convert.ToInt32(Math.Floor(A / Math.Pow(2, deno)));
        IP += 2;
    }

    private Func<Action<int>> GetOperation(int opcode)
    {
        return opcode switch
        {
            0 => () => (int op) => Adv(op),
            1 => () => (int op) => Bxl(op),
            2 => () => (int op) => Bst(op),
            3 => () => (int op) => Jnz(op),
            4 => () => (int op) => Bxc(op),
            5 => () => (int op) => Out(op),
            6 => () => (int op) => Bdv(op),
            7 => () => (int op) => Cdv(op),
            _ => throw new Exception("Opcode doesn't exist"),
        };
    }

    private int GetOperand(int op)
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
        return -1;
    }
}
