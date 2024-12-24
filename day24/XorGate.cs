namespace day24;

public class XorGate((string, bool?) A1, (string, bool?) A2, string Out) : Gate(A1, A2, Out)
{
    public override bool? Compute()
    {
        if (A1.Item2 is null || A2.Item2 is null)
        {
            return null;
        }
        return A1.Item2.Value ^ A2.Item2.Value;
    }
}