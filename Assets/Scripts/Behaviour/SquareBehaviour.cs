using UnityEngine;
using UnityEngine.EventSystems;

public class SquareBehaviour : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public int[] boardPos;
    public bool isOccupied;

    public PieceBehaviour attachedPiece;
    public PieceBehaviour currentPressedPiece { protected get; set; }

    protected HighlightStatus highlightStatus;
    protected Renderer squareRenderer;
    
    protected virtual void Start()
    {
        highlightStatus = HighlightStatus.NotHighlighted;
        squareRenderer = gameObject.GetComponent<Renderer>();
    }

    public virtual void OnPointerDown(PointerEventData pointerEventData)
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
            }
        }

    } 

    public void DestroyAttachedPiece()
    {
        attachedPiece.gameObject.tag = "Destroyed Piece";
        Destroy(attachedPiece.gameObject);

        isOccupied = false;
        attachedPiece = null;
    }

    public void HighlightSquare(Material material, HighlightStatus status)
    {
        squareRenderer.material = material;
        highlightStatus = status;
    }
}
