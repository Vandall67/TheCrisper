using UnityEngine;

public class AudioAmbience : MonoBehaviour
{
    [SerializeField] private AudioClip sfxFridgeHum1;
    [SerializeField] private AudioClip sfxFridgeHum2;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.3f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        if (sfxFridgeHum1 != null)
        {
            audioSource.clip = sfxFridgeHum1;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void SwitchAmbience(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
