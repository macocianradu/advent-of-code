namespace day22;

public class ListComparer: IEqualityComparer<List<long>>
{
    public bool Equals(List<long>? x, List<long>? y)
    {
        if(ReferenceEquals(x, y)) return true;
        if(x is null || y is null) return false;
        
        return x.SequenceEqual(y);
    }

    public int GetHashCode(List<long> obj)
    {
        HashCode hash = new();
        foreach (var i in obj)
        {
            hash.Add(i);
        }
        return hash.ToHashCode();
    }
}