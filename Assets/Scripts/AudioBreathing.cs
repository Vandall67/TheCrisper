using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioBreathing : MonoBehaviour
{
    [SerializeField] private Image coldOverlayImage;
    [SerializeField] private AudioClip sfxBreathingNormal;
    [SerializeField] private AudioClip sfxBreathingCold;
    [SerializeField] private AudioClip sfxIceCrack;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    [Header("Thresholds")]
    [SerializeField] private float maxOverlayAlpha = 0.28f;
    [SerializeField] private float threshold = 0.08f;

    [Range(0f, 1f)]
    [SerializeField] private float iceCrackThreshold = 0.9f;

    private AudioSource audioSource;
    private bool isCrossfading;
    private bool iceCrackPlayed;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        if (sfxBreathingNormal != null)
        {
            audioSource.clip = sfxBreathingNormal;
            audioSource.volume = GetFinalVolume();
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (audioSource != null && !isCrossfading)
        {
            audioSource.volume = GetFinalVolume();
        }

        if (coldOverlayImage == null || isCrossfading)
        {
            return;
        }

        float alpha = coldOverlayImage.color.a;

        if (alpha > threshold && audioSource.clip == sfxBreathingNormal)
        {
            StartCoroutine(Crossfade(sfxBreathingCold));
        }
        else if (alpha <= threshold && audioSource.clip == sfxBreathingCold)
        {
            StartCoroutine(Crossfade(sfxBreathingNormal));
        }

        if (alpha >= iceCrackThreshold * maxOverlayAlpha)
        {
            if (!iceCrackPlayed && sfxIceCrack != null)
            {
                audioSource.PlayOneShot(sfxIceCrack, GetFinalVolume());
                iceCrackPlayed = true;
            }
        }
        else
        {
            iceCrackPlayed = false;
        }
    }

    private IEnumerator Crossfade(AudioClip newClip)
    {
        isCrossfading = true;

        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / 0.5f);
            yield return null;
        }

        audioSource.volume = 0f;

        audioSource.clip = newClip;
        audioSource.Play();

        elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, GetFinalVolume(), elapsed / 0.5f);
            yield return null;
        }

        audioSource.volume = GetFinalVolume();
        isCrossfading = false;
    }

    private float GetFinalVolume()
    {
        return volume * AudioVolumeManager.SfxVolume;
    }
}