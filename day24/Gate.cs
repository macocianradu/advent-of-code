namespace day24;

public abstract class Gate((string, bool?)A1, (string, bool?) A2, string Out)
{
    public (string, bool?) A1 { get; set;  } = A1;
    public (string, bool?) A2 { get; set; } = A2;
    public string Out { get; } = Out;
    public abstract bool? Compute();
}