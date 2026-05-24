using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneTrigger : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameOverSceneName = "GameOver";

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
        {
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }
}