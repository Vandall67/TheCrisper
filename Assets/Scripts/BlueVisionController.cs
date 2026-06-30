using UnityEngine;
using System.Collections;

public class BlueVisionController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode toggleKey = KeyCode.B;

    [Header("References")]
    [SerializeField] private HUDController hudController;
    [SerializeField] private GameObject blueVisionOverlay;

    [Header("Runtime")]
    [SerializeField] private bool isBlueVisionActive;

    public bool IsBlueVisionActive => isBlueVisionActive;

    private BlueVisionRevealable[] revealableObjects;// Cache of revealable objects in the scene

    public Animator playerAnim;

    private void Start()
    {   // Find all revealable objects in the scene at the start and cache them
        revealableObjects = FindObjectsByType<BlueVisionRevealable>(FindObjectsSortMode.None);
        foreach (var obj in revealableObjects)
        {
            //Debug.Log($"!!!!!!!!!!!!!!!Found: {obj.name} at {obj.transform.position}");
        }
        SetBlueVisionState(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))// Toggle blue vision when the specified key is pressed
        {
            ToggleBlueVision();
        }
    }

    public void ToggleBlueVision()
    {
        SetBlueVisionState(!isBlueVisionActive);// Toggle the blue vision state
    }

    public void SetBlueVisionState(bool isActive)//
    {
        isBlueVisionActive = isActive;

        // Update the blue vision overlay
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
        // Set the blue vision state for each revealable object
        foreach (BlueVisionRevealable revealable in revealableObjects)
        {
            if (revealable != null)
            {
                revealable.SetBlueVisionState(isBlueVisionActive);
            }
        }
    }
}