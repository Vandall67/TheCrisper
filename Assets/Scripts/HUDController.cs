using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("HUD Texts")]
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private TMP_Text fragmentsText;
    [SerializeField] private TMP_Text blueVisionText;

    [Header("HUD Bars")]
    [SerializeField] private Image temperatureBarFill;

    [Header("Initial Values")]
    [SerializeField] private int totalFragments = 4;

    private void Start()
    {
        UpdateTemperature(100);
        UpdateFragments(0);
        UpdateBlueVision(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateTemperature(100);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateTemperature(60);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateTemperature(25);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateFragments(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateFragments(4);
        }

       /*  if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateBlueVision(true);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            UpdateBlueVision(false);
        } */
    }

    public void UpdateTemperature(float temperaturePercentage)
    {
        float clampedTemperature = Mathf.Clamp(temperaturePercentage, 0f, 100f);
        int roundedTemperature = Mathf.RoundToInt(clampedTemperature);

        if (temperatureText != null)
        {
            temperatureText.text = $"{roundedTemperature}%";
        }

        if (temperatureBarFill != null)
        {
            temperatureBarFill.fillAmount = clampedTemperature / 100f;
        }
    }

    public void UpdateFragments(int collectedFragments)
    {
        if (fragmentsText != null)
        {
            fragmentsText.text = $"Fragmentos: {collectedFragments} / {totalFragments}";
        }
    }

    public void UpdateBlueVision(bool isActive)
    {
        if (blueVisionText != null)
        {
            blueVisionText.text = isActive ? "Visão Azul: Ativa" : "Visão Azul: Inativa";
        }
    }
}