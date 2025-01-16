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

    public override void SetCaptureHighlightSquaresBPos()
    {
        highlightedSquaresBPos = new List<int[]>();
        SetCaptureSquaresBPos();
        foreach (var captureSquareBPos in captureSquaresBPos)
        {
            int[] negAttachedSquareBPos = CoordsMult(attachedSquare.boardPos, -1);
            int[] squaresDiff = CoordsSum(captureSquareBPos, negAttachedSquareBPos);
            int[] highlightedSquareBPos = CoordsSum(squaresDiff, captureSquareBPos);
            highlightedSquaresBPos.Add(highlightedSquareBPos);
        }
    }

    protected override void SetCaptureSquaresBPos()
    {
        captureSquaresBPos = new List<int[]>();
        foreach (var squareDir in squaresDir)
        {
            SquareBehaviour square = StaticData.GetSquare(CoordsSum(attachedSquare.boardPos, squareDir));
            if (square != null && square.isOccupied && square.attachedPiece.isWhite != isWhite)
                captureSquaresBPos.Add(square.boardPos);
        }
    }

}
