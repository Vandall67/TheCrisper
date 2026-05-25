using UnityEngine;

public class HeatBatteryPickup : MonoBehaviour
{
    [Header("Heat Recovery")]
    [SerializeField] private float restoreAmount = 30f;

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";

    [Header("Pickup Behaviour")]
    [SerializeField] private bool destroyAfterPickup = true;

    private bool wasCollected;

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

        if (destroyAfterPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}