public static class CoordsOperations
{
    public static int[] CoordsSum(int[] a, int[] b)
    {
        return new int[]
        {
            a[0] + b[0],
            a[1] + b[1]
        };
    }

    public static int[] CoordsMult(int[] coords, int multiplier)
    {
        return new int[]
        {
            coords[0] * multiplier,
            coords[1] * multiplier
        };
    }
    public static int[] CoordsDiv(int[] coords, int divider)
    {
        return new int[]
        {
            coords[0] / divider,
            coords[1] / divider
        };
    }
}
