namespace Day13;

public class Machine((int x, int y) ButtonA, (int x, int y) ButtonB, (int x, int y) prize)
{
    private static long Calibration = 10000000000000;
    public int PriceA = 3;
    public int PriceB = 1;
    public (long x, long y) Prize = (prize.x + Calibration, prize.y + Calibration);

    public long GetFewestTokens()
    {
        double aCoef = ButtonA.x * ButtonB.y - ButtonB.x * ButtonA.y;
        var sol = Prize.x * ButtonB.y - Prize.y * ButtonB.x;

        double a = sol / aCoef;
        double b = (Prize.x - (ButtonA.x * a)) / ButtonB.x;

        if (Math.Floor(a) != a || Math.Floor(b) != b)
        {
            return long.MaxValue;
        }
        return (long)a * PriceA + (long)b * PriceB;
    }

}
