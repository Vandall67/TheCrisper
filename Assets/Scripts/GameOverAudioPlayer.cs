using UnityEngine;

public class GameOverAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip defeatClip;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private void Start()
    {
        AudioVolumeManager.ApplySavedMasterVolume();

        if (defeatClip != null)
        {
            AudioSource.PlayClipAtPoint(
                defeatClip,
                Camera.main != null ? Camera.main.transform.position : Vector3.zero,
                volume * AudioVolumeManager.SfxVolume
            );
        }
    }
}