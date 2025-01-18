using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private CameraAnimations cameraAnimations;
    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    public void CheckForCaptures()
    {
        GameObject pieceHolder = StaticData.isWhiteTurn ? whitePiecesHolder : blackPiecesHolder;
        PieceBehaviour[] currentPieces = pieceHolder.GetComponentsInChildren<PieceBehaviour>();

        foreach(PieceBehaviour piece in currentPieces)
        {
            if(piece.IsPossibleToCapture() && !piece.isDestroyed)
            {
                StaticData.isObligatedToCapture = true;
                return;
            }
        }
        
        StaticData.isObligatedToCapture = false;
    }

    public void SwitchTurn()
    {
        cameraAnimations.SwitchCamera(); 
        StaticData.isWhiteTurn = !StaticData.isWhiteTurn;
        CheckForCaptures();
    }
}
