using System.Collections.Generic;
using TMPro;
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
            if (!GetSquareBehaviour(squareBPos).isOccupied)
                highlightedSquares++;
        }
        return highlightedSquares;
    }

    public void HighlightSquares(PieceBehaviour piece, HighlightStatus highlightStatus)
    {
        ClearHighlightedSquares();
        currentPiece = piece;
        currentPieceBPos = piece.attachedSquareBehaviour.boardPos;

        GetSquareBehaviour(currentPieceBPos).HighlightSquare(pressedSquare, HighlightStatus.Pressed);
        foreach (var squareBPos in piece.highlightedSquaresBPos)
        {
            SquareBehaviour squareBehaviour = GetSquareBehaviour(squareBPos);
            if (squareBehaviour != null && !squareBehaviour.isOccupied)
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
                GetSquareBehaviour(squareBPos).HighlightSquare(materialToHighlight, highlightStatus);
            }
                
        }
            
    }

    private void ClearHighlightedSquares()
    {
        if(currentPiece != null)
        {
            GetSquareBehaviour(currentPieceBPos).HighlightSquare(StaticData.blackSquareMaterial, HighlightStatus.NotHighlighted);
            foreach (var squareBPos in currentPiece.highlightedSquaresBPos)
            {
                SquareBehaviour squareBehaviour = GetSquareBehaviour(squareBPos);
                if(squareBehaviour != null)
                    GetSquareBehaviour(squareBPos).HighlightSquare(StaticData.blackSquareMaterial, HighlightStatus.NotHighlighted);
            }
                
        }
    }

    private SquareBehaviour GetSquareBehaviour(int[] squareBPos)
    {
        int i = squareBPos[0];
        int j = squareBPos[1];

        if(i < 0 || i > 7 || j < 0 || j > 7) return null;
        return StaticData.squaresBehaviourScripts[i, j];
    }
}
