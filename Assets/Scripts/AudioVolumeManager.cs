using UnityEngine;

public static class AudioVolumeManager
{
    public const string MasterVolumeKey = "MasterVolume";
    public const string MusicVolumeKey = "MusicVolume";
    public const string SfxVolumeKey = "SfxVolume";

    public static float MasterVolume => PlayerPrefs.GetFloat(MasterVolumeKey, 0.8f);
    public static float MusicVolume => PlayerPrefs.GetFloat(MusicVolumeKey, 0.8f);
    public static float SfxVolume => PlayerPrefs.GetFloat(SfxVolumeKey, 0.8f);

    public static void SetMasterVolume(float value)
    {
        value = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        PlayerPrefs.Save();

        AudioListener.volume = value;
    }

    public static void SetMusicVolume(float value)
    {
        value = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        PlayerPrefs.Save();
    }

    public static void SetSfxVolume(float value)
    {
        value = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        PlayerPrefs.Save();
    }

    public static void ApplySavedMasterVolume()
    {
        AudioListener.volume = MasterVolume;
    }
}