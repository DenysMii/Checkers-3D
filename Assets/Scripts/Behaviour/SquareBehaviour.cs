using UnityEngine;
using UnityEngine.EventSystems;

public class SquareBehaviour : MonoBehaviour, IPointerDownHandler
{
    public int[] boardPos { get; set; }
    public bool isOccupied { get; set; }

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
            switch (highlightStatus)
            {
                case HighlightStatus.ToMove:
                    currentPressedPiece.MoveToNewSquare(this);
                    break;
                case HighlightStatus.ToCapture:
                    currentPressedPiece.CaptureOpponentPiece(this);
                    currentPressedPiece.MoveToNewSquare(this, true);
                    break;
                case HighlightStatus.ToMoveIntoKing:
                    break;
                case HighlightStatus.ToCaptureInKing:
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
