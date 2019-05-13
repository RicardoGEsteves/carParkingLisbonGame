using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class SettingsController : MonoBehaviour {
    public Toggle fullscreenToggle;
    public Dropdown resolutionDrop;
    public Dropdown textQualityDrop;
    public Dropdown antialiasingDrop;
    public Dropdown vSyncDrop;
    public Slider volume;
    public Button saveButton;
    public Resolution[] resolutions;
    public Settings gameSettings;


    void OnEnable()
    {
        gameSettings = new Settings();
        fullscreenToggle.onValueChanged.AddListener(delegate { FullscreenToggle(); });
        resolutionDrop.onValueChanged.AddListener(delegate { ResolutionChange(); });
        textQualityDrop.onValueChanged.AddListener(delegate { TextQChange(); });
        antialiasingDrop.onValueChanged.AddListener(delegate { AntialiasingChange(); });
        vSyncDrop.onValueChanged.AddListener(delegate { VsyncChange(); });
        volume.onValueChanged.AddListener(delegate { VolumeChange(); });
        saveButton.onClick.AddListener(delegate { saveSettings(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDrop.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        loadSettings();
    }

    public void FullscreenToggle()
    {
       gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void ResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDrop.value].width, resolutions[resolutionDrop.value].height, Screen.fullScreen, resolutions[resolutionDrop.value].refreshRate);
        gameSettings.resolutionIndex = resolutionDrop.value;
    }

    public void AntialiasingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2, antialiasingDrop.value);
    }

    public void VsyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDrop.value;
    }

    public void TextQChange()
    {
        gameSettings.textureQuality = QualitySettings.masterTextureLimit = textQualityDrop.value;
    }

    public void VolumeChange()
    {
        gameSettings.volume = AudioListener.volume = volume.value;
    }

    public void saveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings,true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        MenuController.instance.closeOptions();
    }

    public void loadSettings()
    {
        gameSettings = JsonUtility.FromJson<Settings>(File.ReadAllText( Application.persistentDataPath + "/gamesettings.json"));
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDrop.value = gameSettings.resolutionIndex;
        antialiasingDrop.value = gameSettings.antialiasing;
        vSyncDrop.value = gameSettings.vSync;
        textQualityDrop.value = gameSettings.textureQuality;
        volume.value = gameSettings.volume;
        resolutionDrop.RefreshShownValue();
    }
}
