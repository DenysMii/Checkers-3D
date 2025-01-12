using UnityEngine;

public class SquareBehaviour : MonoBehaviour
{
    public int[] boardPos { get; set; }
    public bool isOccupied { get; set; }
    public bool attachedPieceBehaviour { get; set; }

    private HighlightStatus highlightStatus;
    private Renderer squareRenderer;
    
    

    private void Start()
    {
        highlightStatus = HighlightStatus.NotHighlighted;
        squareRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        switch(highlightStatus)
        {
            case HighlightStatus.NotHighlighted:

            case HighlightStatus.ToMove:
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
