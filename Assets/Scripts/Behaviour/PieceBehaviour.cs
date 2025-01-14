using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PieceBehaviour : MonoBehaviour
{
    [SerializeField] public bool isWhite;

    protected int moveDirection;
    public List<int[]> highlightedSquaresBPos { get; protected set; }
    public SquaresHighlighter squaresHighlighter { protected get; set; }

    public SquareBehaviour attachedSquareBehaviour { get; set; }

    protected List<int[]> cellsDir = new List<int[]>
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

    public void OnMouseDown()
    {
        if (isWhite == StaticData.isWhiteTurn)
            HighlightPossibleSquares();
    }

    protected void HighlightPossibleSquares()
    {
        SetMoveHighlightSquaresBPos();
        squaresHighlighter.HighlightSquares(this, HighlightStatus.ToMove);
    }

    protected abstract void SetMoveHighlightSquaresBPos();

    protected int[] CoordsSum(int[] a, int[] b)
    {
        int[] sum = new int[]
        {
            a[0] + b[0],
            a[1] + b[1]
        };
        return sum;
    }
}
