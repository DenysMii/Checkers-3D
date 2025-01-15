using UnityEngine;
using UnityEngine.EventSystems;

public class SquareBehaviour : MonoBehaviour, IPointerClickHandler
{
    public int[] boardPos { get; set; }
    public bool isOccupied { get; set; }

    public PieceBehaviour currentPressedPiece { private get; set; }

    private HighlightStatus highlightStatus;

    
    private Renderer squareRenderer;
    
    private void Start()
    {
        highlightStatus = HighlightStatus.NotHighlighted;
        squareRenderer = gameObject.GetComponent<Renderer>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        switch(highlightStatus)
        {
            case HighlightStatus.NotHighlighted:
                break;
            case HighlightStatus.ToMove:
                currentPressedPiece.MoveToNewSquare(this);
                break;
            case HighlightStatus.ToCapture:
                break;
            case HighlightStatus.ToMoveIntoKing:
                break;
            case HighlightStatus.ToCaptureInKing:
                break;
        }
    }

    public void HighlightSquare(Material material, HighlightStatus status)
    {
        squareRenderer.material = material;
        highlightStatus = status;
    }
}
