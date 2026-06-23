using UnityEngine;

public class AudioShelfCreak : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioClip[] sfxShelfCreaks;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.4f;

    private AudioSource audioSource;
    private float minTimeBetweenCreaks = 2f;
    private float lastCreakTime;
    private int lastCreakIndex = -1;
    private Vector3 lastPosition;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (playerTransform != null)
        {
            lastPosition = playerTransform.position;
        }
    }

    private void Update()
    {
        if (playerTransform == null || sfxShelfCreaks == null || sfxShelfCreaks.Length == 0)
        {
            return;
        }

        Vector3 currentPosition = playerTransform.position;

        if (Vector3.Distance(currentPosition, lastPosition) > 0.01f &&
            Time.time - lastCreakTime > minTimeBetweenCreaks)
        {
            int index;

            do
            {
                index = Random.Range(0, sfxShelfCreaks.Length);
            }
            while (index == lastCreakIndex && sfxShelfCreaks.Length > 1);

            if (sfxShelfCreaks[index] != null)
            {
                audioSource.PlayOneShot(sfxShelfCreaks[index], GetFinalVolume());
            }

            lastCreakTime = Time.time;
            lastCreakIndex = index;
        }

        lastPosition = currentPosition;
    }

    private float GetFinalVolume()
    {
        return volume * AudioVolumeManager.SfxVolume;
    }
}