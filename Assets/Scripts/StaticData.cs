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

    public static void SetSquareObjects(GameObject squaresHolder)
    {
        SquareBehaviour[] squares = squaresHolder.GetComponentsInChildren<SquareBehaviour>();
        int squaresCounter = 0;

        for (int i = 0; i < 8; i++)
        {
            for(int j = i % 2; j < 8; j += 2)
            {
                squaresBehaviourScripts[i, j] = squares[squaresCounter];
                squaresBehaviourScripts[i, j].boardPos = new int[2] { i, j };
                squaresCounter++;
            }
        }
    }
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
