using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsSettings : MonoBehaviour
{
    const string ANIM_PREF_NAME = "Animations";

    [SerializeField] private Toggle animationsToggle;
    [SerializeField] private CameraAnimations cameraAnimations;

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(ANIM_PREF_NAME))
            PlayerPrefsSetAnimations();
        else
            ToggleSetAnimations(true);

        animationsToggle.onValueChanged.AddListener(OnAnimationsChanged);
    }

    private void OnAnimationsChanged(bool isOn)
    {
        ToggleSetAnimations(isOn);
    }

    private void PlayerPrefsSetAnimations()
    {
        animationsToggle.isOn = PlayerPrefs.GetInt(ANIM_PREF_NAME) == 1;
        cameraAnimations.SetAnimations(animationsToggle.isOn);
    }

    public void ToggleSetAnimations(bool isOn)
    {
        int toggleInt = isOn ? 1 : 0;
        cameraAnimations.SetAnimations(isOn);
        PlayerPrefs.SetInt(ANIM_PREF_NAME, toggleInt);
    }
}
