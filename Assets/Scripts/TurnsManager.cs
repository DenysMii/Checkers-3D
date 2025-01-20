using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private float loadResultSceneDelay;
    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    private int drawCounter = 41;

    private bool isGameEnded = false;
    private void Start()
    {
        StaticData.animationsManager.OnPieceMovementFinished += CheckForSwitchTurn;
    }

    public void CheckForSwitchTurn(bool isCapturing)
    {
        if (isCapturing)
            CheckForCaptures();

        if (!StaticData.isObligatedToCapture)
            SwitchTurn();
    }

    public void SwitchTurn()
    {
        StaticData.isWhiteTurn = !StaticData.isWhiteTurn;
        CheckForEndGame();
        if(!isGameEnded)
            StaticData.animationsManager.SwitchCamera();
    }

    private void CheckForEndGame()
    {
        CheckForCaptures();
        if(!StaticData.isObligatedToCapture)
        {
            if ((!IsMovePossible(StaticData.isWhiteTurn) && !IsMovePossible(!StaticData.isWhiteTurn)) || drawCounter <= 0)
               StartCoroutine(EndGame(3)); //Draw
            else if (!IsMovePossible(StaticData.isWhiteTurn))
                StartCoroutine(EndGame(StaticData.isWhiteTurn ? 2 : 1)); //Win 
        }
    }
    
    private IEnumerator EndGame(int sceneId)
    {
        isGameEnded = true;
        yield return new WaitForSeconds(loadResultSceneDelay);
        StaticData.firstPlay = false;
        SceneManager.LoadScene(sceneId);
    }

    public void CheckForCaptures()
    {
        GameObject pieceHolder = StaticData.isWhiteTurn ? whitePiecesHolder : blackPiecesHolder;
        PieceBehaviour[] currentPieces = pieceHolder.GetComponentsInChildren<PieceBehaviour>();

        foreach (PieceBehaviour piece in currentPieces)
        {
            if (piece.IsPossibleToCapture() && !piece.isDestroyed)
            {
                drawCounter = 41;
                StaticData.isObligatedToCapture = true;
                return;
            }
        }
        drawCounter--;
        StaticData.isObligatedToCapture = false;
    }

    private bool IsMovePossible(bool isWhite)
    {
        GameObject pieceHolder = isWhite ? whitePiecesHolder : blackPiecesHolder;
        PieceBehaviour[] currentPieces = pieceHolder.GetComponentsInChildren<PieceBehaviour>();

        foreach (PieceBehaviour piece in currentPieces)
        {
            if (piece.IsPossibleToMove() && !piece.isDestroyed)
                return true;
        }

        return false;
    }

}
