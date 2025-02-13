using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private float loadResultSceneDelay;
    [SerializeField] private float switchCameraDelay;

    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    private int drawCounter = 41;

    private bool isGameEnded = false;
    private void Start()
    {
        StaticData.animationsManager.OnPieceCapture += CheckForExtraCapture;
        StaticData.animationsManager.OnPieceMovementFinished += SwitchTurn;
    }

    public void SwitchTurn()
    {
        StartCoroutine(SwitchTurnCoroutine());
    }

    private IEnumerator SwitchTurnCoroutine()
    {
        yield return new WaitForSeconds(switchCameraDelay);
        if (!StaticData.isObligatedToCapture)
        {
            StaticData.isWhiteTurn = !StaticData.isWhiteTurn;
            CheckForEndGame();
            if (!isGameEnded)
                StaticData.animationsManager.SwitchCamera();
        }
    }

    private void CheckForEndGame()
    {
        CheckForCaptures();
        if(!StaticData.isObligatedToCapture)
        {
            if ((!IsMovePossible(StaticData.isWhiteTurn) && !IsMovePossible(!StaticData.isWhiteTurn)) || drawCounter <= 0)
                StartCoroutine(EndGameCoroutine(3)); //Draw
            else if (!IsMovePossible(StaticData.isWhiteTurn))
                StartCoroutine(EndGameCoroutine(StaticData.isWhiteTurn ? 2 : 1)); //Win 
        }
    }
    
    public void ExternalEndGame(bool isDraw)
    {
        if(isDraw)
            StartCoroutine(EndGameCoroutine(3));
        else
            StartCoroutine(EndGameCoroutine(StaticData.isWhiteTurn ? 2 : 1));
    }

    private IEnumerator EndGameCoroutine(int sceneId)
    {
        isGameEnded = true;
        yield return new WaitForSeconds(loadResultSceneDelay);
        SceneManager.LoadScene(sceneId);
    }

    public void CheckForCaptures()
    {
        GameObject pieceHolder = StaticData.isWhiteTurn ? whitePiecesHolder : blackPiecesHolder;
        PieceBehaviour[] pieceBehaviours = pieceHolder.GetComponentsInChildren<PieceBehaviour>();

        foreach (PieceBehaviour pieceBehaviour in pieceBehaviours)
        {
            if (pieceBehaviour.IsPossibleToCapture() && !pieceBehaviour.gameObject.tag.Equals("Destroyed Piece"))
            {
                drawCounter = 41;
                StaticData.isObligatedToCapture = true;
                return;
            }
        }
        drawCounter--;
        StaticData.isObligatedToCapture = false;
    }

    private void CheckForExtraCapture(PieceBehaviour piece)
    {
        StaticData.isObligatedToCapture = piece.IsPossibleToCapture();
    }

    private bool IsMovePossible(bool isWhite)
    {
        GameObject pieceHolder = isWhite ? whitePiecesHolder : blackPiecesHolder;
        PieceBehaviour[] currentPieces = pieceHolder.GetComponentsInChildren<PieceBehaviour>();

        foreach (PieceBehaviour pieceBehaviour in currentPieces)
        {
            if (pieceBehaviour.IsPossibleToMove() && !pieceBehaviour.gameObject.tag.Equals("Destroyed Piece"))
                return true;
        }

        return false;
    }

}
