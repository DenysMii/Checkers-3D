using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class AnimationsManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineBrain targetCinemachineBrain;
    [SerializeField] private GameObject whiteCamera;
    [SerializeField] private GameObject blackCamera;

    [Header("Pieces")]
    [SerializeField] private float destroyPieceDelay;
    [SerializeField] private int fragmentCount;

    public delegate void PieceAnimationEvent(bool isCapturing);
    public event PieceAnimationEvent OnPieceMovementFinished;

    private bool isAnimationsOn;

    private void Start()
    {

    }
    public void SetAnimationsState(bool isOn)
    {
        isAnimationsOn = isOn;
        targetCinemachineBrain.DefaultBlend = isOn ?
            new(CinemachineBlendDefinition.Styles.EaseInOut, 1f) :
            new(CinemachineBlendDefinition.Styles.Cut, 1f);
    }

    public void SwitchCamera()
    {
        whiteCamera.SetActive(!whiteCamera.activeSelf);
        blackCamera.SetActive(!blackCamera.activeSelf);
    }

    public void MovePiece(Transform pieceTransform, Vector3 targetPos, float duration, bool isCapturing)
    {
        StartCoroutine(MovePieceCoroutine(pieceTransform, targetPos, duration, isCapturing));
    }

    public void DestroyPiece(GameObject piece)
    {
        StartCoroutine(DestroyPieceCoroutine(piece, isAnimationsOn ? destroyPieceDelay : 0));
    }

    private IEnumerator MovePieceCoroutine(Transform pieceTransform, Vector3 targetPos, float duration, bool isCapturing)
    {
        if(isAnimationsOn)
        {
            Vector3 piecePos = pieceTransform.localPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                pieceTransform.localPosition = Vector3.Lerp(piecePos, targetPos, elapsedTime / duration);
                yield return null;
            }
        }

        pieceTransform.localPosition = targetPos;
        OnPieceMovementFinished.Invoke(isCapturing);
    }

    private IEnumerator DestroyPieceCoroutine(GameObject piece, float delay)
    {
        piece.GetComponent<Rigidbody>().isKinematic = !isAnimationsOn;

        Fracture fracture = piece.AddComponent<Fracture>();
        fracture.triggerOptions.triggerType = TriggerType.Collision;
        fracture.fractureOptions.fragmentCount = fragmentCount;
        fracture.triggerOptions.triggerAllowedTags.Add("Piece");

        yield return new WaitForSeconds(delay);
        Destroy(piece);
    }
}
