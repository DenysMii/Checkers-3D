using UnityEngine;

public static class StaticData
{
    public static bool firstPlay = true;
    public static bool isWhiteTurn = true;
    public static bool isObligatedToCapture = false;

    public static Material blackSquareMaterial;

    public static BoardGenerator boardGenerator;
    public static SquaresHighlighter squaresHighlighter;
    public static TurnsManager turnsManager;
    public static AnimationsManager animationsManager;


    public static short[,] piecesStartingPos =
    {
        { 1, 0, 1, 0, 1, 0, 1, 0 },
        { 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 2, 0, 2, 0, 2, 0, 2 },
        { 2, 0, 2, 0, 2, 0, 2, 0 },
        { 0, 2, 0, 2, 0, 2, 0, 2 }
    };






    // 1 - white pieces
    // 2 - black pieces
    // 3 - white kings
    // 4 - black kings
    // 0 - empty

    public static SquareBehaviour[,] squares = new SquareBehaviour[8, 8];

    public static SquareBehaviour GetSquare(int[] squareBPos)
    {
        int i = squareBPos[0];
        int j = squareBPos[1];

        if (i < 0 || i > 7 || j < 0 || j > 7) return null;
        return squares[i, j];
    }

}

public enum HighlightStatus
{
    NotHighlighted,
    Pressed,
    ToMove,
    ToCapture,
}
