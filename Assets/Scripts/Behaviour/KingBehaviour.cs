using System.Collections.Generic;
using UnityEngine;
using static CoordsOperations;

public class KingBehaviour : PieceBehaviour
{
    private int moveDirection;

    protected virtual void Start()
    {
        moveDirection = isWhite ? 1 : -1;
    }

    protected override void SetMoveHighlightSquaresBPos()
    {
        highlightedSquaresBPos = new List<int[]>();
        foreach (var squareDir in squaresDir)
        {
            int multiplier = 1;
            int[] squaresDiff = CoordsMult(squareDir, multiplier);
            int[] squarePos = CoordsSum(attachedSquare.boardPos, squaresDiff);
            SquareBehaviour highlightedSquare = StaticData.GetSquare(squarePos);

            while ( highlightedSquare != null && !highlightedSquare.isOccupied)
            {
                highlightedSquaresBPos.Add(squarePos);
                multiplier++;

                squaresDiff = CoordsMult(squareDir, multiplier);
                squarePos = CoordsSum(attachedSquare.boardPos, squaresDiff);
                highlightedSquare = StaticData.GetSquare(squarePos);
            }
        }
    }

    protected override void SetCaptureSquaresBPos()
    {
        captureSquaresBPos = new List<int[]>();
        foreach (var squareDir in squaresDir)
        {
            int multiplier = 1;
            int[] squaresDiff = CoordsMult(squareDir, multiplier);
            int[] currentSquarePos = CoordsSum(attachedSquare.boardPos, squaresDiff);
            SquareBehaviour highlightedSquare = StaticData.GetSquare(currentSquarePos);

            while (highlightedSquare != null && !highlightedSquare.isOccupied)
            {
                multiplier++;
                squaresDiff = CoordsMult(squareDir, multiplier);
                currentSquarePos = CoordsSum(attachedSquare.boardPos, squaresDiff);
                highlightedSquare = StaticData.GetSquare(currentSquarePos);
            }

            if (highlightedSquare != null && highlightedSquare.isOccupied && highlightedSquare.attachedPiece.isWhite != isWhite)
                captureSquaresBPos.Add(currentSquarePos);
        }
    }
}
