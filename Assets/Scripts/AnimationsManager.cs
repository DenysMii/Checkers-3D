using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineBrain targetCinemachineBrain;
    [SerializeField] private GameObject whiteCamera;
    [SerializeField] private GameObject blackCamera;

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

    public void MovePiece(Transform pieceTransform, Vector3 targetPos, float duration)
    {
        StartCoroutine(MovePieceCoroutine(pieceTransform, targetPos, duration));
    }

    private IEnumerator MovePieceCoroutine(Transform pieceTransform, Vector3 targetPos, float duration)
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
    }
}
