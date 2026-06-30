using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameplaySceneName1 = "Nivel_01_PrateleiraInferior";
    [SerializeField] private string gameplaySceneName2 = "Nivel_02_PrateleiraSuperior";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void RetryGame()
    {
        Time.timeScale = 1f;
        if (GameGlobal.levelcompleted == 0)
        {
            SceneManager.LoadScene(gameplaySceneName1);
        }
        if (GameGlobal.levelcompleted == 1)
        {
            SceneManager.LoadScene(gameplaySceneName2);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}