using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour
{

    [Header("UI Elements")]
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    void Start()
    {
        // Load available resolutions
        LoadSettings();
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        //// Load values from PlayerPrefs
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        // Apply loaded settings
        SetFullscreen(fullscreenToggle.isOn);
        SetVolume();
    }


    public void Close()
    {
        gameObject.SetActive(false);
    }
}
