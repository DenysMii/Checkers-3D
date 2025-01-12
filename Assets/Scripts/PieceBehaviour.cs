using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PieceBehaviour : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public bool isWhite;

    protected int moveDirection;
    public List<int[]> highlightedCellsBPos { get; protected set; }

    public SquaresHighlighter squaresHighlighter { protected get; set; }
    public SquareBehaviour attachedSquareBehaviour { protected get; set; }

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isWhite == StaticData.isWhiteTurn)
            HighlightPossibleSquares();
    }

    protected void HighlightPossibleSquares()
    {
        HighlightStatus highlightStatus;

        squaresHighlighter.HighlightSquares(highlightedCellsBPos, HighlightStatus.ToMove);
    }

    public abstract List<int[]> GetCaptureCellsBPos();
    public abstract List<int[]> GetMoveCellsBPos();

    public virtual void ClearPossibleCells()
    {
        boardManager.ClearCells(highlightedCellsBPos);
    }

    public virtual void MoveToNewCell(Cell newCellObject, bool isCapturing = false, bool turnIntoKing = false)
    {
        audioSource.clip = isCapturing ? captureAudio : moveAudio;
        audioSource.Play();

        ClearPossibleCells();

        attachedSquareBehaviour.attachedPieceObject = null;
        attachedSquareBehaviour = newCellObject;
        attachedSquareBehaviour.attachedPieceObject = this;

        Vector3 piecePos = attachedSquareBehaviour.gameObject.transform.localPosition;
        Quaternion pieceRotation = gameObject.transform.localRotation;
        gameObject.transform.SetLocalPositionAndRotation(piecePos, pieceRotation);

    }

    public virtual void CaptureOpponentPiece(Cell newCellObject, bool turnIntoKing = false)
    {
        int[] opponentPieceBPos = new int[]
        {
            newCellObject.boardPosition[0] - Math.Sign(newCellObject.boardPosition[0] - attachedSquareBehaviour.boardPosition[0]),
            newCellObject.boardPosition[1] - Math.Sign(newCellObject.boardPosition[1] - attachedSquareBehaviour.boardPosition[1])
        };

        boardManager.DestroyPiece(opponentPieceBPos);
        boardManager.isObligatedToCapture = false;

        MoveToNewCell(newCellObject, true, turnIntoKing);
    }

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
