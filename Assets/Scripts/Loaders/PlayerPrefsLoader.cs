using UnityEngine;

public class PlayerPrefsLoader : MonoBehaviour
{
    [SerializeField] private SoundSettings soundSettings;
    [SerializeField] private AnimationsSettings animationsSettings;
    [SerializeField] private PiecesSettings piecesSettings;
    [SerializeField] private BoardSettings boardSettings;
    [SerializeField] private BGColorSettings bgColorSettings;

    public void LoadPlayerPrefs()
    {
        soundSettings.LoadSettings();
        animationsSettings.LoadSettings();
        piecesSettings.LoadSettings();
        boardSettings.LoadSettings();
        bgColorSettings.LoadSettings();
    }
}
