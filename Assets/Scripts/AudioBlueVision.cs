using UnityEngine;

public class AudioBlueVision : MonoBehaviour
{
    [SerializeField] private GameObject blueVisionOverlay;
    [SerializeField] private AudioClip sfxVisionActivate;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private bool wasActive;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        wasActive = blueVisionOverlay != null && blueVisionOverlay.activeSelf;
    }

    private void Update()
    {
        if (blueVisionOverlay == null)
        {
            return;
        }

        bool isActive = blueVisionOverlay.activeSelf;

        if (!wasActive && isActive && sfxVisionActivate != null)
        {
            audioSource.PlayOneShot(sfxVisionActivate, GetFinalVolume());
        }

        wasActive = isActive;
    }

    private float GetFinalVolume()
    {
        return volume * AudioVolumeManager.SfxVolume;
    }
}