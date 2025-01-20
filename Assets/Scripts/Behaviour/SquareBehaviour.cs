using UnityEngine;
using UnityEngine.EventSystems;

public class SquareBehaviour : MonoBehaviour, IPointerDownHandler
{
    public int[] boardPos { get; set; }
    public bool isOccupied { get; set; }
    public bool isKingPromoteSquare { get; set; }
    public bool promoteForWhite { get; set; }

    public PieceBehaviour attachedPiece { get; set; }
    public PieceBehaviour currentPressedPiece { private get; set; }

    private HighlightStatus highlightStatus;
    private Renderer squareRenderer;
    
    private void Start()
    {
        highlightStatus = HighlightStatus.NotHighlighted;
        squareRenderer = gameObject.GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (highlightStatus != HighlightStatus.NotHighlighted)
            {
                if (isKingPromoteSquare && currentPressedPiece.isWhite == promoteForWhite && currentPressedPiece is CheckerBehaviour)
                {
                    CheckerBehaviour currentChecker = currentPressedPiece as CheckerBehaviour;
                    currentPressedPiece = StaticData.boardGenerator.PromoteChecker(currentChecker);
                }
            }

            switch (highlightStatus)
            {
                case HighlightStatus.ToMove:
                    currentPressedPiece.MoveToNewSquare(this);
                    break;
                case HighlightStatus.ToCapture:
                    currentPressedPiece.CaptureOpponentPiece(this);
                    currentPressedPiece.MoveToNewSquare(this, true);
                    break;
            }

        }
    }

    public void HighlightSquare(Material material, HighlightStatus status)
    {
        squareRenderer.material = material;
        highlightStatus = status;
    }
}
