using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

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
}