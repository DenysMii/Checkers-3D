using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PieceBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public bool isWhite;

    protected int moveDirection;
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

    protected virtual void Start()
    {
        moveDirection = isWhite ? 1 : -1;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isWhite == StaticData.isWhiteTurn)
            HighlightPossibleSquares();
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

    public virtual void MoveToNewSquare(SquareBehaviour newSquare, bool isCapturing = false)
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

        if (isCapturing)
            StaticData.turnsManager.CheckForCaptures();

        if (!StaticData.isObligatedToCapture)
            StaticData.turnsManager.SwitchTurn();
    }

    public virtual void CaptureOpponentPiece(SquareBehaviour newSquare)
    {
        int[] opponentPieceBPos = new int[]
        {
            newSquare.boardPos[0] - Math.Sign(newSquare.boardPos[0] - attachedSquare.boardPos[0]),
            newSquare.boardPos[1] - Math.Sign(newSquare.boardPos[1] - attachedSquare.boardPos[1])
        };

        SquareBehaviour squareWithOpponent = StaticData.GetSquare(opponentPieceBPos);
        Destroy(squareWithOpponent.attachedPiece.gameObject);
        squareWithOpponent.isOccupied = false;
        squareWithOpponent.attachedPiece = null;


    }

    protected abstract void SetMoveHighlightSquaresBPos();
    public abstract void SetCaptureHighlightSquaresBPos();
    protected abstract void SetCaptureSquaresBPos();

    protected int[] CoordsSum(int[] a, int[] b)
    {
        return new int[]
        {
            a[0] + b[0],
            a[1] + b[1]
        };
    }

    protected int[] CoordsMult(int[] coords, int multiplier)
    {
        return new int[]
        {
            coords[0] * multiplier,
            coords[1] * multiplier
        };
    }
}
