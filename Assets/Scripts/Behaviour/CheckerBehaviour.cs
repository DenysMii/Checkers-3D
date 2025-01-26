using System;
using System.Collections.Generic;
using UnityEngine;
using static CoordsOperations;

public class CheckerBehaviour : PieceBehaviour
{
    private int moveDirection;

    protected virtual void Start()
    {
        moveDirection = isWhite ? 1 : -1;
    }

    protected override void SetMoveHighlightSquaresBPos()
    {
        int squaresDirIndex = isWhite ? 0 : 2;
        highlightedSquaresBPos = new List<int[]>
        {
            CoordsSum(attachedSquare.boardPos, squaresDir[squaresDirIndex]),
            CoordsSum(attachedSquare.boardPos, squaresDir[squaresDirIndex + 1])
        };
    }

    protected override void SetCaptureSquaresBPos()
    {
        captureSquaresBPos = new List<int[]>();
        foreach (var squareDir in squaresDir)
        {
            SquareBehaviour squareBehaviour = StaticData.GetSquare(CoordsSum(attachedSquare.boardPos, squareDir));
            if (squareBehaviour != null && squareBehaviour.isOccupied && squareBehaviour.attachedPiece.isWhite != isWhite)
                captureSquaresBPos.Add(squareBehaviour.boardPos);
        }
    }

}
