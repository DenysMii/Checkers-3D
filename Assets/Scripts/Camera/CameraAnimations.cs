using Unity.Cinemachine;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    [SerializeField] private CinemachineBrain targetCinemachineBrain;
    [SerializeField] private GameObject whiteCamera;
    [SerializeField] private GameObject blackCamera;

    public void SwitchCamera()
    {
        whiteCamera.SetActive(!whiteCamera.activeSelf);
        blackCamera.SetActive(!blackCamera.activeSelf);
    }

    public void SetAnimations(bool isOn)
    {
        targetCinemachineBrain.DefaultBlend = isOn ?
            new(CinemachineBlendDefinition.Styles.EaseInOut, 1f) :
            new(CinemachineBlendDefinition.Styles.Cut, 1f);
    }
}
