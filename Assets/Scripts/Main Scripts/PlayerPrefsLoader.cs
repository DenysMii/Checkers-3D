using UnityEngine;

public class PlayerPrefsLoader : MonoBehaviour
{
    [SerializeField] private SoundSettings soundSettings;
    [SerializeField] private FullscreenSettings fullscreenSettings;
    [SerializeField] private AnimationsSettings animationsSettings;
    [SerializeField] private PiecesSettings piecesSettings;
    [SerializeField] private BoardSettings boardSettings;
    [SerializeField] private BGColorSettings bgColorSettings;

    public void LoadPlayerPrefs()
    {
        soundSettings.LoadSettings();
        fullscreenSettings.LoadSettings();
        animationsSettings.LoadSettings();
        piecesSettings.LoadSettings();
        boardSettings.LoadSettings();
        bgColorSettings.LoadSettings();
    }
}
