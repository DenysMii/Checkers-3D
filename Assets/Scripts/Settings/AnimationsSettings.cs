using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsSettings : MonoBehaviour
{
    const string ANIM_PREF_NAME = "Animations";

    [SerializeField] private Toggle animationsToggle;

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
        StaticData.animationsManager.SetAnimationsState(animationsToggle.isOn);
    }

    public void ToggleSetAnimations(bool isOn)
    {
        int toggleInt = isOn ? 1 : 0;
        StaticData.animationsManager.SetAnimationsState(isOn);
        PlayerPrefs.SetInt(ANIM_PREF_NAME, toggleInt);
    }
}
