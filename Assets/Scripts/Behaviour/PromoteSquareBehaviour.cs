using UnityEngine.EventSystems;

public class PromoteSquareBehaviour : SquareBehaviour
{
    public bool promoteForWhite { get; set; }

    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            switch (highlightStatus)
            {
                case HighlightStatus.ToMove:
                    StaticData.animationsManager.OnPieceMovementFinished += PromoteAttachedChecker;
                    currentPressedPiece.MoveToNewSquare(this);
                    break;
                case HighlightStatus.ToCapture:
                    StaticData.animationsManager.OnPieceMovementFinished += PromoteAttachedChecker;
                    currentPressedPiece.CaptureOpponentPiece(this);
                    currentPressedPiece.MoveToNewSquare(this, true);
                    break;
            }
        }
    }

    private void PromoteAttachedChecker()
    {
        if (attachedPiece.isWhite == promoteForWhite && attachedPiece is CheckerBehaviour)
        {
            CheckerBehaviour checker = attachedPiece as CheckerBehaviour;
            StaticData.boardGenerator.PromoteChecker(checker);
            StaticData.animationsManager.OnPieceMovementFinished -= PromoteAttachedChecker;
        }
    }
}
