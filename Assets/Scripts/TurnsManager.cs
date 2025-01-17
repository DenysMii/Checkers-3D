using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private CameraAnimations cameraAnimations;

    public void CheckForCaptures()
    {
        SquareBehaviour[,] squares = StaticData.squares;
        foreach (var square in squares)
        {
            if (square != null && square.isOccupied && square.attachedPiece.isWhite == StaticData.isWhiteTurn)
            {
                square.attachedPiece.SetCaptureHighlightSquaresBPos();
                List<int[]> highlightedSquaresBPos = square.attachedPiece.highlightedSquaresBPos;
                if (StaticData.squaresHighlighter.GetHighlightedSquaresCount(highlightedSquaresBPos) > 0)
                {
                    StaticData.isObligatedToCapture = true;
                    return;
                }
            }
        }
        StaticData.isObligatedToCapture = false;
    }

    public void SwitchTurn()
    {
        cameraAnimations.SwitchCamera();
        StaticData.isWhiteTurn = !StaticData.isWhiteTurn;
        CheckForCaptures();
    }
}
