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
            lastFragmentCount = gameManager.FragmentosRecolhidos; //fragmentosRecolhidos;
    }

    private void Update()
    {
        if (gameManager == null) return;

        if (gameManager.FragmentosRecolhidos > lastFragmentCount) //fragmentosRecolhidos;
        {
            if (sfxFragmentCollect != null)
                audioSource.PlayOneShot(sfxFragmentCollect, volume);

            lastFragmentCount = gameManager.FragmentosRecolhidos; //fragmentosRecolhidos;

            if (gameManager.FragmentosRecolhidos >= gameManager.TotalFragmentos) //totalFragmentos
            {
                if (sfxGameOverFreeze != null)
                    audioSource.PlayOneShot(sfxGameOverFreeze, volume);
            }
        }
    }
}
