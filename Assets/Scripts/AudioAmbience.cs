using UnityEngine;

public class AudioAmbience : MonoBehaviour
{
    [Header("Ambience Clips")]
    [SerializeField] private AudioClip sfxFridgeHum1;
    [SerializeField] private AudioClip sfxFridgeHum2;

    [Header("Temperature Reference")]
    [SerializeField] private PlayerTemperatureController temperatureController;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float baseVolume = 0.3f;

    [Header("Temperature Audio Behaviour")]
    [SerializeField] private float tensionStartTemperature = 70f;
    [SerializeField] private float fullTensionTemperature = 25f;

    private AudioSource calmSource;
    private AudioSource tenseSource;

    private void Awake()
    {
        calmSource = gameObject.AddComponent<AudioSource>();
        tenseSource = gameObject.AddComponent<AudioSource>();

        calmSource.loop = true;
        tenseSource.loop = true;

        calmSource.playOnAwake = false;
        tenseSource.playOnAwake = false;
    }

    private void Start()
    {
        if (sfxFridgeHum1 != null)
        {
            calmSource.clip = sfxFridgeHum1;
            calmSource.Play();
        }

        if (sfxFridgeHum2 != null)
        {
            tenseSource.clip = sfxFridgeHum2;
            tenseSource.Play();
        }

        UpdateAmbience();
    }

    private void Update()
    {
        UpdateAmbience();
    }

    private void UpdateAmbience()
    {
        float tension = GetTensionFactor();
        float musicVolume = AudioVolumeManager.MusicVolume;

        if (calmSource != null)
        {
            calmSource.volume = baseVolume * musicVolume * (1f - tension);
            calmSource.pitch = Mathf.Lerp(1f, 0.95f, tension);
        }

        if (tenseSource != null)
        {
            tenseSource.volume = baseVolume * musicVolume * tension;
            tenseSource.pitch = Mathf.Lerp(0.95f, 1.05f, tension);
        }
    }

    private float GetTensionFactor()
    {
        if (temperatureController == null)
        {
            return 0f;
        }

        float temperature = temperatureController.TemperaturePercentage;

        return Mathf.InverseLerp(
            tensionStartTemperature,
            fullTensionTemperature,
            temperature
        );
    }
}