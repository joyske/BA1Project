using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour
{

    [Header("UI Elements")]
    public TMP_Dropdown windowModeDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_InputField maxFpsInput;
    public Slider sensitivitySlider;
    public Toggle invertYAxisToggle;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    void Start()
    {
        // Load available resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height));
        }

        LoadSettings();
    }

    public void SetResolution(int index)
    {
        if (index >= 0 && index < resolutions.Length)
        {
            Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
        }
        SaveSettings();
    }

    public void SetMaxFPS(string fpsText)
    {
        if (int.TryParse(fpsText, out int fps))
        {
            Application.targetFrameRate = (fps > 0) ? fps : -1;
        }
        SaveSettings();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("MaxFPS", int.Parse(maxFpsInput.text));
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        //// Load values from PlayerPrefs
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", resolutions.Length - 1);
        maxFpsInput.text = PlayerPrefs.GetInt("MaxFPS", -1).ToString();
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);

        // Apply loaded settings
        SetResolution(resolutionDropdown.value);
        SetMaxFPS(maxFpsInput.text);
        SetVolume(volumeSlider.value);
    }


    public void Close()
    {
        gameObject.SetActive(false);
    }
}
