using UnityEngine;

public class AudioFragments : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip sfxFragmentCollect;
    [SerializeField] private AudioClip sfxHeatCollect;
    [SerializeField] private AudioClip sfxGameOverFreeze;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private int lastFragmentCount;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (gameManager != null)
            lastFragmentCount = gameManager.fragmentosRecolhidos;
    }

    private void Update()
    {
        if (gameManager == null) return;

        if (gameManager.fragmentosRecolhidos > lastFragmentCount)
        {
            if (sfxFragmentCollect != null)
                audioSource.PlayOneShot(sfxFragmentCollect, volume);

            lastFragmentCount = gameManager.fragmentosRecolhidos;

            if (gameManager.fragmentosRecolhidos >= gameManager.totalFragmentos)
            {
                if (sfxGameOverFreeze != null)
                    audioSource.PlayOneShot(sfxGameOverFreeze, volume);
            }
        }
    }
}
