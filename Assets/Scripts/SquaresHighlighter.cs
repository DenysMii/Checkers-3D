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

    public int GetHighlightedCellsCount(List<int[]> squaresBPos)
    {
        int highlightedSquares = 0;
        foreach (var squareBPos in squaresBPos)
        {
            SquareBehaviour square = StaticData.GetSquare(squareBPos);
            if (square != null && !square.isOccupied)
                highlightedSquares++;
        }
        return highlightedSquares;
    }

    public void HighlightSquares(PieceBehaviour piece, HighlightStatus highlightStatus)
    {
        ClearHighlightedSquares();
        if (currentPiece == piece)
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
                switch(highlightStatus)
                {
                    case HighlightStatus.ToMove:
                        materialToHighlight = greenSquare;
                        break;
                    case HighlightStatus.ToCapture:
                        materialToHighlight = redSquare;
                        break;
                    case HighlightStatus.ToCaptureInKing: case HighlightStatus.ToMoveIntoKing:
                        materialToHighlight = yellowSquare;
                        break;
                    default:
                        materialToHighlight = StaticData.blackSquareMaterial;
                        break;
                }
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
