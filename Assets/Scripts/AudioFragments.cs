using System.Collections;
using UnityEngine;

public class AudioFragments : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip sfxFragmentCollect;
    [SerializeField] private AudioClip sfxHeatCollect;
    [SerializeField] private AudioClip sfxGameOverFreeze;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    [Header("Fragment Pickup Audio")]
    [SerializeField] private float fragmentSoundMaxDuration = 1.1f;

    private AudioSource audioSource;
    private int lastFragmentCount;
    private Coroutine stopFragmentSoundCoroutine;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    private void Start()
    {
        if (gameManager != null)
        {
            lastFragmentCount = gameManager.FragmentosRecolhidos;
        }
    }

    private void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        if (gameManager.FragmentosRecolhidos > lastFragmentCount)
        {
            PlayFragmentCollectSound();

            lastFragmentCount = gameManager.FragmentosRecolhidos;

            if (gameManager.FragmentosRecolhidos >= gameManager.TotalFragmentos)
            {
                if (sfxGameOverFreeze != null)
                {
                    audioSource.PlayOneShot(sfxGameOverFreeze, GetFinalVolume());
                }
            }
        }
    }

    private void PlayFragmentCollectSound()
    {
        if (sfxFragmentCollect == null)
        {
            return;
        }

        audioSource.Stop();
        audioSource.clip = sfxFragmentCollect;
        audioSource.volume = GetFinalVolume();
        audioSource.Play();

        if (stopFragmentSoundCoroutine != null)
        {
            StopCoroutine(stopFragmentSoundCoroutine);
        }

        stopFragmentSoundCoroutine = StartCoroutine(StopFragmentSoundAfterDelay());
    }

    private IEnumerator StopFragmentSoundAfterDelay()
    {
        yield return new WaitForSeconds(fragmentSoundMaxDuration);

        if (audioSource != null && audioSource.clip == sfxFragmentCollect)
        {
            audioSource.Stop();
        }
    }

    private float GetFinalVolume()
    {
        return volume * AudioVolumeManager.SfxVolume;
    }
}