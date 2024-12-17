namespace day16;

public class Raindeer(int x, int y)
{
    private readonly int MovePenalty = 1;
    private readonly int TurnPenalty = 1000;

    public long Score { get; set; } = 0;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    // 0 -> Nord, 1 -> East, 2 -> South, 3 -> West
    public int Direction { get; set; } = 1;

    public bool Move(List<List<char>> map)
    {
        var x = X;
        var y = Y;
        x += Direction == 1 ? +1 : 0;
        x += Direction == 3 ? -1 : 0;
        y += Direction == 0 ? -1 : 0;
        y += Direction == 2 ? 1 : 0;
        if (map[y][x] == '#')
        {
            return false;
        }
        X = x;
        Y = y;
        Score += MovePenalty;
        return true;
    }

    public void Turn(bool clockwise)
    {
        Direction += clockwise ? 1 : -1;
        Direction %= 4;
        Score += TurnPenalty;
    }
}
