using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string targetSceneName = "Gameplay";
    [SerializeField] private float minimumLoadingTime = 1.5f;

    [Header("UI References")]
    [SerializeField] private Image loadingBarFill;

    private void Start()
    {
        StartCoroutine(LoadTargetSceneAsync());
    }

    private IEnumerator LoadTargetSceneAsync()
    {
        float elapsedTime = 0f;

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(targetSceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            elapsedTime += Time.deltaTime;

            float loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(elapsedTime / minimumLoadingTime);
            float displayedProgress = Mathf.Min(loadingProgress, timeProgress);

            if (loadingBarFill != null)
            {
                loadingBarFill.fillAmount = displayedProgress;
            }

            if (loadingOperation.progress >= 0.9f && elapsedTime >= minimumLoadingTime)
            {
                if (loadingBarFill != null)
                {
                    loadingBarFill.fillAmount = 1f;
                }

                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
