using UnityEngine;
using UnityEngine.EventSystems;

public class SquareBehaviour : MonoBehaviour, IPointerDownHandler
{
    public int[] boardPos { get; set; }
    public bool isOccupied { get; set; }
    public bool isKingPromoteSquare { get; set; }
    public bool promoteForWhite { private get; set; }

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
            HighlightStatus highlightTemp = highlightStatus;
            bool isCapturing = false;
            switch (highlightStatus)
            {
                case HighlightStatus.ToMove:
                    currentPressedPiece.MoveToNewSquare(this);
                    break;
                case HighlightStatus.ToCapture:
                    currentPressedPiece.CaptureOpponentPiece(this);
                    currentPressedPiece.MoveToNewSquare(this);
                    isCapturing = true;
                    break;
            }

            if (highlightTemp != HighlightStatus.NotHighlighted && isKingPromoteSquare &&
                attachedPiece.isWhite == promoteForWhite && attachedPiece is CheckerBehaviour)
            {
                CheckerBehaviour attachedChecker = attachedPiece as CheckerBehaviour;
                StaticData.boardGenerator.PromoteChecker(attachedChecker, this);
            }
            SwitchTurn(isCapturing);
        }
    }

    private void SwitchTurn(bool isCapturing)
    {
        if (isCapturing)
            StaticData.turnsManager.CheckForCaptures();

        if (!StaticData.isObligatedToCapture)
            StaticData.turnsManager.SwitchTurn();
    }

    public void HighlightSquare(Material material, HighlightStatus status)
    {
        squareRenderer.material = material;
        highlightStatus = status;
    }
}
