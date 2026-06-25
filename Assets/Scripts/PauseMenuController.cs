using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject pausePanelGroup;
    [SerializeField] private GameObject pauseOptionsPanelGroup;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused;

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape)) // when using escape to resume the cursor leaves when you go back to gameplay so using another key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            //Debug.Log(isPaused);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        ShowPausePanel();
        Time.timeScale = 1f;

        //hide Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        isPaused = true;

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
        }

        ShowPausePanel();
        Time.timeScale = 0f;

        //show Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenOptions()
    {
        if (pausePanelGroup != null)
        {
            pausePanelGroup.SetActive(false);
        }

        if (pauseOptionsPanelGroup != null)
        {
            pauseOptionsPanelGroup.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        ShowPausePanel();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void ShowPausePanel()
    {
        if (pausePanelGroup != null)
        {
            pausePanelGroup.SetActive(true);
        }

        if (pauseOptionsPanelGroup != null)
        {
            pauseOptionsPanelGroup.SetActive(false);
        }
    }
}