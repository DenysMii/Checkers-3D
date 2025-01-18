using System.Collections.Generic;
using UnityEngine;

public class SquaresHighlighter : MonoBehaviour
{
    [SerializeField] private Material pressedSquare;
    [SerializeField] private Material greenSquare;
    [SerializeField] private Material yellowSquare;
    [SerializeField] private Material redSquare;

    private PieceBehaviour currentPiece;
    private int[] currentPieceBPos;

    public int GetHighlightedSquaresCount(List<int[]> squaresBPos)
    {
        int squaresCount = 0;
        foreach (var squareBPos in squaresBPos)
        {
            SquareBehaviour square = StaticData.GetSquare(squareBPos);
            if (square != null && !square.isOccupied)
                squaresCount++;
        }
        return squaresCount;
    }

    public void HighlightSquares(PieceBehaviour piece, HighlightStatus highlightStatus)
    {
        ClearHighlightedSquares();
        if (currentPiece == piece && currentPieceBPos == piece.attachedSquare.boardPos)
        {
            currentPiece = null;
            return;
        }
        
        currentPiece = piece;
        currentPieceBPos = piece.attachedSquare.boardPos;

        StaticData.GetSquare(currentPieceBPos).HighlightSquare(pressedSquare, HighlightStatus.Pressed);
        foreach (var squareBPos in piece.highlightedSquaresBPos)
        {
            SquareBehaviour square = StaticData.GetSquare(squareBPos);
            if (square != null && !square.isOccupied)
            {
                Material materialToHighlight;
                if (square.isKingPromoteSquare && currentPiece is CheckerBehaviour)
                    materialToHighlight = yellowSquare;
                else if (highlightStatus == HighlightStatus.ToMove)
                    materialToHighlight = greenSquare;
                else if (highlightStatus == HighlightStatus.ToCapture)
                    materialToHighlight = redSquare;
                else
                    materialToHighlight = StaticData.blackSquareMaterial;

                square.HighlightSquare(materialToHighlight, highlightStatus);
                square.currentPressedPiece = currentPiece;
            }
        }
            
    }

    public void ClearHighlightedSquares()
    {
        if(currentPiece != null)
        {
            StaticData.GetSquare(currentPieceBPos).HighlightSquare(StaticData.blackSquareMaterial, HighlightStatus.NotHighlighted);
            foreach (var squareBPos in currentPiece.highlightedSquaresBPos)
            {
                SquareBehaviour square = StaticData.GetSquare(squareBPos);
                if(square != null)
                {
                    square.HighlightSquare(StaticData.blackSquareMaterial, HighlightStatus.NotHighlighted);
                    square.currentPressedPiece = currentPiece;
                }
            }
        }
    }

    

}
