using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Scene Transition")]
    [SerializeField] private string sceneToLoad;

    [Header("Completion Requirement")]
    [SerializeField] private bool requireAllFragments = true;
    [SerializeField] private GameManager gameManager;

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
        {
            return;
        }

        if (requireAllFragments)
        {
            if (gameManager == null)
            {
                Debug.LogWarning("LevelEndTrigger: GameManager não está atribuído.", this);
                return;
            }

            if (!gameManager.MapaCompleto)
            {
                Debug.Log("Ainda faltam fragmentos do mapa: " +
                          gameManager.FragmentosRecolhidos + "/" +
                          gameManager.TotalFragmentos);
                return;
            }
        }

        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning("LevelEndTrigger: sceneToLoad não está definido.", this);
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }
}