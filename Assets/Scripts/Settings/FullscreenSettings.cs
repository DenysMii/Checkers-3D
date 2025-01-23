using UnityEngine;
using UnityEngine.UI;

public class FullscreenSettings : MonoBehaviour
{
    const string FS_PREF_NAME = "Fullscreen";

    [SerializeField] private Toggle fullscreenToggle;

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(FS_PREF_NAME))
            PlayerPrefsSetFullscreen();
        else
            ToggleSetFullscreen(true);

        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void OnFullscreenChanged(bool isOn)
    {
        ToggleSetFullscreen(isOn);
    }

    private void PlayerPrefsSetFullscreen()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt(FS_PREF_NAME) == 1;
        if(fullscreenToggle.isOn)
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        else
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);
    }

    public void ToggleSetFullscreen(bool isOn)
    {
        int toggleInt = isOn ? 1 : 0;
        if (fullscreenToggle.isOn)
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        else
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        PlayerPrefs.SetInt(FS_PREF_NAME, toggleInt);
    }
}
