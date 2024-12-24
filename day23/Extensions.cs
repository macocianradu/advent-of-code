namespace day23;

public class ListComparer : IEqualityComparer<List<string>>
{
    public bool Equals(List<string>? x, List<string>? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }
        return x.SequenceEqual(y);
    }

    public int GetHashCode(List<string> obj)
    {
        obj.Sort();
        HashCode hash = new();
        foreach (var s in obj)
        {
            hash.Add(s);
        }
        return hash.ToHashCode();
    }
}