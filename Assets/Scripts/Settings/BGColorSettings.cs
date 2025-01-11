using HSVPicker;
using UnityEngine;
using UnityEngine.UI;

public class BGColorSettings : MonoBehaviour
{
    const string BGCOLOR_PREF_NAME = "BGColor";

    [SerializeField] private ColorPicker colorPicker;
    private void Start()
    {
        if (PlayerPrefs.HasKey(BGCOLOR_PREF_NAME) && ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(BGCOLOR_PREF_NAME), out Color color))
        {
            colorPicker.CurrentColor = color;
            colorPicker.onValueChanged.Invoke(color);
        }
        else
            Debug.Log("Somethings wrong");
    }

    public void SetPlayerPrefs()
    {
        string colorHex = "#" + ColorUtility.ToHtmlStringRGB(colorPicker.CurrentColor);
        PlayerPrefs.SetString(BGCOLOR_PREF_NAME, colorHex);
    }
}
