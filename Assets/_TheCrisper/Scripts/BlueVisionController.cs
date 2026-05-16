using UnityEngine;

public class BlueVisionController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode toggleKey = KeyCode.B;

    [Header("References")]
    [SerializeField] private HUDController hudController;
    [SerializeField] private GameObject blueVisionOverlay;

    [Header("Runtime")]
    [SerializeField] private bool isBlueVisionActive;

    private BlueVisionRevealable[] revealableObjects;

    private void Start()
    {
        revealableObjects = FindObjectsByType<BlueVisionRevealable>(FindObjectsSortMode.None);
        SetBlueVisionState(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleBlueVision();
        }
    }

    public void ToggleBlueVision()
    {
        SetBlueVisionState(!isBlueVisionActive);
    }

    public void SetBlueVisionState(bool isActive)
    {
        isBlueVisionActive = isActive;

        if (blueVisionOverlay != null)
        {
            blueVisionOverlay.SetActive(isBlueVisionActive);
        }

        if (hudController != null)
        {
            hudController.UpdateBlueVision(isBlueVisionActive);
        }

        if (revealableObjects == null)
        {
            return;
        }

        foreach (BlueVisionRevealable revealable in revealableObjects)
        {
            if (revealable != null)
            {
                revealable.SetBlueVisionState(isBlueVisionActive);
            }
        }
    }
}