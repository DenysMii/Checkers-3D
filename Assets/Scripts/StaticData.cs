using UnityEngine;

public static class StaticData
{
    public static bool firstPlay = true;
    public static bool isWhiteTurn = true;

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

    public static SquareBehaviour[,] squaresBehaviourScripts = new SquareBehaviour[8, 8];
    public static Material blackSquareMaterial;
}

public enum HighlightStatus
{
    NotHighlighted,
    Pressed,
    ToMove,
    ToCapture,
    ToMoveIntoKing,
    ToCaptureInKing
}
