using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineBrain targetCinemachineBrain;
    [SerializeField] private GameObject whiteCamera;
    [SerializeField] private GameObject blackCamera;

    [Header("Pieces")]
    [SerializeField] private float destroyPieceDelay;
    [SerializeField] private float pieceMovementSpeed;
    [SerializeField] private float pieceCaptureSpeed;

    public delegate void PieceMovementEvent();
    public event PieceMovementEvent OnPieceMovementFinished;

    public delegate void PieceCaptureEvent(PieceBehaviour piece);
    public event PieceCaptureEvent OnPieceCapture;

    private bool isAnimationsOn;

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

    public void MovePiece(Rigidbody pieceRB, Vector3 targetPos, bool isCapturing)
    {
        StartCoroutine(MovePieceCoroutine(pieceRB, targetPos, isCapturing));
    }

    private IEnumerator MovePieceCoroutine(Rigidbody pieceRB, Vector3 targetPos, bool isCapturing)
    {
        float squareMultiplier = Mathf.Abs((pieceRB.position - targetPos).z) / 2;

        if(isAnimationsOn)
        {
            float speed = (isCapturing ? pieceCaptureSpeed : pieceMovementSpeed) * squareMultiplier;
            while (Vector3.Distance(pieceRB.position, targetPos) > 0.1f)
            {
                Vector3 newPosition = Vector3.MoveTowards(pieceRB.position, targetPos, speed * Time.fixedDeltaTime);
                pieceRB.MovePosition(newPosition);
                yield return new WaitForFixedUpdate();
            }

            pieceRB.linearVelocity = Vector3.zero;
        }

        pieceRB.gameObject.transform.position = targetPos;
        if(isCapturing)
            OnPieceCapture.Invoke(pieceRB.GetComponent<PieceBehaviour>());
        OnPieceMovementFinished.Invoke();
    }
}
