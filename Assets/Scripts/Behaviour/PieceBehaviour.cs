using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CoordsOperations;

public abstract class PieceBehaviour : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public bool isWhite;

    public bool isDestroyed { get; set; }
    public List<int[]> highlightedSquaresBPos { get; protected set; }
    public List<int[]> captureSquaresBPos { get; protected set; }

    public SquareBehaviour attachedSquare { get; set; }

    protected List<int[]> squaresDir = new List<int[]>
    {
        new int[]{ 1, 1 },
        new int[]{ 1, -1 },
        new int[]{ -1, 1 },
        new int[]{ -1, -1 },
    };

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (isWhite == StaticData.isWhiteTurn && pointerEventData.button == PointerEventData.InputButton.Left)
            HighlightPossibleSquares();
    }
    public bool IsPossibleToCapture()
    {
        SetCaptureHighlightSquaresBPos();
        return StaticData.squaresHighlighter.GetHighlightedSquaresCount(highlightedSquaresBPos) > 0;
    }

    protected void HighlightPossibleSquares()
    {
        HighlightStatus highlightStatus;
        if(StaticData.isObligatedToCapture)
        {
            SetCaptureHighlightSquaresBPos();
            highlightStatus = HighlightStatus.ToCapture;
        }
        else
        {
            SetMoveHighlightSquaresBPos();
            highlightStatus = HighlightStatus.ToMove;
        }

        StaticData.squaresHighlighter.HighlightSquares(this, highlightStatus);
    }

    public void MoveToNewSquare(SquareBehaviour newSquare)
    {
        StaticData.squaresHighlighter.ClearHighlightedSquares();

        attachedSquare.isOccupied = false;
        attachedSquare.attachedPiece = null;
        attachedSquare = null;
        
        newSquare.isOccupied = true;
        newSquare.attachedPiece = this;
        newSquare.currentPressedPiece = null;
        attachedSquare = newSquare;

        Vector3 localPos = new Vector3(newSquare.transform.position.x, 0, newSquare.transform.position.z);
        transform.SetLocalPositionAndRotation(localPos, transform.rotation);

        
    }

    public void CaptureOpponentPiece(SquareBehaviour newSquare)
    {
        int[] opponentPieceBPos = new int[]
        {
            newSquare.boardPos[0] - Math.Sign(newSquare.boardPos[0] - attachedSquare.boardPos[0]),
            newSquare.boardPos[1] - Math.Sign(newSquare.boardPos[1] - attachedSquare.boardPos[1])
        };

        SquareBehaviour squareWithOpponent = StaticData.GetSquare(opponentPieceBPos);
        squareWithOpponent.attachedPiece.isDestroyed = true;
        Destroy(squareWithOpponent.attachedPiece.gameObject);

        squareWithOpponent.isOccupied = false;
        squareWithOpponent.attachedPiece = null;
    }

    protected abstract void SetMoveHighlightSquaresBPos();
    public void SetCaptureHighlightSquaresBPos()
    {
        highlightedSquaresBPos = new List<int[]>();
        SetCaptureSquaresBPos();
        foreach (var captureSquareBPos in captureSquaresBPos)
        {
            int[] negAttachedSquareBPos = CoordsDiv(attachedSquare.boardPos, -1);
            int[] squaresDiff = CoordsSum(captureSquareBPos, negAttachedSquareBPos);
            int[] direction = CoordsDiv(squaresDiff, Mathf.Abs(squaresDiff[0]));
            int[] highlightedSquareBPos = CoordsSum(captureSquareBPos, direction);
            highlightedSquaresBPos.Add(highlightedSquareBPos);
        }
    }
    protected abstract void SetCaptureSquaresBPos();
}
