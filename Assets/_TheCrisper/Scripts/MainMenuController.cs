using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    [Header("Menu Panels")]
    [SerializeField] private GameObject mainPanelGroup;
    [SerializeField] private GameObject optionsPanelGroup;

    private void Start()
    {
        ShowMainPanel();
    }

    public void StartGame()
    {
        Debug.Log("Start Game clicked.");

        if (string.IsNullOrWhiteSpace(gameplaySceneName))
        {
            Debug.LogWarning("Gameplay scene name is empty.");
            return;
        }

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked.");
        ShowOptionsPanel();
    }

    public void CloseOptions()
    {
        Debug.Log("Close Options clicked.");
        ShowMainPanel();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game clicked.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ShowMainPanel()
    {
        if (mainPanelGroup != null)
        {
            mainPanelGroup.SetActive(true);
        }

        if (optionsPanelGroup != null)
        {
            optionsPanelGroup.SetActive(false);
        }
    }

    private void ShowOptionsPanel()
    {
        if (mainPanelGroup != null)
        {
            mainPanelGroup.SetActive(false);
        }

        if (optionsPanelGroup != null)
        {
            optionsPanelGroup.SetActive(true);
        }
    }
}