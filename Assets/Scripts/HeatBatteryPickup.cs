using UnityEngine;

public class HeatBatteryPickup : MonoBehaviour
{
    [Header("Heat Recovery")]
    [SerializeField] private float restoreAmount = 30f;

    [Header("Audio")]
    [SerializeField] private AudioClip pickupSfx;

    [Range(0f, 2f)]
    [SerializeField] private float pickupVolume = 1.5f;

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";

    [Header("Pickup Behaviour")]
    [SerializeField] private bool destroyAfterPickup = true;

    private AudioSource audioSource;
    private bool wasCollected;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // 0 = som 2D. Assim não fica baixo por distância à câmara.
        audioSource.spatialBlend = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasCollected)
        {
            return;
        }

        if (!other.CompareTag(playerTag))
        {
            return;
        }

        PlayerTemperatureController temperatureController =
            other.GetComponent<PlayerTemperatureController>();

        if (temperatureController == null)
        {
            temperatureController = other.GetComponentInParent<PlayerTemperatureController>();
        }

        if (temperatureController == null)
        {
            Debug.LogWarning("HeatBatteryPickup: Player não tem PlayerTemperatureController.", this);
            return;
        }

        wasCollected = true;

        temperatureController.RestoreTemperature(restoreAmount);

        DisableVisualsAndColliders();
        PlayPickupSound();

        if (destroyAfterPickup)
        {
            float destroyDelay = pickupSfx != null ? pickupSfx.length : 0f;
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSfx == null || audioSource == null)
        {
            return;
        }

        AudioVolumeManager.ApplySavedMasterVolume();

        audioSource.PlayOneShot(
            pickupSfx,
            pickupVolume * AudioVolumeManager.SfxVolume
        );
    }

    private void DisableVisualsAndColliders()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}