using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void OnEnable()
    {
        LoadSettings();
        RegisterSliderEvents();
        AudioVolumeManager.ApplySavedMasterVolume();
    }

    private void OnDisable()
    {
        UnregisterSliderEvents();
    }

    private void LoadSettings()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.SetValueWithoutNotify(AudioVolumeManager.MasterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.SetValueWithoutNotify(AudioVolumeManager.MusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.SetValueWithoutNotify(AudioVolumeManager.SfxVolume);
        }
    }

    private void RegisterSliderEvents()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    private void UnregisterSliderEvents()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }
    }

    private void SetMasterVolume(float value)
    {
        AudioVolumeManager.SetMasterVolume(value);
        Debug.Log($"Master volume set to: {value}");
    }

    private void SetMusicVolume(float value)
    {
        AudioVolumeManager.SetMusicVolume(value);
        Debug.Log($"Music volume set to: {value}");
    }

    private void SetSfxVolume(float value)
    {
        AudioVolumeManager.SetSfxVolume(value);
        Debug.Log($"SFX volume set to: {value}");
    }
}