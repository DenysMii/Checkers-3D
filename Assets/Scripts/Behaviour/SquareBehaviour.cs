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

            switch (highlightStatus)
            {
                case HighlightStatus.ToMove:
                    currentPressedPiece.MoveToNewSquare(this);
                    PromoteAttachedChecker();
                    break;
                case HighlightStatus.ToCapture:
                    currentPressedPiece.CaptureOpponentPiece(this);
                    currentPressedPiece.MoveToNewSquare(this, true);
                    PromoteAttachedChecker();
                    break;
            }
        }

    }

    private void PromoteAttachedChecker()
    {
        print(attachedPiece == null);
        if (isKingPromoteSquare && attachedPiece.isWhite == promoteForWhite && attachedPiece is CheckerBehaviour)
        {
            CheckerBehaviour checker = attachedPiece as CheckerBehaviour;
            StaticData.boardGenerator.PromoteChecker(checker);
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
