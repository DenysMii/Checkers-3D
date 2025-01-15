using System.Collections.Generic;
using UnityEngine;

public class CheckerBehaviour : PieceBehaviour
{
    protected override void SetMoveHighlightSquaresBPos()
    {
        int squaresDirIndex = isWhite ? 0 : 2;
        highlightedSquaresBPos = new List<int[]>
        {
            CoordsSum(attachedSquare.boardPos, squaresDir[squaresDirIndex]),
            CoordsSum(attachedSquare.boardPos, squaresDir[squaresDirIndex + 1])
        };
    }

    protected override void SetCaptureHighlightSquaresBPos()
    {
        highlightedSquaresBPos = new List<int[]>();
        foreach (var squareDir in squaresDir)
        {
            SquareBehaviour square = StaticData.GetSquare(CoordsSum(attachedSquare.boardPos, squareDir));
            //if(square != null && square.isOccupied && square)
        }
    }

}
