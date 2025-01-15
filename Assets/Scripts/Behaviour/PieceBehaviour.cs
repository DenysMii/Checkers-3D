using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PieceBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public bool isWhite;

    protected int moveDirection;
    public List<int[]> highlightedSquaresBPos { get; protected set; }
    public SquaresHighlighter squaresHighlighter { protected get; set; }

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
        SetMoveHighlightSquaresBPos();
        squaresHighlighter.HighlightSquares(this, HighlightStatus.ToMove);
    }

    

    public virtual void MoveToNewSquare(SquareBehaviour newSquare)
    {
        squaresHighlighter.ClearHighlightedSquares();

        attachedSquare.isOccupied = false;
        attachedSquare = null;
        
        newSquare.isOccupied = true;
        newSquare.currentPressedPiece = null;
        attachedSquare = newSquare;

        Vector3 localPos = new Vector3(newSquare.transform.position.x, 0, newSquare.transform.position.z);
        transform.SetLocalPositionAndRotation(localPos, transform.rotation);
    }

    protected abstract void SetMoveHighlightSquaresBPos();
    protected abstract void SetCaptureHighlightSquaresBPos();

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
