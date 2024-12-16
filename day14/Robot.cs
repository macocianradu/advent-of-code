namespace Day14;

public class Robot((int x, int y) Position, (int x, int y) Velocity)
{
    public (int x, int y) Position = Position;
    public (int x, int y) Velocity = Velocity;
    public (int x, int y) Move(int positions, int width, int height)
    {
        var x = Position.x + Velocity.x * positions;
        x %= width;
        if (x < 0)
        {
            x = width + x;
        }
        var y = Position.y + Velocity.y * positions;
        y %= height;
        if (y < 0)
        {
            y = height + y;
        }

        return (x, y);
    }
}
