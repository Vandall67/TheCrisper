using UnityEngine;

public class FragmentoMapa : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private GameManager gameManager;
    private bool wasCollected;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasCollected)
        {
            return;
        }

        if (!other.CompareTag(playerTag))
        {
            return;
        }

        if (gameManager == null)
        {
            Debug.LogWarning("FragmentoMapa: GameManager não encontrado.", this);
            return;
        }

        wasCollected = true;

        DisableAllColliders();
        DisableAllRenderers();

        Debug.Log($"Fragmento recolhido: {gameObject.name} | ID: {GetInstanceID()}");

        gameManager.RecolherFragmento();

        gameObject.SetActive(false);
    }

    private void DisableAllColliders()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>(true);

        foreach (Collider fragmentCollider in colliders)
        {
            fragmentCollider.enabled = false;
        }
    }

    private void DisableAllRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}