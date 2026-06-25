using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerTemperatureController : MonoBehaviour
{
    [Header("Temperature")]
    [SerializeField] private float maxTemperature = 100f;
    [SerializeField] private float currentTemperature = 100f;
    [SerializeField] private float baseTemperatureLossPerSecond = 1f;

    [Header("Blue Vision Temperature Cost")]
    [SerializeField] private float blueVisionExtraLossPerSecond = 3f;

    [Header("References")]
    [SerializeField] private HUDController hudController;
    [SerializeField] private ColdVisualController coldVisualController;
    [SerializeField] private BlueVisionController blueVisionController;

    [Header("Game Over")]
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private bool triggerGameOverAtZero = true;

    public Animator playerAnim;
    public float CurrentTemperature => currentTemperature;
    public float MaxTemperature => maxTemperature;
    public float TemperaturePercentage => maxTemperature <= 0f ? 0f : (currentTemperature / maxTemperature) * 100f;
    public bool IsFrozen => currentTemperature <= 0f;

    private bool gameOverTriggered;

    private void Start()
    {
        currentTemperature = Mathf.Clamp(currentTemperature, 0f, maxTemperature);
        UpdateTemperatureFeedback();
    }

    private void Update()
    {
        if (gameOverTriggered)
        {
            return;
        }

        float totalLoss = baseTemperatureLossPerSecond;

        if (blueVisionController != null && blueVisionController.IsBlueVisionActive)
        {
            totalLoss += blueVisionExtraLossPerSecond;
        }

        ReduceTemperature(totalLoss * Time.deltaTime);
    }

    public void ReduceTemperature(float amount)
    {
        if (amount <= 0f || IsFrozen)
        {
            return;
        }

        currentTemperature = Mathf.Clamp(currentTemperature - amount, 0f, maxTemperature);
        UpdateTemperatureFeedback();

        if (IsFrozen && triggerGameOverAtZero)
        {
            TriggerGameOver();
        }
    }

    public void RestoreTemperature(float amount)
    {
        if (amount <= 0f || gameOverTriggered)
        {
            return;
        }

        currentTemperature = Mathf.Clamp(currentTemperature + amount, 0f, maxTemperature);
        UpdateTemperatureFeedback();
    }

    private void UpdateTemperatureFeedback()
    {
        if (hudController != null)
        {
            hudController.UpdateTemperature(TemperaturePercentage);
        }

        if (coldVisualController != null)
        {
            float temperature01 = TemperaturePercentage / 100f;

            // O efeito visual de frio só começa a ser mais evidente abaixo de 70%.
            // Isto evita que a vinheta de gelo fique forte demasiado cedo.
            float coldIntensity = Mathf.InverseLerp(70f, 0f, TemperaturePercentage);

            // Curva suave: torna o crescimento do gelo mais gradual.
            coldIntensity = coldIntensity * coldIntensity;

            coldVisualController.SetColdIntensity(coldIntensity);
        }
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;
        Time.timeScale = 1f;
        playerAnim.SetTrigger("idle");
        playerAnim.SetTrigger("death");

        //show Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(LoadGameOverScene());
    }


    //Load next scene after death and after a few seconds
    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(gameOverSceneName);
    }
}
