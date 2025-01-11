using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    const string SOUND_PREF_NAME = "Sound";

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Text percentage;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SOUND_PREF_NAME))
            PlayerPrefsSetVolume();
        else
            SliderSetVolume(100);

        soundSlider.onValueChanged.AddListener(OnSoundChanged);
    }

    private void OnSoundChanged(float volume)
    {
        SliderSetVolume((int)volume);
    }

    private void PlayerPrefsSetVolume()
    {
        int volume = PlayerPrefs.GetInt(SOUND_PREF_NAME);
        soundSlider.value = volume;
        percentage.text = volume + "%";
        AudioListener.volume = volume / 100f;
    }

    public void SliderSetVolume(int volume)
    {
        percentage.text = volume + "%";
        AudioListener.volume = volume / 100f;
        PlayerPrefs.SetInt(SOUND_PREF_NAME, volume);
    }
}
