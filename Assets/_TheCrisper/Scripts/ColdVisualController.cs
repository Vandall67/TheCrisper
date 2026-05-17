using UnityEngine;
using UnityEngine.UI;

public class ColdVisualController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image coldOverlayImage;
    [SerializeField] private Image frostFrameImage;

    [Header("Cold Visual Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float coldIntensity;

    [SerializeField] private Color coldOverlayColor = new Color(0.45f, 0.85f, 1f, 0f);
    [SerializeField] private float maxOverlayAlpha = 0.28f;
    [SerializeField] private float maxFrostAlpha = 0.85f;

    [Header("Debug Controls")]
    [SerializeField] private bool enableDebugKeys = true;

    private void Start()
    {
        SetColdIntensity(0f);
    }

    private void Update()
    {
        if (!enableDebugKeys)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetColdIntensity(0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetColdIntensity(0.35f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetColdIntensity(0.7f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetColdIntensity(1f);
        }
    }

    public void SetColdIntensity(float intensity)
    {
        coldIntensity = Mathf.Clamp01(intensity);
        ApplyColdVisual();
    }

    private void ApplyColdVisual()
    {
        if (coldOverlayImage != null)
        {
            Color overlayColor = coldOverlayColor;
            overlayColor.a = coldIntensity * maxOverlayAlpha;
            coldOverlayImage.color = overlayColor;
        }

        if (frostFrameImage != null)
        {
            Color frostColor = Color.white;
            frostColor.a = coldIntensity * maxFrostAlpha;
            frostFrameImage.color = frostColor;
        }
    }
}